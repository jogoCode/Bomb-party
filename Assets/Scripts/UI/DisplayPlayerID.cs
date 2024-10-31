using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(TextMeshPro))]
public class PlayerID : MonoBehaviour
{
    GameManager m_gameManager;
    PlayerController m_playerController;
    public TMP_Text m_tMPro;
    public Image m_confirmIcon;

    void Awake()
    {
        m_gameManager = GameManager.Instance;
        m_confirmIcon = GetComponentInChildren<Image>();
        m_playerController = GetComponentInParent<PlayerController>();
        m_tMPro = GetComponent<TMP_Text>();
        m_playerController.OnReady += DisplayConfirmIcon;
        m_gameManager.OnGameStarted += DisableConfirmIcon;
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
        m_confirmIcon.gameObject.SetActive(false);
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


    void DisableConfirmIcon()
    {
        m_confirmIcon.gameObject.SetActive(false);
    }
    void DisplayConfirmIcon(bool state)
    {
        m_confirmIcon.gameObject.SetActive(state);
    }

}
