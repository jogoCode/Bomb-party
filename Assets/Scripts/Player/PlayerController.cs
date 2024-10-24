using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    int m_playerId;

    [SerializeField] PlayerMovement m_playerMovement;
    [SerializeField] PlayerVisual m_playerVisual;
    Vector2 m_inputDir = Vector2.zero;
    bool m_jumped = false;
    public event Action OnJustGrounded;



    public int PlayerId
    {
        get { return m_playerId;}
    }

    public PlayerVisual PlayerVisual
    {
        get { return m_playerVisual;}
    }


    public void OnInputMove(InputAction.CallbackContext context)
    {
        m_inputDir = context.ReadValue<Vector2>();
    }

    public void OnInputJump(InputAction.CallbackContext context)
    {
        m_jumped = context.action.triggered;
    }


    void Awake()
    {
        m_playerMovement = GetComponent<PlayerMovement>();
        m_playerVisual = GetComponent<PlayerVisual>();
        OnJustGrounded += m_playerVisual.JustGrounded;

    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SceneManager.LoadScene(1);
        }
    }



    public void Jump()
    {
        if (m_playerMovement.CoyoteTimer > 0)
        {
            m_playerMovement.Jump();
        }
    }


    public void SetPlayerID(int newId)
    {
        m_playerId = newId;
    }


    Vector3 warpPosition = Vector3.zero;
    public void WarpToPosition(Vector3 newPosition)
    {
        m_playerMovement.GetCharacterController().enabled = false;
        warpPosition = newPosition;
        transform.position = warpPosition;
        m_playerMovement.GetCharacterController().enabled = true;
    }


    public void JustGrounded()
    {
        OnJustGrounded?.Invoke();
    }




    #region ACCESORS
    public Vector2 GetInputDir()=> m_inputDir;

    public float GetVerticalVelY() => m_playerMovement.GetVerticalVelY();

    public bool GetJumped() => m_jumped;
    #endregion

}
