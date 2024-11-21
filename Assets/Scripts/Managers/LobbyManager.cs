using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class LobbyManager : MonoBehaviour
{
    PlayerManager m_playerManager;

    public event Action<bool> OnPlayerIsReady;

    [SerializeField] int  m_startTimer = 3;
    [SerializeField] ParticleSystem m_lobbyBombFx;
    [SerializeField] TextMeshPro m_lobbyBombTimer;

    Coroutine m_startCoroutine;

    void Start()
    {
        m_lobbyBombTimer.text = "";
        m_playerManager = GameManager.Instance.GetPlayerManager();
        m_playerManager.OnPlayerInListIsReady += CanStartGame;
        SoundManager.Instance.PlayMusic(SoundManager.Instance.m_musicClips[0]);     
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
            m_lobbyBombFx.gameObject.SetActive(true);
        }
        else if (m_startCoroutine != null)
        {           
            StopCoroutine(m_startCoroutine);
            m_lobbyBombTimer.text = "";
            m_lobbyBombFx.gameObject.SetActive(false);
        }
    }

    IEnumerator StartTimer()
    {
        int timer = m_startTimer+1;
        for (int i = 0; i < m_startTimer; i++)
        {
            timer--;
            m_lobbyBombTimer.text = timer.ToString()+"";
            yield return new WaitForSeconds(1);       
        }
        GameObject camera = Camera.main.gameObject;
        FeedBackManager.Instance.InstantiateParticle(FeedBackManager.Instance.m_explosionVfx,camera.transform.position+new Vector3(0.1f,0,0.5f),transform.rotation);
        FeedBackManager.Instance.InstantiateParticle(FeedBackManager.Instance.m_explosionVfx, camera.transform.position + new Vector3(-0.1f, 0, 0.5f), transform.rotation);
        FeedBackManager.Instance.InstantiateParticle(FeedBackManager.Instance.m_explosionVfx, camera.transform.position + new Vector3(0f, 0, 0.5f), transform.rotation);
        SoundManager.Instance.PlaySFX("Explosion");
        //Destroy(m_playerManager.GetComponent<PlayerInputManager>());
        GameManager.Instance.GameStart();
    }
    
}
