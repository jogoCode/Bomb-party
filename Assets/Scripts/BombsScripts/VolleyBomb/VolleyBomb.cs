using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class VolleyBomb : MonoBehaviour
{
    [SerializeField] private float _bombTimer = 10;
    [SerializeField] private bool _boom = false;
    [SerializeField] private bool _goundCollision = false;
    public int _playerId = 0;
    [SerializeField] private bool _P1Lost = false;
    [SerializeField] private bool _P2Lost = false;
    [SerializeField] private bool _P3Lost = false;
    [SerializeField] private bool _P4Lost = false;
    [SerializeField] private GameObject _bombCenterChecker;
    Rigidbody _RB;
    VolleyBombManager _volleyBombManager;
    [SerializeField] private GameObject _bombVFX;
    [SerializeField] private GameObject _bombeTimer;
    [SerializeField] private TMP_Text _bombeTimerDisplay;
    [SerializeField] private Camera _camera;
    [SerializeField] private PlayerManager _playerManager;

    private void Start()
    {
        _RB = GetComponent<Rigidbody>();
        _volleyBombManager = FindObjectOfType<VolleyBombManager>();
        _camera = FindObjectOfType<Camera>();   
        _playerManager = GameManager.Instance.GetPlayerManager();
    }

    void Update()
    {
        var vec = _RB.velocity;
        var force = Mathf.Clamp(vec.magnitude,0,20);
        var newvec = vec.normalized * force;
        _RB.velocity = newvec;
        BombTimer();
        _bombCenterChecker.transform.position = transform.position;
        if(_goundCollision == true || _boom == true)
        {
            Lost();
            gameObject.SetActive(false);
        }
    }

    void Lost()
    {
        _RB.isKinematic = true;
        PlayerController player;
        switch (_playerId) 
        {
            case 0:
                _P1Lost = true;
                Debug.Log("Player 1 Lost");
                player = _volleyBombManager._playersList[_playerId];
                if(_volleyBombManager._playersList.Count > 2)
                {
                    _volleyBombManager._playersList.Remove(player);
                    player.gameObject.SetActive(false);
                }
                else
                {
                    _volleyBombManager._playersList.Remove(player);
                    PlayerController winner = _volleyBombManager._playersList[0];
                    Debug.Log("player " + (winner.PlayerId + 1) + "won");
                    _volleyBombManager._playersList.Add(player);
                    _volleyBombManager._intertiming = _volleyBombManager._interTime;
                    _volleyBombManager.Init();
                }
                _goundCollision = false;
                _boom = false;
                break;
            
            case 1:
                _P2Lost = true;
                Debug.Log("Player 2 Lost");
                player = _volleyBombManager._playersList[_playerId];
                if (_volleyBombManager._playersList.Count > 2)
                {
                    _volleyBombManager._playersList.Remove(player);
                    player.gameObject.SetActive(false);
                }
                else
                {
                    _volleyBombManager._playersList.Remove(player);
                    PlayerController winner = _volleyBombManager._playersList[0];
                    Debug.Log("player " + (winner.PlayerId + 1) + "won");
                    _volleyBombManager._playersList.Add(player);
                    _volleyBombManager._intertiming = _volleyBombManager._interTime;
                    _volleyBombManager.Init();
                }
                _goundCollision = false;
                _boom = false;
                break;
            
            case 2:
                _P3Lost = true;
                Debug.Log("Player 3 Lost");
                player = _volleyBombManager._playersList[_playerId];
                if (_volleyBombManager._playersList.Count > 2)
                {
                    _volleyBombManager._playersList.Remove(player);
                    player.gameObject.SetActive(false);
                }
                else
                {
                    _volleyBombManager._playersList.Remove(player);
                    PlayerController winner = _volleyBombManager._playersList[0];
                    Debug.Log("player " + (winner.PlayerId + 1) + "won");
                    _volleyBombManager._playersList.Add(player);
                    _volleyBombManager._intertiming = _volleyBombManager._interTime;
                    _volleyBombManager.Init();
                }
                _goundCollision = false;
                _boom = false;
                break;
            
            case 3:
                _P4Lost = true;
                Debug.Log("Player 4 Lost");
                player = _volleyBombManager._playersList[_playerId];
                if (_volleyBombManager._playersList.Count > 2)
                {
                    _volleyBombManager._playersList.Remove(player);
                    player.gameObject.SetActive(false);
                }
                else
                {
                    _volleyBombManager._playersList.Remove(player);
                    PlayerController winner = _volleyBombManager._playersList[0];
                    Debug.Log("player " + (winner.PlayerId + 1) + "won");
                    _volleyBombManager._playersList.Add(player);
                    _volleyBombManager._intertiming = _volleyBombManager._interTime;
                    _volleyBombManager.Init();
                }
                _goundCollision = false;
                _boom = false;
                break;
        }
        _volleyBombManager.Init();
        Instantiate(_bombVFX,transform.position,transform.rotation);
        _volleyBombManager._bombDidntSpawn = true;
        Destroy(gameObject);
        _volleyBombManager.Init();
    }

    void BombTimer()
    {
        if (_bombTimer > 0)
        {
            _bombTimer = Mathf.Clamp(_bombTimer, 0, _bombTimer) - Time.deltaTime; // diminue le titer de la bombe au fine du temps 
        }
        else
        {
            _boom = true;
            //Debug.Log("Timer of Bombe is = to : " + _bombTimer);
            //Debug.Log("Boom !");
        }
        _bombeTimerDisplay.text = ((int)_bombTimer).ToString();
        _bombeTimer.transform.LookAt(_camera.transform.position);
    }

    private void OnCollisionEnter(Collision collision)
    {   
        if (collision.gameObject.tag == "Ground")
        {
            _goundCollision = true;
            _bombTimer = 0;
            //Debug.Log("Sol Touché !");
        }
    }
}
