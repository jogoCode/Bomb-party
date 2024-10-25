using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    PlayerManager m_playerManager;
    


    void Awake()
    {
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
