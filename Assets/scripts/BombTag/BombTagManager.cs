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
    [SerializeField] float _seeRuleCD;
    
    public WhoWin _whoWin;
    private bool _gameEnded = false;

    public event Action OnGameFinished;
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
        _whoWin = FindObjectOfType<WhoWin>();
        foreach (PlayerController playerController in _players)
        {
            playerController.GetPlayerBombTag().Init();
        }
    }
    void Update()
    {
        if (_gameReady && !_gameEnded)
        {
            BombTimer();
            List<PlayerController> list = GameManager.Instance.GetPlayerManager().GetActivePlayers();
            if (list.Count == 1)
            {
                if (_hasBomb.GetPlayerBombTag().HasPoint == false)
                {
                    _hasBomb.GetPlayerBombTag().HasPoint = true;
                    FinDeGame();
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
        if (_whoWin._isFinish)
        {
            GameManager.Instance.GetPartyManager().ChangeMiniGame();
            _whoWin._isFinish = false;
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
        if (_gameEnded) return;
        List<PlayerController> list = GameManager.Instance.GetPlayerManager().GetActivePlayers();
        
        foreach (PlayerController playerController in list)
        {
            _scoreManager.OneWin();
        }
        _whoWin.WinnerUI();
    }
    IEnumerator TimerForRule()
    {
        yield return new WaitForSeconds(_seeRuleCD);
        _rules.SetActive(false);
        _gameReady = true;
    }

}
