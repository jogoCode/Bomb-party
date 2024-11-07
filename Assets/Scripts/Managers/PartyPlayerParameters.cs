using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyPlayerParameters : MonoBehaviour
{
    [SerializeField] float m_playerSpeed = PlayerMovement.BASE_SPEED;
    [SerializeField] float m_jumpForce = PlayerMovement.BASE_JUMP_FORCE;
    [SerializeField] float m_dashSpeed = PlayerMovement.BASE_DASH_SPEED;
    [SerializeField] float m_dashCooldown = PlayerMovement.BASE_DASH_CD;

    PlayerManager m_playerManager;

    [SerializeField] bool m_parryBombActions;

    

    private void Start()
    {
        m_playerManager = GameManager.Instance.GetPlayerManager();
        Init();
    }


    private void Init()
    {
        m_playerManager.SetPlayersSpeed(m_playerSpeed);
        m_playerManager.SetPlayersJumpForce(m_jumpForce);
        m_playerManager.SetPlayersDashCoolDown(m_dashCooldown);
        HandleParryBombAction();
    }

    void HandleParryBombAction()
    {
        if (m_parryBombActions)
        {
            m_playerManager.EnabledPlayersParryBomb();
        }
        else
        {
            m_playerManager.DisabledPlayersParryBomb();
        }
    }




}
