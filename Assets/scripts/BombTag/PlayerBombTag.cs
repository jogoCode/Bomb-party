using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBombTag : MonoBehaviour
{
    [SerializeField]PlayerController _player;
    [SerializeField] bool _hasBomb;

    //tkt c'est la deums
    [SerializeField] GameObject _bomb;
    [SerializeField] GameObject _light;
    [SerializeField] GameObject _fx1;
    [SerializeField] GameObject _fx2;
    //

    [SerializeField] float _stunTime = 1;
    [SerializeField] bool _stuned;
    PlayerMovement _playerMovement;
    PartyPlayerParameters _playerParameters;
    [SerializeField] int _hasBombSpeed;
    [SerializeField] bool _hasPoint;


    public bool HasPoint { get { return _hasPoint; } set {; } }
    public int HasBombSpeed { get { return _hasBombSpeed; } set {; } }
    public bool HasABomb { get { return _hasBomb; } set {; } }
    public GameObject Bomb { get { return _bomb; } }
    public GameObject Light { get { return _light; } }

    public GameObject Fx1 { get { return _fx1; } }

    public GameObject Fx2 { get { return _fx2; } }


    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.GetComponent<PlayerBombTag>())
        {
            BombTagManager bombTagManager = FindObjectOfType<BombTagManager>();
            if (bombTagManager.HasBomb == gameObject.GetComponent<PlayerController>())
            {
                // remet la vitesse du player qui na pas la bombe a la normale
                bombTagManager.HasBomb.GetPlayerMovement().SetPlayerSpeed(_playerParameters.PlayerBaseSpeed);

                PlayerBombTag attraped = hit.gameObject.GetComponent<PlayerBombTag>();

                bombTagManager.HasBomb = hit.gameObject.GetComponent<PlayerController>();
                _hasBomb = false;
                attraped._hasBomb = true;
                SoundManager.Instance.PlaySFX("siren");
                SoundManager.Instance.PlaySFX("PushSurPlayer");
                SoundManager.Instance.PlaySFX("WooshBatBomb");
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
