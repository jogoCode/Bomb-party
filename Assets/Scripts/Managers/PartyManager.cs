using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PartyManager : MonoBehaviour
{
    GameManager m_gameManager;
    public const int MAX_MINI_GAME = 4;
    [SerializeField] List<int> m_mapList;
    [SerializeField] int m_lastMiniGame;

    //0 VolleyBomb
    //1 ParryBomb
    //2 BombTag
    //3 BombShower


    void Start()
    {
        InitMapList();
        m_gameManager = GameManager.Instance;
    }

    void InitMapList()
    {
        m_mapList.Clear();
        for (int i = 0; i < MAX_MINI_GAME; i++)
        {
            m_mapList.Add(i + 1);
        }
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            ChangeMiniGame();
        }
    }

    public void ChangeMiniGame()
    {
        int rng = Random.Range(1,MAX_MINI_GAME);
        if (AllMapsWasUsed())
        {
            InitMapList();
            ChangeMiniGame();
            return;
        }
        while(rng == m_lastMiniGame || m_mapList[rng] == -1)
        {
            rng = Random.Range(1, MAX_MINI_GAME);
        }
    
        m_lastMiniGame = rng;
        if (m_mapList[rng] != -1)
        {
            GameManager.Instance.LoadScene(rng);
            m_mapList[rng] = -1;
        }

    }

    bool AllMapsWasUsed()
    {
        var count = 0;
        foreach (int i in m_mapList) { 
            if(i == -1)
            {
                count++;
            }     
        }
        Debug.Log("COUNT "+count);
        if (count == m_mapList.Count-1)
        {
            return true;
        }else{
            return false;
        }
    }

}
