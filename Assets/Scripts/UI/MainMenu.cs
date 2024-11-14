using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    GameManager m_gm;
    [SerializeField] GameObject m_titlePanel;
    [SerializeField] GameObject m_playerSelectPanel;

    void Start()
    {
        m_gm = GameManager.Instance;
        if(m_titlePanel == null){
            Debug.LogError("no title panel");
        }
        m_playerSelectPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (m_titlePanel.activeInHierarchy)
            {
                StartCoroutine(SelectScreenCoroutine());
            }
            m_titlePanel.SetActive(false);   
        }
    }

    IEnumerator SelectScreenCoroutine()
    {
        m_playerSelectPanel.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        m_gm.GetPlayerManager().SetPlayerManagerState(PlayerManager.PlayerManagerState.SEARCHING);
    }
}
