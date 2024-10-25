using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ParryBombManager : MonoBehaviour
{

    PlayerController m_winner;

    GameManager m_gameManager;
    List<PlayerController> m_players;

    ParryBomb m_parryBomb;


    public event Action OnPlayerEliminated;
    public event Action OnGameFinished;

    
    void Start()
    {
        m_gameManager = GameManager.Instance;
        m_players = m_gameManager.GetPlayerManager().GetActivePlayers();
        m_parryBomb = FindObjectOfType<ParryBomb>();
        m_parryBomb.OnPlayerTouched += HasAWinner;
    }

    void Initialize()
    {

    }

    void HasAWinner()
    {
        m_players = m_gameManager.GetPlayerManager().GetActivePlayers();
        switch(m_players.Count)
        {
            case 0:
                GameFinished("DRAW");
            break;
            case 1:
                GameFinished($"P{m_players[0].PlayerId+1} WIN !");
            break;

        }
    }

    void GameFinished(string message)
    {
        Debug.Log(message);
       
    }




}
