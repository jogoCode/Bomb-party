using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshPro))]
public class PlayerID : MonoBehaviour
{
    PlayerController m_playerController;
    public TextMeshPro m_tMPro;

    void Start()
    {
        m_playerController = GetComponentInParent<PlayerController>();
        m_tMPro = GetComponent<TextMeshPro>();
        Initialize();
    }

    void Initialize()
    {
        m_tMPro.text= $"P{m_playerController.PlayerId+1}";
    }

}
