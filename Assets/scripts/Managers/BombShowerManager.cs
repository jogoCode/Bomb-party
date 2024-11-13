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
    public float _time;
    float _baseTime;
    bool _isTimerRunning;
    FeedBackManager _fbM;
    bool _finish;

    private void Start()
    {
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
        _fbM.InstantiateParticle(_fbM.m_explosionVfx, player.gameObject.transform.position, player.gameObject.transform.rotation);
        HasAWinner();
    }

    private void Update()
    {
        UpdateTimer();
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
        if (m_gameManager.GetPlayerManager().GetActivePlayers().Count <= 1)
        {
            _time = 0;
            m_gameManager.GetPartyManager().ChangeMiniGame();
        }
        if (_finish)
        {
            _finish = false;
        }
    }
}
