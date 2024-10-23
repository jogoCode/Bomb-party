using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{

    PlayerController m_playerController;
    CharacterController m_characterController;

    [SerializeField ]const float JUMP_FORCE = 2;
    [SerializeField ]const float GRAVITY = 9.81f;
    [SerializeField] const float SPEED = 4;


    Vector3 m_vVel;

    [SerializeField] float m_vSpeed = 0;
    [SerializeField] float m_vVelFactor = 4f;

    bool Grounded;


    void Start()
    {
        m_playerController = GetComponent<PlayerController>();
        m_characterController = GetComponent<CharacterController>();  
    }



    // Update is called once per frame
    void Update()
    {
        Vector2 inputDir = m_playerController.GetInputDir();
        bool jumped = m_playerController.GetJumped();
        Vector3 dir = new Vector3(inputDir.x,m_vVel.y, inputDir.y);

        if (!IsGrounded())
        {
            gravity();
        }
        else
        {
            if (jumped)
            {
                Jump();
            }        
        }
   
        Movement(dir, 5);       
    }

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

    void Jump()
    {
        m_vSpeed = 0;
        m_vVel = Vector3.zero;
        m_vVel.y = JUMP_FORCE;
    }



    bool IsGrounded()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 0.1f))
        {
            if (hit.collider != null)
            {
                m_vVel = Vector3.zero;
                m_vSpeed = 0;
               return true;
            }
        }
        return false;

    }

    #region Get Variables
    CharacterController GetCharacterController() => m_characterController;
    
    #endregion

}
