using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class BombTagManager : MonoBehaviour
{

    [SerializeField] float _baseTimer = 30;
    [SerializeField] float _bombTimer = 30;
    [SerializeField] bool _boom = false;
    [SerializeField] List<PlayerController> _players;
    [SerializeField] float  _baseSpeed;
    [SerializeField] ScoreManager _scoreManager;
    [SerializeField] PartyPlayerParameters _playerParameters;
    [SerializeField] PlayerBombTag _playerBombTag;
    [SerializeField] PlayerController _playerhasBomb;
    [SerializeField] GameObject _rules;
    [SerializeField] bool _gameReady = false;
    [SerializeField] float _seeRuleCD;

    FeedBackManager _fBM;
    WhoWin _whoWin;
    private bool _gameEnded = false;
    public bool GameReady { get { return _gameReady; } }
    public float BombTime { get { return _bombTimer; } }

    public PlayerController HasBomb { get { return _playerhasBomb; } set { ;} }

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
        _whoWin.IsFinish = false;
        SoundManager.Instance.PlayMusic(SoundManager.Instance.m_musicClips[3]);
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
                if (_playerhasBomb.GetPlayerBombTag().HasPoint == false)
                {
                    _playerhasBomb.GetPlayerBombTag().HasPoint = true;
                    FinDeGame();
                }
            }
            if (_boom)
            {
                SoundManager.Instance.PlaySFX("Explosion");
                SoundManager.Instance.PlaySFX("Crowd");
                if (_playerhasBomb.GetPlayerBombTag().HasPoint == false)
                {
                    _scoreManager.AddPlayerToList(_playerhasBomb, _scoreManager.Bonus);
                    _fBM.InstantiateParticle(_fBM.m_explosionVfx, _playerhasBomb.gameObject.transform.position, _playerhasBomb.gameObject.transform.rotation);
                    _playerhasBomb.gameObject.SetActive(false);
                    _playerhasBomb.GetPlayerBombTag().HasPoint = true;
                }
                if (list.Count >= 2)
                {
                    // TODO : si la fine gagne des point return;
                    AssignRandomBomb();
                    
                }
                _boom = false;
            }
        }
        if (_whoWin.IsFinish)
        {
            GameManager.Instance.GetPartyManager().ChangeMiniGame();
            _whoWin.IsFinish = false;
        }
    }

    public void AssignRandomBomb()
    {
        _bombTimer = _baseTimer;
        _players = GameManager.Instance.GetPlayerManager().GetActivePlayers();
        // Réinitialiser tous les hasBomb à false
        foreach (PlayerController player in _players)
        {
            player.GetPlayerBombTag().HasABomb = false;
        }

        // Choisir un joueur au hasard pour lui donner la bombe
        int randomIndex = UnityEngine.Random.Range(0, _players.Count);
        _players[randomIndex].GetPlayerBombTag().HasABomb = true;
        _playerhasBomb = _players[randomIndex];
        _playerhasBomb.GetPlayerMovement().SetPlayerSpeed(_baseSpeed + _playerhasBomb.GetPlayerBombTag().HasBombSpeed);

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
