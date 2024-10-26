using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{

    PlayerMovement m_pm;
    PlayerStates m_actualState = PlayerStates.IDLE;

    public enum PlayerStates
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetState(PlayerStates newState)
    {
        m_actualState = newState;
    }
    public PlayerStates GetState() => m_actualState;

}

