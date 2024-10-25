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
    [SerializeField] PlayerParryBomb m_playerParryBomb;


    Vector2 m_inputDir = Vector2.zero;
    Vector2 m_lastInputDir = Vector2.zero;
    bool m_jumped = false;


    public event Action OnJustGrounded;
    public event Action OnParry;



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
        if(m_inputDir != Vector2.zero)
        {
            m_lastInputDir = m_inputDir;
        }
    }

    public void OnInputJump(InputAction.CallbackContext context)
    {
        m_jumped = context.action.triggered;
    }

    public void OnAction(InputAction.CallbackContext context)
    {
        if (context.action.triggered) {

            OnParry?.Invoke();  
        }
    }


    void Awake()
    {
        m_playerMovement = GetComponent<PlayerMovement>();
        m_playerVisual = GetComponent<PlayerVisual>();
        m_playerParryBomb = GetComponentInChildren<PlayerParryBomb>();
        OnJustGrounded += m_playerVisual.JustGrounded;
    }


    private void Start()
    {
        OnParry += m_playerParryBomb.Parry;
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



    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.name.Contains("ParryBomb"))
        {
            gameObject.SetActive(false);
        }
    }


    #region ACCESORS
    public Vector2 GetInputDir()=> m_inputDir;

    public Vector2 GetLastInputDir() => m_lastInputDir;

    public float GetVerticalVelY() => m_playerMovement.GetVerticalVelY();

    public bool GetJumped() => m_jumped;
    #endregion

}
