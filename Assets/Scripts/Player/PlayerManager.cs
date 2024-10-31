using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerManager : MonoBehaviour
{


    public const int MAX_PLAYER_COUNT = 4;



    List<PlayerController> m_players = new List<PlayerController>();
    PlayerInputManager m_playerInputManager;

    public event Action OnPlayerManagerStateChanged;
    public event Action OnPlayerInListIsReady;

    int m_playersCount = 0;

    [SerializeField] PlayerManagerState m_playerManagerState;
    public enum PlayerManagerState
    {
        DISABLE,
        SEARCHING
    }

    void Start()
    {
        m_playerInputManager = GetComponent<PlayerInputManager>();
        OnPlayerManagerStateChanged += PlayerManagerStateChanged;
       
        OnPlayerManagerStateChanged?.Invoke();
    }
   

    public void OnPlayerJoined(PlayerInput playerInput)
    {

        PlayerController newPlayer = playerInput.gameObject.GetComponent<PlayerController>();
        AddPlayerInPlayerList(newPlayer);
        m_playersCount++;
        Debug.Log("Nouveau joueur ajouté : " + playerInput.gameObject.name);
        if(m_playersCount == MAX_PLAYER_COUNT) // if player count equal max playercount change the player manager state
        {
            SetPlayerManagerState(PlayerManagerState.DISABLE);
        }
    }

    public void JoinAFakePlayer()
    {

    }

    public void Restart()
    {
        m_playersCount = 0;
        foreach (PlayerController player in m_players)
        {
            Destroy(player.gameObject);
        }
    }


    public void SetActivePlayerParryBomb(bool state)
    {
        foreach(PlayerController player in m_players)
        {
            player.EnabledPlayerParryBomb(state);
        }
    }



    void AddPlayerInPlayerList(PlayerController newPlayer)
    {
        m_players.Add(newPlayer);
        newPlayer.SetPlayerID(m_players.IndexOf(newPlayer));
    }


    public void SetPlayerManagerState(PlayerManagerState newState)
    {
        m_playerManagerState=newState;
        OnPlayerManagerStateChanged?.Invoke();
    }

    void PlayerManagerStateChanged()
    {
        switch (m_playerManagerState)
        {
            case PlayerManagerState.DISABLE:
                m_playerInputManager.DisableJoining();
            break;
            case PlayerManagerState.SEARCHING:
                m_playerInputManager.EnableJoining();
            break;
        }
    }

    public void PlayerInListIsReady(bool isReady)
    {
        OnPlayerInListIsReady?.Invoke();
    }

    public void ActiveAllPlayerController()
    {
        foreach (PlayerController player in m_players)
        {
            player.gameObject.SetActive(true);
        }
    }



    #region ACCESORS
    public List<PlayerController> GetReadyPlayers()
    {
        List<PlayerController> players = m_players;
        List<PlayerController> readyPlayer= new List<PlayerController>();
        foreach (PlayerController player in players)
        {
            if (player.IsReady)
            {
                readyPlayer.Add(player);
            }
        }
        return readyPlayer;
    }

    public List<PlayerController> GetPlayerList() => m_players;

    public List<PlayerController> GetActivePlayers()
    {
        List<PlayerController> list = new List<PlayerController>();
        foreach (PlayerController player in m_players)
        {
            if (player.gameObject.activeInHierarchy)
            {
                list.Add(player);
            }
        }
        return list;
    }
    #endregion
}
