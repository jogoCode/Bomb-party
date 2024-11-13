using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public List<PlayerController> _playerDied;
    public List<PlayerController> _maxPlayer;
    [SerializeField] int _bonus =0;
    [SerializeField] int _winGame = 4;

    public int Bonus {  get { return _bonus; } } 
    public int WinGame { get { return _winGame; } }
    void Start()
    {
        _bonus = 0;
        // Exemple d'initialisation des joueurs
        _maxPlayer = GameManager.Instance.GetPlayerManager().GetPlayerList(); 
    }

    private void Update()
    {
        //PlayerController[] players = FindObjectsOfType<PlayerController>();


        // ajout de  _playerDied.Count == _maxPlayer.Count si 2 joueurs arrivent a mourir en meme temps

        //if (_playerDied.Count == _maxPlayer.Count-1 || _playerDied.Count == _maxPlayer.Count)
        //{ // Met à jour le score des joueurs et trie la liste à la fin de la partie
        //    EndGame();
        //}
    }

    void EndGame()
    {
        
        // Trie les joueurs en fonction du score, du plus élevé au plus faible
        _playerDied.Sort((a, b) => b._score.CompareTo(a._score));

        // Affiche les scores triés
        Debug.Log("Classement des joueurs: ");
        foreach (var player in _playerDied)
        {
            Debug.Log($"P{player.PlayerId+1} : {player._score} points");
        }
    }

    public void AddScore(PlayerController player, int ajout)
    {
        player._score += ajout;
        _bonus++;
    }

    public void AddPlayerToList(PlayerController player, int ajout)
    {
        // TODO : mettre le dernier joueur en setactive false pour qu'il gagne aussi des points
        foreach (PlayerController p in _playerDied) { 
            if (p == player)
            {
                return;
            }       
        }
        AddScore(player, ajout);
        _playerDied.Add(player);
    }

    public void ChangeGame()
    {
        _bonus = 0;
        _playerDied.Clear();
    }

    public void OneWin()
    {
        _playerDied = GameManager.Instance.GetPlayerManager().GetActivePlayers();
        foreach (PlayerController p in _playerDied)
        {
            AddScore(p, _winGame);
        }
    }
}
