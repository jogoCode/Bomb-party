using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombShowerManager : MonoBehaviour
{

    public event Action OnPlayerEliminated;
    public event Action OnGameFinished;
    public GameManager m_gameManager;

    private void Start()
    {
        m_gameManager = GameManager.Instance;

    }

    public void PlayerEliminated(PlayerController player)
    {
        ScoreManager scoreManager = m_gameManager.GetScoreManager();
        scoreManager.AddPlayerToList(player);
    }
}
