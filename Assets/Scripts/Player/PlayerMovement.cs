using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{

    PlayerController m_playerController;
    CharacterController m_characterController;

    [SerializeField] float JUMP_FORCE = 10000;
    [SerializeField] float GRAVITY = 9.81f;
    [SerializeField] float SPEED = 4;
    [SerializeField] float COYOTE_TIME = 0.1f;
    [SerializeField] float JUMP_BUFFER_TIME = 0.1f;



    float m_coyoteTimer;
    float m_jumpBufferTimer =0;


    Vector3 m_vVel;

    [SerializeField] float m_vSpeed = 0;
    [SerializeField] float m_vVelFactor = 4f;


    bool m_canJump = true;
    bool m_wasGrounded;



  

    public float CoyoteTimer
    {
        get { return m_coyoteTimer; }
    }

    public float Speed
    {
        get { return SPEED; } 
    }


    #region BUILT-IN
    void Awake()
    {
        m_playerController = GetComponent<PlayerController>();
        m_characterController = GetComponent<CharacterController>();
    }


    void Update()
    {
        PlayerController pc = m_playerController;
        Vector2 inputDir = m_playerController.GetInputDir();
        bool jumped = m_playerController.GetJumped();
        Vector3 dir = new Vector3(inputDir.x,m_vVel.y, inputDir.y);

        bool isGrounded =  m_characterController.isGrounded;
        pc.PlayerVisual.CheckGrounded(m_characterController.isGrounded);

        if(!m_wasGrounded && isGrounded) {

            pc.JustGrounded();
            

        }
        m_wasGrounded = isGrounded;


        if (!m_characterController.isGrounded){      
            gravity();
            m_coyoteTimer -= Time.deltaTime;
        }else{
            m_vVel.y = 0;
            m_vSpeed = 0f;
            ResetCoyoteTimer();
        }



        JumpBufferLogic();
        Movement(dir, SPEED);       
    }

    #endregion

    void gravity()
    {
        m_vSpeed += m_vVelFactor * Time.deltaTime;
        m_vVel += Vector3.down * m_vSpeed * GRAVITY * Time.deltaTime;
        m_characterController.Move(m_vVel*Time.deltaTime);
    }

    void Movement(Vector3 direction, float speed)
    {
        m_characterController.Move(direction*speed*Time.deltaTime);
    }


    public void JumpBufferLogic()
    {
        if(m_jumpBufferTimer > 0 && m_coyoteTimer> 0)
        {
            Jump();
            
        }
        if (m_jumpBufferTimer >0)
        {
            m_jumpBufferTimer -= Time.deltaTime;
        }

    }

    public void Jump()
    {
        m_jumpBufferTimer = 0;
        ResetCoyoteTimer();
        m_vSpeed = 0;
        m_vVel.y = 0;
        m_vVel.y += JUMP_FORCE;
        m_playerController.JustGrounded();
        
    }


    public bool IsGrounded()
    {
        return m_characterController.isGrounded;
    }

    public void ResetJumpBufferTimer()
    {
        m_jumpBufferTimer = JUMP_BUFFER_TIME;
    }

    public void ResetCoyoteTimer()
    {
        m_coyoteTimer = COYOTE_TIME;
    }

    #region Get Variables
    public CharacterController GetCharacterController() => m_characterController;

    public float GetVerticalVelY() => m_vVel.y;
    #endregion




}
