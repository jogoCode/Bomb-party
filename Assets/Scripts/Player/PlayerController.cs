using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    int m_playerId;

    [SerializeField] PlayerMovement m_playerMovement;
    Vector2 m_inputDir = Vector2.zero;
    bool m_jumped = false;


    public int PlayerId
    {
        get { return m_playerId;}
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        m_inputDir = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        m_jumped = context.action.triggered;
    }


    void Awake()
    {
        m_playerMovement = GetComponent<PlayerMovement>(); 
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SceneManager.LoadScene(1);
        }
    }


    public void SetPlayerID(int newId)
    {
        m_playerId = newId;
    }


    Vector3 warpPosition = Vector3.zero;
    public void WarpToPosition(Vector3 newPosition)
    {
        m_playerMovement.GetCharacterController().enabled = false;
        warpPosition = newPosition;
        transform.position = warpPosition;
        m_playerMovement.GetCharacterController().enabled = true;
    }

 

    #region ACCESORS
    public Vector2 GetInputDir()=> m_inputDir;

    public bool GetJumped() => m_jumped;
    #endregion

}
