using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{

    PlayerMovement m_pm;

    enum PlayerStates
    {
        IDLE,
        MOVE,
        JUMP,
        FALL,
        ATK
    }


    void Start()
    {
        m_pm = GetComponent<PlayerMovement>();
        if( m_pm == null)
        {
            Debug.LogError("No player movement component");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
