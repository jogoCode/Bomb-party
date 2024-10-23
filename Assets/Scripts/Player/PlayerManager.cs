using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerManager : MonoBehaviour
{


    List<GameObject> m_players = new List<GameObject>();

  

    public void OnPlayerJoined(PlayerInput playerInput)
    {
       
        m_players.Add(playerInput.gameObject);

        // Affiche un message de confirmation dans la console
        Debug.Log("Nouveau joueur ajouté : " + playerInput.gameObject.name);
    }
}
