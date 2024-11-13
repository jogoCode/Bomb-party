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

    private void Start()
    {
        m_gameManager = GameManager.Instance;
        _scoreManager = GameManager.Instance.GetScoreManager();
        _baseTime = _time;
        _isTimerRunning = true;
    }

    public void PlayerEliminated(PlayerController player)
    {
        _scoreManager.AddPlayerToList(player, _scoreManager.Bonus);
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
                    m_gameManager.GetPartyManager().ChangeMiniGame();
                }
            }
        }
    }
}
