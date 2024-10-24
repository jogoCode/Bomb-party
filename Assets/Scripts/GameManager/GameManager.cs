using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    PlayerManager m_playerManager;

    public Material PLAYER1;
    public Material PLAYER2;
    public Material PLAYER3;
    public Material PLAYER4;



    public void InitMaterials() //TODO Maybe deplace this in player manager
    {
        PLAYER1 = Resources.Load("Materials/PLAYERS/P1/Player1Material", typeof(Material)) as Material;
        PLAYER2 = Resources.Load("Materials/PLAYERS/P2/Player2Material", typeof(Material)) as Material;
        PLAYER3 = Resources.Load("Materials/PLAYERS/P3/Player3Material", typeof(Material)) as Material;
        PLAYER4 = Resources.Load("Materials/PLAYERS/P4/Player4Material", typeof(Material)) as Material;
        Debug.Log("suce");
    }


    void Awake()
    {
        InitMaterials();
        if(Instance != null)
        {
            Debug.LogError("Plus d'une instance game manager dans la scene");
            Destroy(gameObject);
            return;
        }
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        m_playerManager = GetComponent<PlayerManager>();
    }


    public PlayerManager GetPlayerManager()=> m_playerManager;

}
