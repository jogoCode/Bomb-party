using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

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
        ScoreManager sm = GameManager.Instance.GetScoreManager();
        sm.ChangeGame();
        if (AllMapsWasUsed())
        {
            InitMapList();
            //TODO FONCTION POUR DRAW
            GameManager.Instance.LoadScene(5);
            //ChangeMiniGame();
            return;
        }
       
        //while(rng == m_lastMiniGame || m_mapList[rng] == -1)
        //{
        //    if(m_mapList[rng] == -1)
        //    {
        //        count++;
        //    }
        //    rng = Random.Range(1, MAX_MINI_GAME);
        //}
        rng = Random.Range(0, m_mapList.Count-1);
   

        m_lastMiniGame = rng;
        if (m_mapList[rng] != -1)
        {
            GameManager.Instance.LoadScene(m_mapList[rng]);
            m_mapList.RemoveAt(rng);
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
        if (count == m_mapList.Count)
        {
            return true;
        }else{
            return false;
        }
    }

    void CheckForFinalist()
    {
        List<PlayerController> players = GameManager.Instance.GetPlayerManager().GetPlayerList();
        players.Sort((p1, p2) => p2._score.CompareTo(p1._score));
        var duplicateScores = players
         .GroupBy(player => player._score)
         .Where(group => group.Count() > 1)
         .Select(group => group.Key)
         .ToList();
    }

}
