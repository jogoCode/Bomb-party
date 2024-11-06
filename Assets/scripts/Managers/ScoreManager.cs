using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public List<PlayerController> _player;
    int _bonus =0;

    void Start()
    {
        _bonus = 0;
        // Exemple d'initialisation des joueurs
        _player = GameManager.Instance.GetPlayerManager().GetPlayerList(); 
    }

    private void Update()
    {

        PlayerController[] players = FindObjectsOfType<PlayerController>();

        if (players.Length == 0)
        { // Met à jour le score des joueurs et trie la liste à la fin de la partie
            EndGame();
        }
    }

    void EndGame()
    {
        // Trie les joueurs en fonction du score, du plus élevé au plus faible
        _player.Sort((a, b) => b._score.CompareTo(a._score));

        // Affiche les scores triés
        Debug.Log("Classement des joueurs: ");
        foreach (var player in _player)
        {
            Debug.Log($"P{player.PlayerId+1} : {player._score} points");
        }
    }

    public void AddScore(PlayerController player)
    {
        player._score += _bonus;
        _bonus++;
    }

    public void AddPlayerToList(PlayerController player)
    {
        foreach (PlayerController p in _player) { 
            if (p == player)
            {
                return;
            }       
        }
        _player.Add(player);
    }
}
