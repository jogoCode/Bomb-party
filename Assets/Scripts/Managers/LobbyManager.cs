using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LobbyManager : MonoBehaviour
{
    PlayerManager m_playerManager;

    public event Action<bool> OnPlayerIsReady;

    [SerializeField]int  m_startTimer = 3;

    Coroutine m_startCoroutine;

    void Start()
    {
        m_playerManager = GameManager.Instance.GetPlayerManager();
        m_playerManager.OnPlayerInListIsReady += CanStartGame;
    }

   
    void Update()
    {
        
    }


    void CanStartGame()
    {
        var readyPlayer = m_playerManager.GetReadyPlayers();

        if (readyPlayer.Count == m_playerManager.GetActivePlayers().Count && readyPlayer.Count > 1)
        {
            m_startCoroutine = StartCoroutine(StartTimer());
        }
        else if (m_startCoroutine != null)
        {           
            StopCoroutine(m_startCoroutine);    
        }
    }

    IEnumerator StartTimer()
    {
        for (int i = 0; i < m_startTimer; ++i)
        {
            yield return new WaitForSeconds(1);       
        }
        //Destroy(m_playerManager.GetComponent<PlayerInputManager>());
        GameManager.Instance.GameStart();
    }
    
}
