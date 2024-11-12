using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{

    PlayerController m_playerController;
    Animator m_animator;
    Oscillator m_ocscillator;

    [SerializeField]GameObject m_model;
    [SerializeField] Renderer[] m_coloredParts;
    Material m_material;
    [SerializeField] float m_rotationSpeed;
    [SerializeField] GameObject m_playerBat;

    public const float SPEED_ANIM_RATIO = 5;


   

   
    public event Action OnGrounded;


    Vector3 m_playerDir;
    Quaternion m_targetRotation;


    public Oscillator Oscillator
    {
        get { return m_ocscillator; }
    }

    public GameObject Model
    {
        get { return m_model; }
    }

    #region BUILT-IN
    void Awake()
    {
        m_playerController = GetComponent<PlayerController>();  
        m_animator = GetComponentInChildren<Animator>();
        m_ocscillator = GetComponent<Oscillator>();
        SetMaterial();
    }

    void Update()
    {
        VerticalMoveAnimation();
        RotateModel();    
    }
    #endregion

    void RotateModel()
    {
      
        if (m_playerController.GetInputDir() != Vector2.zero)
        {
            m_playerDir = new Vector3(m_playerController.GetInputDir().x, 0, m_playerController.GetInputDir().y);
            m_targetRotation = Quaternion.LookRotation(m_playerDir);
        }
       
        m_model.transform.rotation = Quaternion.Slerp(m_model.transform.rotation, m_targetRotation, m_rotationSpeed * Time.deltaTime);
    }


    #region Animation

    public void BatAnimation()
    {
        if (m_playerController.GetPlayerStateManager().GetState() == PlayerStateManager.PlayerStates.ATK) return;
        if (m_playerBat.activeInHierarchy)
        {
            m_animator.SetTrigger("isBat");
        }
        else
        {
            m_animator.SetTrigger("isPush");
        }
     
        
        Oscillator.StartOscillator(15);
    }


    public void HitAnimation()
    {
        if (m_playerController.GetPlayerStateManager().GetState() == PlayerStateManager.PlayerStates.HIT) return;
        m_animator.SetTrigger("isHit");
        Oscillator.StartOscillator(5);
    }
    public void MoveAnimation(float x,float speedPercent) // x = xvel for horizontalAnim . y = yVel for verticalAnim . speedPercent = Speed ratio
    {

        float speed = speedPercent / SPEED_ANIM_RATIO;
        if(x == 0)
        {
            speed = 1;
        }
        m_animator.SetFloat("SpeedPercent",speed);
        m_animator.SetFloat("Movement", x);
    }

    public void VerticalMoveAnimation()
    {
        PlayerMovement playerMovement = m_playerController.GetPlayerMovement();
        m_animator.SetFloat("VMovement", playerMovement.GetVerticalVelY());
    }

#endregion
    public void ActiveBatModel()
    {
        m_playerBat.SetActive(true);
    }

    public void DesactiveBatModel()
    {
        m_playerBat.SetActive(false);
    }

    void SetMaterial()
    {
        switch (m_playerController.PlayerId)
        {
            case 0:
                m_material = GameManager.Instance.PLAYER1;
            break;
            case 1:
                m_material = GameManager.Instance.PLAYER2;
            break;
            case 2:
                m_material = GameManager.Instance.PLAYER3;
            break;
            case 3:
                m_material = GameManager.Instance.PLAYER4;
            break;
        }
       foreach (var part in m_coloredParts)
        {
            if (part.materials.Length > 1) 
            {
                Material[] mats = part.materials;
                mats[1] = m_material;
                part.gameObject.GetComponent<Renderer>().materials = mats;
            }
            else
            {
                part.gameObject.GetComponent<Renderer>().material = m_material;

            }
        }
    }

    #region Events
    public void JustGrounded()
    {
        Oscillator.StartOscillator(5);
    }


    public void CheckGrounded(bool isGrounded)
    {
        if (m_animator != null)
        {
            m_animator.SetBool("isGrounded", isGrounded);
        }
    }

    #endregion
}
