using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ParryBombManager : MonoBehaviour
{

    PlayerController m_winner;

    [SerializeField]GameObject m_rules;
    GameManager m_gameManager;
    List<PlayerController> m_players;

    ParryBomb m_parryBomb;

    public WhoWin m_whoWin;

    public event Action OnPlayerEliminated;
    public event Action OnGameFinished;

    
    void Start()
    {
        m_gameManager = GameManager.Instance;
        m_players = m_gameManager.GetPlayerManager().GetActivePlayers();
        m_parryBomb = FindObjectOfType<ParryBomb>();
        m_parryBomb.OnPlayerTouched += HasAWinner;
        m_parryBomb.OnExplode +=TimeOver;
        m_parryBomb.gameObject.SetActive(false);
        m_whoWin = FindObjectOfType<WhoWin>();
        StartCoroutine(StartTimer());
        SoundManager.Instance.PlayMusic(SoundManager.Instance.m_musicClips[2]);

    }


    private void Update()
    {
        if (m_whoWin.IsFinish)
        {
            GameManager.Instance.GetPartyManager().ChangeMiniGame();
            m_whoWin.IsFinish = false;
        }
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
        SoundManager.Instance.PlayMusic(SoundManager.Instance.m_musicClips[2]);
        fbm.FreezeFrame(2f, 0.6f);
        Debug.Log(message);
        m_parryBomb.gameObject.SetActive(false);
        m_whoWin.WinnerUI();
        //GameManager.Instance.GameFinished();
    }

    IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(5);
        m_rules.SetActive(false);
        m_parryBomb.gameObject.SetActive(true);
        m_parryBomb.StartParryBomb();
    }



}
