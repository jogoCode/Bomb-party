using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    PlayerManager m_playerManager;
    CameraHandler m_camera;


    public const int PLAYER_PARRY_BOMB_LAYER = 8;
    public const int PLAYER_LAYER = 6;


    public static readonly Color BLUE = new Color32(68,176,243,1);
    public static readonly Color GREEN = new Color32(82,211,106,1);
    public static readonly Color YELLOW = new Color32(255,229,50,1); //TODO CHANGE THIS COLOR
    public static readonly Color PURPLE = new Color32(155,43,253,1);



    public Material PLAYER1;
    public Material PLAYER2;
    public Material PLAYER3;
    public Material PLAYER4;



    public event Action OnGameStarted;


    public void InitMaterials() //TODO Maybe deplace this in player manager
    {
        PLAYER1 = Resources.Load("Materials/PLAYERS/P1/Player1Material", typeof(Material)) as Material;
        PLAYER2 = Resources.Load("Materials/PLAYERS/P2/Player2Material", typeof(Material)) as Material;
        PLAYER3 = Resources.Load("Materials/PLAYERS/P3/Player3Material", typeof(Material)) as Material;
        PLAYER4 = Resources.Load("Materials/PLAYERS/P4/Player4Material", typeof(Material)) as Material;
    }


    void Awake()
    {
        Debug.Log(PLAYER_LAYER);
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
        m_camera = Camera.main.gameObject.GetComponent<CameraHandler>();
    }


    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.R)) {
            RestartGame();
        
        }
    }

    public void RestartGame()
    {
        m_playerManager.Restart();
        Destroy(gameObject);
        SceneManager.LoadScene(0);
    }


    public void GameStart()
    {
        //TODO faire une fonction pour le mini jeux 
        OnGameStarted?.Invoke();
        SceneManager.LoadScene(1);
    }


    public CameraHandler GetCameraHandler() => m_camera;
    public PlayerManager GetPlayerManager()=> m_playerManager;

}
