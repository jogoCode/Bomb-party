using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParryBomb : MonoBehaviour
{
    
    PlayerController m_playerController;

    [SerializeField]BoxCollider m_boxCollider;



    void Start()
    {
        m_playerController = GetComponentInParent<PlayerController>();
        m_boxCollider = GetComponent<BoxCollider>();   
    }


    public void Parry()
    {
        Debug.Log("BAT");
        if (!m_playerController.GetPlayerVisual().isActiveAndEnabled) return;
        StartCoroutine(ResetBoxCollider());
    }



    private void OnTriggerEnter(Collider other)
    {
        //m_boxCollider.enabled = false;
        FeedBackManager fbm = FeedBackManager.Instance;
        Vector3 inputDir = new Vector3(m_playerController.GetLastInputDir().x, 0, m_playerController.GetLastInputDir().y);
        ParryBomb bomb = other.GetComponent<ParryBomb>();
        if (bomb == null) return;
        //if (bomb.Owner == m_playerController) return;
        bomb.Parry(inputDir,m_playerController);
        fbm.FreezeFrame(0.06f,0.5f);
        
    }


    IEnumerator ResetBoxCollider()
    {
        yield return new WaitForSeconds(0.1f);
        m_boxCollider.enabled = false;
    }
}
