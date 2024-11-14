using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBombTag : MonoBehaviour
{
    [SerializeField]PlayerController _player;
    public bool _hasBomb;
    public GameObject _bomb;
    public float _stunTime = 1;
    public bool _stuned;
    public PlayerMovement _playerMovement;
    PartyPlayerParameters _playerParameters;
    public int _hasBombSpeed;
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

                StartCoroutine(attraped.Stuned());
            }
        }
    }
    private void Update()
    {
        if (_hasBomb)
        {

            _bomb.SetActive(true);
            if(!_stuned)
            _player.GetPlayerMovement().SetPlayerSpeed(_playerParameters.PlayerBaseSpeed + _hasBombSpeed);
        }
        else
        {
            _bomb.SetActive(false);
        }

    }

   public  IEnumerator  Stuned()
    {
        _stuned = true;
        _playerMovement.enabled = false; // Désactive le script de mouvement

        // Attendre la durée du stun
        yield return new WaitForSeconds(_stunTime);

        _stuned = false;
        _playerMovement.enabled = true; // Réactive le script de mouvement

    }

    public void Init()
    {
        _playerParameters = FindObjectOfType<PartyPlayerParameters>();
    }
}
