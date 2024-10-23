using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerMovement m_playerMovement;



    void Start()
    {
        m_playerMovement = GetComponent<PlayerMovement>(); 
    }

  
}
