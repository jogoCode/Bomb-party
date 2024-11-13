using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreenManager : MonoBehaviour
{
    GameManager m_gm;
    [SerializeField] float m_timeToRestart =10;
    [SerializeField] List<PlayerController> m_clasement;
    [SerializeField] GameObject[] m_spawnPoints;

    void Awake()
    {
        m_gm = GameManager.Instance;
    }

    void Start()
    {
        InitClassement();
        PlacePlayer();
        StartCoroutine(TimerToRestart());
    }

    void InitClassement()
    {
        List<PlayerController> players = GameManager.Instance.GetPlayerManager().GetPlayerList();
        players.Sort((p1, p2) => p2._score.CompareTo(p1._score));
        m_clasement = players;
    }

    void PlacePlayer()
    {
        for (int i = 0; i < m_clasement.Count; i++){
            m_clasement[i].WarpToPosition(m_spawnPoints[i].transform.position);
            m_clasement[i].GetPlayerVisual().Model.transform.LookAt(m_clasement[i].GetPlayerVisual().Model.transform.position - Vector3.forward);
        }
        //For the last
        m_clasement[m_clasement.Count-1].WarpToPosition(m_spawnPoints[3].transform.position);
        m_clasement[m_clasement.Count - 1].InCage = true;
        m_clasement[m_clasement.Count - 1].EnabledPlayerParryBomb(false);
    }

    IEnumerator TimerToRestart()
    {
        yield return new WaitForSeconds(m_timeToRestart);
        GameManager.Instance.RestartGame();
    }
}
