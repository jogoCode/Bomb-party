using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParryBomb : MonoBehaviour
{
    
    PlayerController m_playerController;

    BoxCollider m_boxCollider;



    void Start()
    {
        m_playerController = GetComponentInParent<PlayerController>();
        m_boxCollider = GetComponent<BoxCollider>();   
    }


    public void Parry()
    {
        Debug.Log("BAT");
        m_boxCollider.enabled = true;
        StartCoroutine(ResetBoxCollider());
    }



    private void OnTriggerEnter(Collider other)
    {
        Vector3 inputDir = new Vector3(m_playerController.GetLastInputDir().x, 0, m_playerController.GetLastInputDir().y);
        ParryBomb bomb = other.GetComponent<ParryBomb>();

        if (bomb.Owner == m_playerController) return;
        // Set the owner of the bomb
        bomb.SetOwner(m_playerController);
        bomb.Parry(inputDir,m_playerController);
        

    }


    IEnumerator ResetBoxCollider()
    {
        yield return new WaitForSeconds(0.5f);
        m_boxCollider.enabled = false;
    }
}