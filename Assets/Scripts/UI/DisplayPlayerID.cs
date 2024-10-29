using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshPro))]
public class PlayerID : MonoBehaviour
{
    PlayerController m_playerController;
    public TMP_Text m_tMPro;

    void Awake()
    {

        m_playerController = GetComponentInParent<PlayerController>();
        m_tMPro = GetComponent<TMP_Text>();
        Initialize();
    }


    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            SetColor();
        }
    }

    void Initialize()
    {
        m_tMPro.text= $"P{m_playerController.PlayerId+1}";
        SetColor();
    }

    void SetColor()
    {
        switch (m_playerController.PlayerId)
        {
            case 0:
                m_tMPro.color = GameManager.BLUE;
                //m_tMPro.fontSharedMaterial.SetColor(ShaderUtilities.ID_FaceColor,GameManager.BLUE);
                break;
            case 1:
                m_tMPro.color = GameManager.GREEN;
            break;
            case 2:
                m_tMPro.color = GameManager.YELLOW;
                break;
            case 3:
                m_tMPro.color = GameManager.PURPLE;
            break;
        }
        
        m_tMPro.alpha = 1.0f;
        //m_tMPro.SetMaterialDirty(); 

    }

}
