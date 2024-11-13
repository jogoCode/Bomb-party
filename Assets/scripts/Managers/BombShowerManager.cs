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

    private void Start()
    {
        m_gameManager = GameManager.Instance;
        _scoreManager = GameManager.Instance.GetScoreManager();
    }

    public void PlayerEliminated(PlayerController player)
    {
        _scoreManager.AddPlayerToList(player, _scoreManager.Bonus);
    }
}
