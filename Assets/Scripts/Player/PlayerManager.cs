using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerManager : MonoBehaviour
{


    public const int MAX_PLAYER_COUNT = 3;



    List<PlayerController> m_players = new List<PlayerController>();
    PlayerInputManager m_playerInputManager;
    public GameObject[] m_playerSpawnerPoints;

    Coroutine m_spawnDelay;

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

        if (m_playerManagerState == PlayerManagerState.DISABLE) return;// if player count equal max playercount change the player manager state
        PlayerController newPlayer = playerInput.gameObject.GetComponent<PlayerController>();
        newPlayer.m_device = playerInput.devices[0];
        newPlayer.m_playerInput = playerInput;
        AddPlayerInPlayerList(newPlayer);
        Debug.Log(newPlayer.m_device);
        m_spawnDelay = StartCoroutine(DelaySpawnPlayer(newPlayer));
        //Debug.Log(m_playerSpawnerPoints[m_playersCount].name);
        m_playersCount++;
        //Debug.Log("Nouveau joueur ajouté : " + playerInput.gameObject.name);
        if (m_playersCount == MAX_PLAYER_COUNT) // if player count equal max playercount change the player manager state
        {
            SetPlayerManagerState(PlayerManagerState.DISABLE);
        }
    }



    IEnumerator DelaySpawnPlayer(PlayerController newPlayer)
    {
        yield return new WaitForSeconds(0.0f);
        newPlayer.WarpToPosition(m_playerSpawnerPoints[m_playersCount-1].transform.position);
        
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
        PlayerManagerStateChanged();
        //OnPlayerManagerStateChanged?.Invoke();
    }

    void PlayerManagerStateChanged()
    {
        if (m_playerInputManager == null) return;    
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
        foreach (PlayerController player in m_players) {
            player.gameObject.SetActive(true);
            player.m_playerInput.SwitchCurrentControlScheme(player.m_device);
        }
    }

    public void SetPlayersSpeed(float speed)
    {
        foreach (PlayerController player in m_players)
        {
            player.GetPlayerMovement().SetPlayerSpeed(speed);
        }
    }

    public void SetPlayersJumpForce(float jumpForce)
    {
        foreach (PlayerController player in m_players)
        {
            player.GetPlayerMovement().SetJumpForce(jumpForce);
        }
    }

    public void SetPlayersDashCoolDown(float dashCoolDown)
    {
        foreach (PlayerController player in m_players)
        {
            player.GetPlayerMovement().SetDashCooldown(dashCoolDown);
        }
    }

    public void  SetPlayersDashSpeed(float dashSpeed)
    {
        foreach (PlayerController player in m_players)
        {
            player.GetPlayerMovement().SetDashSpeed(dashSpeed);
        }
    }

    public void ActiveBatModelPlayers()
    {
        foreach (PlayerController player in m_players)
        {
            player.GetPlayerVisual().ActiveBatModel();
        }
    }
    public void DesactiveBatModelPlayers()
    {
        foreach (PlayerController player in m_players)
        {
            player.GetPlayerVisual().DesactiveBatModel();
        }
    }
    public void DesactiveBombTagModel()
    {
        foreach (PlayerController player in m_players)
        {
            player.GetPlayerBombTag().Bomb.GetComponent<Renderer>().enabled = false;

            if (player.GetPlayerBombTag().Light == null) return;
            if (player.GetPlayerBombTag().Fx1 == null) return;
            if (player.GetPlayerBombTag().Fx2 == null) return;
            player.GetPlayerBombTag().Light.GetComponent<Light>().enabled = false;
            player.GetPlayerBombTag().Fx1.GetComponent<ParticleSystem>().Stop();
            player.GetPlayerBombTag().Fx2.GetComponent<ParticleSystem>().Stop();
        }
    }
    public void ActiveBombTagModel()
    {
        foreach (PlayerController player in m_players)
        {
            player.GetPlayerBombTag().Bomb.GetComponent<Renderer>().enabled = true;
        }
    }


    public void EnabledPlayersParryBomb()
    {
        foreach (PlayerController player in m_players)
        {
            player.EnabledPlayerParryBomb(true);
        }
    }

    public void EnablePlayersBombTag()
    {
        foreach (PlayerController player in m_players)
        {
            player.EnabledPlayerBombTag(true);
        }
    }
    public void DisablePlayersBombTag()
    {
        foreach (PlayerController player in m_players)
        {
            player.EnabledPlayerBombTag(true);
        }
    }


    public void DisabledPlayersParryBomb()
    {
        foreach (PlayerController player in m_players)
        {
            player.EnabledPlayerParryBomb(false);
        }
    }

    public void ResetPlayersMovementValues()
    {
        foreach (PlayerController player in m_players)
        {
            player.GetPlayerMovement().ResetMovementValues();
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
    public List<PlayerController> GetInactivePlayers()
    {
        List<PlayerController> list = new List<PlayerController>();
        foreach (PlayerController player in m_players)
        {
            if (!player.gameObject.activeInHierarchy)
            {
                list.Add(player);
            }
        }
        return list;
    }
    #endregion
}
