using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ExplosionCollide : MonoBehaviour
{
    BombShowerManager _bombShowerManager;
    ScoreManager _scoreManager;
    public event Action<PlayerController> OnPlayerEliminated;

    private void Start()
    {
        _bombShowerManager = FindAnyObjectByType<BombShowerManager>();
        _scoreManager = GameManager.Instance.GetScoreManager();
        OnPlayerEliminated += _bombShowerManager.PlayerEliminated;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>() != null)
        {
           PlayerController _player = other.gameObject.GetComponent<PlayerController>();

            //_scoreManager.AddScore(_player);
            OnPlayerEliminated?.Invoke(_player);
            other.gameObject.SetActive(false);
            _bombShowerManager.HasAWinner();
        }
    }
}
