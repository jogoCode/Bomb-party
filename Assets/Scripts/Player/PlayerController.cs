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
    PlayerStateManager m_playerStateManager;


    Vector2 m_inputDir = Vector2.zero;
    Vector2 m_lastInputDir = Vector2.zero;
    bool m_jumped = false;


    public event Action OnJustGrounded;
    public event Action OnParried;
    public event Action<float,float> OnMoved;
    public event Action OnJumped;
    public event Action OnDashed;




    public int PlayerId
    {
        get { return m_playerId;}
    }

    public PlayerVisual PlayerVisual
    {
        get { return m_playerVisual;}
    }

    #region INPUT EVENTS
    public void OnInputMove(InputAction.CallbackContext context)
    {
        m_inputDir = context.ReadValue<Vector2>();
        OnMoved?.Invoke(m_inputDir.magnitude,m_playerMovement.Speed);
        if(m_inputDir != Vector2.zero)
        {
            m_lastInputDir = new Vector2(m_inputDir.x, m_inputDir.y).normalized;
        }
    }

    public void OnInputJump(InputAction.CallbackContext context)
    {
        if (context.action.triggered ){
            m_playerMovement.ResetJumpBufferTimer();    
        }
    }

    public void OnInputAction(InputAction.CallbackContext context)
    {
        if (context.action.triggered) {
            if (m_playerParryBomb.isActiveAndEnabled)
            {
                OnParried?.Invoke();
            }
        }
    }

    public void OnInputDash(InputAction.CallbackContext context)
    {
        if (context.action.triggered)
        {
            OnDashed?.Invoke();
        }
    }

    #endregion

    void Awake()
    {
        m_playerMovement = GetComponent<PlayerMovement>();
        m_playerVisual = GetComponent<PlayerVisual>();
        m_playerParryBomb = GetComponentInChildren<PlayerParryBomb>();
        m_playerStateManager = GetComponentInChildren<PlayerStateManager>();

       

        OnDashed += m_playerMovement.Dash;
        OnJumped += m_playerMovement.Jump;

        OnJustGrounded += m_playerVisual.JustGrounded;
        OnParried += m_playerVisual.BatAnimation;
        OnMoved += m_playerVisual.MoveAnimation;


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

    public void SetLayer(int newLayer)
    {
        gameObject.layer = newLayer;
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


    public void EnabledPlayerParryBomb(bool state)
    {
        m_playerParryBomb.enabled = state;   
    }

 

    #region ACCESORS
    public PlayerVisual GetPlayerVisual() => m_playerVisual;

    public PlayerMovement GetPlayerMovement() => m_playerMovement;

    public PlayerStateManager GetPlayerStateManager() => m_playerStateManager;

    public Vector2 GetInputDir()=> m_inputDir;

    public Vector2 GetLastInputDir() => m_lastInputDir;

    public float GetVerticalVelY() => m_playerMovement.GetVerticalVelY();

    public bool GetJumped() => m_jumped;
    #endregion

}