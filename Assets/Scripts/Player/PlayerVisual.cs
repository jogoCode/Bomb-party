using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    [SerializeField]GameObject m_model;
    PlayerController m_playerController;
    [SerializeField] float m_rotationSpeed;


    Vector3 m_playerDir;
    Quaternion m_targetRotation;
    void Start()
    {
        m_playerController = GetComponent<PlayerController>();  
    }

    void Update()
    {
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
}
