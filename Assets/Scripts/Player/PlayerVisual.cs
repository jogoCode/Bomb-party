using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    [SerializeField]GameObject m_model;
    PlayerController m_playerController;
    [SerializeField] float m_rotationSpeed;

    Material m_material;    


    Vector3 m_playerDir;
    Quaternion m_targetRotation;
    void Start()
    {
        m_playerController = GetComponent<PlayerController>();  
        SetMaterial();
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
       m_model.GetComponentInChildren<MeshRenderer>().material = m_material;
    }
}
