using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{

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
        m_characterController = GetComponent<CharacterController>();  
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 inputDir = new Vector3(Input.GetAxis("Horizontal"),m_vVel.y,Input.GetAxis("Vertical"));
        if (!IsGrounded())
        {
            gravity();
        }
        else
        {
            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }        
        }
   
        Movement(inputDir, 5);       
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
               return true;
            }
        }
        return false;

    }

    #region Get Variables
    CharacterController GetCharacterController() => m_characterController;
    
    #endregion

}
