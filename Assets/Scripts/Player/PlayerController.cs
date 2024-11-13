using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerController : MonoBehaviour
{
    int m_playerId;
    bool m_isReady = false;
    bool m_inCage = false;

    [SerializeField] PlayerMovement m_playerMovement;
    [SerializeField] PlayerVisual m_playerVisual;
    [SerializeField] PlayerParryBomb m_playerParryBomb;
    [SerializeField] PlayerBombTag m_playerBombTag;
    [SerializeField] PlayerVolleyBomb m_playerVolleyBomb;
    PlayerStateManager m_playerStateManager;

    public int _score;
    public string m_nom;

    Vector2 m_inputDir = Vector2.zero;
    Vector2 m_lastInputDir = Vector2.zero;
    bool m_jumped = false;


    public event Action OnJustGrounded;
    public event Action OnParried;
    public event Action<float,float> OnMoved;
    public event Action OnJumped;
    public event Action OnDashed;
    public event Action OnHit;
    public event Action<bool> OnReady;


    public PlayerController(string name, int score)
    {
        m_nom = name;
        _score = score;
    }


    public bool InCage
    {
        get { return m_inCage; }
        set { m_inCage = value; }
    }
    public bool IsReady
    {
        get {return m_isReady;}
    }
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
                if (m_inCage) return;
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

    public void OnInputReady(InputAction.CallbackContext context)
    {
        if (context.action.triggered)
        {
            m_isReady = !m_isReady;
            OnReady?.Invoke(m_isReady);
        }
    }

    #endregion

    void Awake()
    {
        m_playerMovement = GetComponent<PlayerMovement>();
        m_playerVisual = GetComponent<PlayerVisual>();
        m_playerParryBomb = GetComponentInChildren<PlayerParryBomb>();
        m_playerStateManager = GetComponentInChildren<PlayerStateManager>();
        m_playerBombTag = GetComponentInChildren<PlayerBombTag>();
        m_playerVolleyBomb = GetComponent<PlayerVolleyBomb>();


        OnDashed += m_playerMovement.Dash;
        OnJumped += m_playerMovement.Jump;
      
       

        OnJustGrounded += m_playerVisual.JustGrounded;
        OnParried += m_playerVisual.BatAnimation;
        OnMoved += m_playerVisual.MoveAnimation;
        OnHit += m_playerVisual.HitAnimation;

        OnReady += GameManager.Instance.GetPlayerManager().PlayerInListIsReady;


    }

    public void Hit()
    {
        OnHit?.Invoke();
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
        if (m_playerMovement.GetCharacterController() == null) return;
        m_playerMovement.ResetVerticalVel();
        m_playerVisual.Model.SetActive(false);
        m_playerMovement.GetCharacterController().enabled = false;
        warpPosition = newPosition;
        transform.position = warpPosition;
        m_playerMovement.GetCharacterController().enabled = true;
        m_playerVisual.Model.SetActive(true);
    }

    public void JustGrounded()
    {
        OnJustGrounded?.Invoke();
    }


    public void EnabledPlayerParryBomb(bool state)
    {
        m_playerParryBomb.enabled = state;
        if (state == false) {
            m_playerVisual.DesactiveBatModel();
        }
    }

 
    public void ApplyImpulse(Vector3 direction, float impulseFoce)
    {
        m_playerMovement.ApplyImpulse(direction, impulseFoce);
    }

    public void StartOscillator(float impulse)
    {
        m_playerVisual.Oscillator.StartOscillator(impulse);
    }



    #region ACCESORS
    public PlayerVisual GetPlayerVisual() => m_playerVisual;

    public PlayerBombTag GetPlayerBombTag() => m_playerBombTag;

    public PlayerMovement GetPlayerMovement() => m_playerMovement;

    public PlayerStateManager GetPlayerStateManager() => m_playerStateManager;

    public PlayerVolleyBomb GetPlayerVolleyBomb() => m_playerVolleyBomb;

    public Vector2 GetInputDir()=> m_inputDir;

    public Vector2 GetLastInputDir() => m_lastInputDir;

    public float GetVerticalVelY() => m_playerMovement.GetVerticalVelY();

    public bool GetJumped() => m_jumped;
    #endregion

}
