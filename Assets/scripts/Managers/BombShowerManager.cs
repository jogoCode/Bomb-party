using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombShowerManager : MonoBehaviour
{
    public event Action OnPlayerEliminated;
    public event Action OnGameFinished;
    private ScoreManager _scoreManager;
    public GameManager m_gameManager;
    public BombShower _spawn;
    public float _time;
    float _baseTime;
    bool _isTimerRunning;
    FeedBackManager _fbM;
    bool _finish;
    [SerializeField] GameObject _rules;
    public bool _gameReady = false;
    [SerializeField] float _timeForRule;
    public WhoWin _whoWin;
    bool _isRestarted;

    private void Start()
    {
        _isRestarted = false;
        _whoWin = FindObjectOfType<WhoWin>();
        _spawn = FindObjectOfType<BombShower>();
        SoundManager.Instance.PlayMusic(SoundManager.Instance.m_musicClips[4]);
        _spawn.enabled = false;
        StartCoroutine(TimerForRule());
        //foreach (PlayerController i in GameManager.Instance.GetPlayerManager().GetPlayerList())
        //{
        //    i.gameObject.SetActive(true);
        //}
        _fbM = FeedBackManager.Instance;
        m_gameManager = GameManager.Instance;
        _scoreManager = m_gameManager.GetScoreManager();
        _baseTime = _time;
        _isTimerRunning = true;
        _finish = false;
    }

    public void PlayerEliminated(PlayerController player)
    {
        _scoreManager.AddPlayerToList(player, _scoreManager.Bonus);
        SoundManager.Instance.PlaySFX("Explosion");
        SoundManager.Instance.PlaySFX("Crowd");
        _fbM.InstantiateParticle(_fbM.m_explosionVfx, player.gameObject.transform.position, player.gameObject.transform.rotation);
        HasAWinner();
    }

    private void Update()
    {
        if (_gameReady)
        {
          UpdateTimer();
        }
        if (_whoWin._isFinish && !_isRestarted)
        {
            _isRestarted = true;
            _whoWin._isFinish = false;
            GameManager.Instance.GetPartyManager().ChangeMiniGame();
        }
    }
    void UpdateTimer()
    {
        List<PlayerController> list = GameManager.Instance.GetPlayerManager().GetActivePlayers();
        if (_isTimerRunning)
        {
            if (_time > 0)
            {
                _time = Mathf.Clamp(_time, 0, _baseTime) - Time.deltaTime; // diminue le titer de la bombe au fine du temps 
            }
            if (_time <= 0)
            {
                foreach (PlayerController player in list)
                {
                    _scoreManager.OneWin();
                    _time = _baseTime;
                    
                }
                
                _finish = true;
            }
        }
    }
    public void HasAWinner()
    {
        if (m_gameManager.GetPlayerManager().GetActivePlayers().Count == 1)
        {
            _time = 0;
            _spawn.enabled = false;
            _whoWin.WinnerUI();
        }
        if (_finish)
        {
            _spawn.enabled = false;
            _whoWin.WinnerUI();
        }
    }
    IEnumerator TimerForRule()
    {
        yield return new WaitForSeconds(_timeForRule);
        _spawn.enabled = true;
        _rules.SetActive(false);
        _gameReady = true;
    }
}
