using System.Collections.Generic;
using UnityEngine;

public class BombTagManager : MonoBehaviour
{

    public float _baseTimer = 30;
    public float _bombTimer = 30;
    public bool _boom = false;
    [SerializeField] List<PlayerController> _players;

    public PlayerController _hasBomb;
    public List<PlayerController> Players { get { return _players; } }

    private void Start()
    {
        
        AssignRandomBomb();
    }
    void Update()
    {
        BombTimer();
        if (_boom)
        {

            _hasBomb.gameObject.SetActive(false);
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
        // Réinitialiser tous les hasBomb à false
        foreach (PlayerController player in _players)
        {
            player.GetPlayerBombTag()._hasBomb = false;
        }

        // Choisir un joueur au hasard pour lui donner la bombe
        int randomIndex = Random.Range(0, _players.Count);
        _players[randomIndex].GetPlayerBombTag()._hasBomb = true;
        _hasBomb = _players[randomIndex];

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
