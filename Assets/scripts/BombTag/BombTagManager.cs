using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class BombTagManager : MonoBehaviour
{

    public float _baseTimer = 30;
    public float _bombTimer = 30;
    public bool _boom = false;
    [SerializeField] List<PlayerController> _players;
    [SerializeField] float  _baseSpeed;
    public ScoreManager _scoreManager;
    PartyPlayerParameters _playerParameters;
    public PlayerController _hasBomb;
    public List<PlayerController> Players { get { return _players; } }

    private void Awake()
    {
        _playerParameters = FindObjectOfType<PartyPlayerParameters>();
    }
    private void Start()
    {
        AssignRandomBomb();
        _baseSpeed = _playerParameters.PlayerBaseSpeed;
        _scoreManager = GameManager.Instance.GetScoreManager();
        foreach (PlayerController playerController in _players)
        {
            playerController.GetPlayerBombTag().Init();
        }
    }
    void Update()
    {
        BombTimer();
        if (_boom)
        {
            _hasBomb.gameObject.SetActive(false);
            _scoreManager.AddPlayerToList(_hasBomb, _scoreManager.Bonus);
            List<PlayerController> list = GameManager.Instance.GetPlayerManager().GetActivePlayers();
            if (list.Count > 1)
            {
                _bombTimer = _baseTimer;
                AssignRandomBomb();
            }

        }

    }

    public void AssignRandomBomb()
    {
        _players = GameManager.Instance.GetPlayerManager().GetActivePlayers();
        // R�initialiser tous les hasBomb � false
        foreach (PlayerController player in _players)
        {
            player.GetPlayerBombTag()._hasBomb = false;
        }

        // Choisir un joueur au hasard pour lui donner la bombe
        int randomIndex = Random.Range(0, _players.Count);
        _players[randomIndex].GetPlayerBombTag()._hasBomb = true;
        _hasBomb = _players[randomIndex];
        _hasBomb.GetPlayerMovement().SetPlayerSpeed(_baseSpeed+ 500f);

    }
    void BombTimer()
    {
        if (_bombTimer > 0)
        {
            _bombTimer = Mathf.Clamp(_bombTimer, 0, _baseTimer) - Time.deltaTime; // diminue le timer de la bombe au fine du temps 
        }
        else
        {
            _boom = true;

            Debug.Log("sa a PETER");
        }
    }
    

}
