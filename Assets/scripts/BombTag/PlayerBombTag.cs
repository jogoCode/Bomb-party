using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerBombTag : MonoBehaviour
{
    [SerializeField]PlayerController _player;
    public bool _hasBomb;
    public GameObject _bomb;
    public float _stunTime = 1;
    public float _stunSpeed = 0;
    public bool _stuned;
    PartyPlayerParameters _playerParameters;
    [SerializeField] public bool HasPoint { get; set; }



    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.GetComponent<PlayerBombTag>())
        {
            BombTagManager bombTagManager = FindObjectOfType<BombTagManager>();
            if (bombTagManager._hasBomb == gameObject.GetComponent<PlayerController>())
            {
                // remet la vitesse du player qui na pas la bombe a la normale
                bombTagManager._hasBomb.GetPlayerMovement().SetPlayerSpeed(_playerParameters.PlayerBaseSpeed);

                PlayerBombTag attraped = hit.gameObject.GetComponent<PlayerBombTag>();

                bombTagManager._hasBomb = hit.gameObject.GetComponent<PlayerController>();
                _hasBomb = false;
                attraped._hasBomb = true;

                StartCoroutine(Stuned(bombTagManager._hasBomb));
            }
        }
    }
    private void Update()
    {
        if (_hasBomb)
        {

            _bomb.SetActive(true);
            if(!_stuned)
            _player.GetPlayerMovement().SetPlayerSpeed(_playerParameters.PlayerBaseSpeed + 3f);
        }
        else
        {
            _bomb.SetActive(false);
        }

    }

   public  IEnumerator  Stuned(PlayerController Player)
    {
        _stuned = true;
        Player.GetPlayerMovement().SetPlayerSpeed(_stunSpeed); // TO DO : Anim de Stun
        Player.ApplyImpulse(new Vector3(_player.GetLastInputDir().x,0, _player.GetLastInputDir().y),15);
        Debug.Log("jsuis Stun");
        yield return new WaitForSeconds (_stunTime);
        Debug.Log("jsuis plus Stun");
        _stuned = false;

    }

    public void Init()
    {
        _playerParameters = FindObjectOfType<PartyPlayerParameters>();
    }
}
