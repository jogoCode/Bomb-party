using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
  
    [SerializeField] PlayerMovement m_playerMovement;
    Vector2 m_inputDir = Vector2.zero;
    bool m_jumped = false;

    public void OnMove(InputAction.CallbackContext context)
    {
        m_inputDir = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        m_jumped = context.action.triggered;
    }


    void Awake()
    {
        m_playerMovement = GetComponent<PlayerMovement>(); 
    }





    #region ACCESORS
    public Vector2 GetInputDir()=> m_inputDir;

    public bool GetJumped() => m_jumped;
    #endregion

}
