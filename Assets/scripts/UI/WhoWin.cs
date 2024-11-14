using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class WhoWin : MonoBehaviour
{
    GameManager _gM;
    public TMP_Text _winner;
    public float _seeWhoWinCD = 3f;
    public GameObject _win;
    public bool _isFinish;


    private void Start()
    {
        _gM = GameManager.Instance;
        _win.SetActive(false);
        _isFinish = false;
    }

    public void WinnerUI()
    {
        _win.SetActive(true);
        if (_gM.GetPlayerManager().GetActivePlayers().Count == 1) 
        {
            foreach (var player in _gM.GetPlayerManager().GetActivePlayers())
            {
                _winner.text = $"P{player.PlayerId + 1}" + " win";
            }
        }
        else
        {
            _winner.text = $"Draw";
        }
        StartCoroutine(TimerToLook());
    }
    private IEnumerator TimerToLook()
    {
        Debug.Log("Timer started"); // Vérification que la coroutine démarre bien

        // Attendre le délai spécifié
        yield return new WaitForSeconds(_seeWhoWinCD);

        Debug.Log("Timer ended"); // Vérification que la coroutine arrive à la fin
        _isFinish = true;
        // Masquer l'interface de victoire après le délai
        _win.SetActive(false);
    }

}
