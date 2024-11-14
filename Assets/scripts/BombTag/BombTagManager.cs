using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class BombTagManager : MonoBehaviour
{

    public float _baseTimer = 30;
    public float _bombTimer = 30;
    public bool _boom = false;
    [SerializeField] List<PlayerController> _players;
    [SerializeField] float  _baseSpeed;
    public ScoreManager _scoreManager;
    PartyPlayerParameters _playerParameters;
    PlayerBombTag _playerBombTag;
    public PlayerController _hasBomb;
    FeedBackManager _fBM;
    [SerializeField] GameObject _rules;
    public bool _gameReady = false;
    [SerializeField] float _cd;

    public List<PlayerController> Players { get { return _players; } }

    private void Awake()
    {
        _playerParameters = FindObjectOfType<PartyPlayerParameters>();
        _fBM = FeedBackManager.Instance;
    }
    private void Start()
    {
        StartCoroutine(TimerForRule());
        AssignRandomBomb();
        _baseSpeed = _playerParameters.PlayerBaseSpeed;
        _scoreManager = GameManager.Instance.GetScoreManager();
        foreach (PlayerController playerController in _players)
        {
            playerController.GetPlayerBombTag().Init();
        }
    }
    void Update()
    {
        if (_gameReady)
        {
            BombTimer();
            List<PlayerController> list = GameManager.Instance.GetPlayerManager().GetActivePlayers();
            if (list.Count == 1)
            {
                if (_hasBomb.GetPlayerBombTag().HasPoint == false)
                {
                    FinDeGame();

                    _hasBomb.GetPlayerBombTag().HasPoint = true;
                }
            }
            if (_boom)
            {
                if (_hasBomb.GetPlayerBombTag().HasPoint == false)
                {
                    _scoreManager.AddPlayerToList(_hasBomb, _scoreManager.Bonus);
                    _fBM.InstantiateParticle(_fBM.m_explosionVfx, _hasBomb.gameObject.transform.position, _hasBomb.gameObject.transform.rotation);
                    _hasBomb.gameObject.SetActive(false);
                    _hasBomb.GetPlayerBombTag().HasPoint = true;
                    _bombTimer = _baseTimer;
                }
                if (list.Count >= 2)
                {
                    // TODO : si la fine gagne des point return;
                    AssignRandomBomb();
                    
                }
                _boom = false;
            }
        }
    }

    public void AssignRandomBomb()
    {
        _players = GameManager.Instance.GetPlayerManager().GetActivePlayers();

        // Réinitialiser tous les hasBomb à false
        foreach (PlayerController player in _players)
        {
            player.GetPlayerBombTag()._hasBomb = false;
        }

        // Choisir un joueur au hasard pour lui donner la bombe
        int randomIndex = UnityEngine.Random.Range(0, _players.Count);
        _players[randomIndex].GetPlayerBombTag()._hasBomb = true;
        _hasBomb = _players[randomIndex];
        _hasBomb.GetPlayerMovement().SetPlayerSpeed(_baseSpeed+ _hasBomb.GetPlayerBombTag()._hasBombSpeed);

    }
    void BombTimer()
    {
        if (_bombTimer > 0)
        {
            _bombTimer = Mathf.Clamp(_bombTimer, 0, _baseTimer) - Time.deltaTime; // diminue le timer de la bombe au fine du temps 
        }
        else if (_bombTimer <= 0)
        {
            _boom = true;
        }
    }

    void FinDeGame()
    {
        List<PlayerController> list = GameManager.Instance.GetPlayerManager().GetActivePlayers();
        
        foreach (PlayerController playerController in list)
        {
            _scoreManager.OneWin();
        }
        GameManager.Instance.GetPartyManager().ChangeMiniGame();
    }
    IEnumerator TimerForRule()
    {
        yield return new WaitForSeconds(_cd);
        _rules.SetActive(false);
        _gameReady = true;
    }

}
