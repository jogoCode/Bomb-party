using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    [SerializeField]GameObject m_model;
    [SerializeField] Renderer[] m_coloredParts;
    PlayerController m_playerController;
    [SerializeField] float m_rotationSpeed;
    Animator m_animator;

    Material m_material;    


    Vector3 m_playerDir;
    Quaternion m_targetRotation;
    void Start()
    {
        m_playerController = GetComponent<PlayerController>();  
        m_animator = GetComponentInChildren<Animator>();
        SetMaterial();
    }

    void Update()
    {
        if(m_playerController.GetInputDir().magnitude > 0)
        {
            m_animator.SetBool("isRunning", true);
        }
        else
        {
            m_animator.SetBool("isRunning", false);
        }

        RotateModel();    
    }

    void RotateModel()
    {
      
        if (m_playerController.GetInputDir() != Vector2.zero)
        {
            m_playerDir = new Vector3(m_playerController.GetInputDir().x, 0, m_playerController.GetInputDir().y);
            m_targetRotation = Quaternion.LookRotation(m_playerDir);
        }
       
        m_model.transform.rotation = Quaternion.Slerp(m_model.transform.rotation, m_targetRotation, m_rotationSpeed * Time.deltaTime);
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
                Debug.Log(part.materials.Length);
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
}
