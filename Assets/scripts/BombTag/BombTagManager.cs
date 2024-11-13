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
    PlayerBombTag _playerBombTag;
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
        List<PlayerController> list = GameManager.Instance.GetPlayerManager().GetActivePlayers();
        if (list.Count == 1)
        {
            if (_hasBomb.GetPlayerBombTag().HasPoint == false)
            {
                FinDeGame();
                _hasBomb.GetPlayerBombTag().HasPoint = true;
            }
        }
        if (_boom)
        {

            if (_hasBomb.GetPlayerBombTag().HasPoint == false) 
            {
                _scoreManager.AddPlayerToList(_hasBomb, _scoreManager.Bonus);
                _hasBomb.gameObject.SetActive(false);
                _hasBomb.GetPlayerBombTag().HasPoint = true;
            }
            if (list.Count > 1)
            {
                // TODO : si la fine gagne des point return;
                _bombTimer = _baseTimer;
                AssignRandomBomb();
            }
        }


    }

    public void AssignRandomBomb()
    {
        _players = GameManager.Instance.GetPlayerManager().GetActivePlayers();

        // Réinitialiser tous les hasBomb à false
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

    void FinDeGame()
    {
        List<PlayerController> list = GameManager.Instance.GetPlayerManager().GetActivePlayers();
        
        foreach (PlayerController playerController in list)
        {
            _scoreManager.OneWin();
            GameManager.Instance.GetPartyManager().ChangeMiniGame();
        }
        
    }
    

}
