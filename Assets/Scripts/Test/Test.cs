using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Test : MonoBehaviour
{
    GameManager m_gm;
    PlayerManager m_pm;


    [SerializeField] List<GameObject> m_playersSpawns = new List<GameObject>();

    void Start()
    {
        m_gm = GameManager.Instance;
        m_pm = m_gm.GetPlayerManager();
        Debug.Log(m_gm.gameObject.name);
        InitPlayersPosition();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SceneManager.LoadScene(2);
        } 
    }

    public void InitPlayersPosition()
    {
        List<PlayerController> players = m_pm.GetPlayerList();
        foreach (PlayerController player in players) {
            player.WarpToPosition(m_playersSpawns[player.PlayerId].transform.position);
        }
    }
}
