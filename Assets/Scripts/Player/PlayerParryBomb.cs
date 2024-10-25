using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParryBomb : MonoBehaviour
{
    
    PlayerController m_playeController;

    BoxCollider m_boxCollider;



    void Start()
    {
        m_playeController = GetComponentInParent<PlayerController>();
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
        Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
        Vector3 inputDir = new Vector3(m_playeController.GetLastInputDir().x, 0, m_playeController.GetLastInputDir().y);
        Vector3 oldVel = rb.velocity;
        rb.velocity = Vector3.zero;
        if (oldVel != Vector3.zero)
        {
            rb.AddForce(inputDir * 1.5f * oldVel.magnitude, ForceMode.Impulse); //TODO replace this hard value
        }
        else
        {
            rb.AddForce(inputDir * 15, ForceMode.Impulse);
        }
    }


    IEnumerator ResetBoxCollider()
    {
        yield return new WaitForSeconds(0.5f);
        m_boxCollider.enabled = false;
    }
}
