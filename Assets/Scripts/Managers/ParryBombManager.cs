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
        m_parryBomb.OnExplode +=TimeOver;

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
                ScoreManager sm = GameManager.Instance.GetScoreManager();
                sm.OneWin();
                GameFinished($"P{m_players[0].PlayerId+1} WIN !");
            break;
        }
    }


    void TimeOver(PlayerController winner)
    {
        if(winner == null)
        {
            GameFinished("DRAW!");
            return;
        }
        GameFinished($"P{winner.PlayerId + 1} WIN !");
    }

    void GameFinished(string message)
    {
        FeedBackManager fbm = FeedBackManager.Instance;
        ScoreManager sm = GameManager.Instance.GetScoreManager();
        fbm.FreezeFrame(2f, 0.6f);
        Debug.Log(message);
        m_parryBomb.gameObject.SetActive(false);
        GameManager.Instance.GameFinished();
        //GameManager.Instance.GetPartyManager().ChangeMiniGame();
    }



}
