using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    PlayerManager m_playerManager;
    CameraHandler m_camera;
    ScoreManager m_scoreManager;
    PartyManager m_partyManager;
    Animator m_sceneTransition;


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
        Debug.Log("sucepute.fr");
    }


    void Awake()
    {
        //Debug.Log(PLAYER_LAYER);
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
        m_scoreManager = GetComponent<ScoreManager>();
        m_partyManager = GetComponent<PartyManager>();
        m_sceneTransition = GetComponentInChildren<Animator>();
    }


    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.R)) {
            RestartGame();
        }
    }


    public void RestartGame()
    {
        Time.timeScale = 1.0f;
     
        LoadScene(0);
    }


    public void GameStart()
    {
     
        m_playerManager.SetPlayerManagerState(PlayerManager.PlayerManagerState.DISABLE);
        OnGameStarted?.Invoke();
        LoadScene(1);
        //m_partyManager.ChangeMiniGame();
    }

    public void GameFinished()
    {
        OnGameStarted?.Invoke();
        LoadScene(5);
    }

    public void LoadScene(int scene)
    {
        StartCoroutine(LoadSceneCoroutine(scene));
    }


    IEnumerator LoadSceneCoroutine(int scene)
    {
        m_sceneTransition.SetTrigger("End");
        yield return new WaitForSeconds(1f);
        m_sceneTransition.SetTrigger("Start");
        SceneManager.LoadScene(scene);
        if (scene == 0)
        {
            m_playerManager.Restart();
            foreach (Component component in GetComponents<MonoBehaviour>())
            {
                Destroy(component);
                SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetActiveScene());
            }
            Destroy(SoundManager.Instance.gameObject);
        };
        m_playerManager.ActiveAllPlayerController();
   
    }


    public CameraHandler GetCameraHandler() => m_camera;
    public PlayerManager GetPlayerManager() => m_playerManager;
    public ScoreManager GetScoreManager() => m_scoreManager;
    public PartyManager GetPartyManager() => m_partyManager;


}
