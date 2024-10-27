using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerManager : MonoBehaviour
{


    public const int MAX_PLAYER_COUNT = 4;

    List<PlayerController> m_players = new List<PlayerController>();
    PlayerInputManager m_playerInputManager;

    public event Action OnPlayerManagerStateChanged;

    int m_playersCount = 0;

    [SerializeField] PlayerManagerState m_playerManagerState;
    enum PlayerManagerState
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
        if(m_playersCount == MAX_PLAYER_COUNT) // if player count equal max player count change player manager state
        {
            SetPlayerManagerState(PlayerManagerState.DISABLE);
        }
    }




    void AddPlayerInPlayerList(PlayerController newPlayer)
    {
        m_players.Add(newPlayer);
        newPlayer.SetPlayerID(m_players.IndexOf(newPlayer));
    }


    void SetPlayerManagerState(PlayerManagerState newState)
    {
        m_playerManagerState=newState;
        OnPlayerManagerStateChanged?.Invoke();
    }

    void PlayerManagerStateChanged()
    {
        switch (m_playerManagerState)
        {
            case PlayerManagerState.DISABLE:
                m_playerInputManager.enabled=false; 
            break;
            case PlayerManagerState.SEARCHING:
                m_playerInputManager.enabled=true;
            break;
        }
    }

    public void ActiveAllPlayerController()
    {
        foreach (PlayerController player in m_players)
        {
            player.gameObject.SetActive(true);
        }
    }







    #region ACCESORS
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
