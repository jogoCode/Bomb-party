using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBombTag : MonoBehaviour
{
    [SerializeField]PlayerController _player;
    public bool _hasBomb;
    public GameObject _bomb;
    public BombTag _bombTag;
    



    private void Start()
    {
        
    }



    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        BombTagManager bombTagManager = FindObjectOfType<BombTagManager>();
        if (hit.gameObject.GetComponent<PlayerBombTag>())
        {
            if (bombTagManager._hasBomb == gameObject.GetComponent<PlayerController>())
            {
               
                PlayerBombTag attraped = hit.gameObject.GetComponent<PlayerBombTag>();

                bombTagManager._hasBomb = hit.gameObject.GetComponent<PlayerController>();
                _hasBomb = false;
                attraped._hasBomb = true;
                // TODO : mettre un mini stun pour laisser le joueur s'enfuir ?
            }
        }
    }


    private void Update()
    {
        if (_hasBomb)
        {
            _bomb.SetActive(true);
        }else
        {
            _bomb.SetActive(false);
        }
    }

}
