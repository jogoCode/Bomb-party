using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerManager : MonoBehaviour
{


    List<PlayerController> m_players = new List<PlayerController>();

 
    public void OnPlayerJoined(PlayerInput playerInput)
    {

        PlayerController newPlayer = playerInput.gameObject.GetComponent<PlayerController>();
        AddPlayerInPlayerList(newPlayer);
        Debug.Log("Nouveau joueur ajouté : " + playerInput.gameObject.name);
    }


    void AddPlayerInPlayerList(PlayerController newPlayer)
    {
        m_players.Add(newPlayer);
        newPlayer.SetPlayerID(m_players.IndexOf(newPlayer));
    }


    #region ACCESORS

    public List<PlayerController> GetPlayerList() => m_players;
    #endregion
}
