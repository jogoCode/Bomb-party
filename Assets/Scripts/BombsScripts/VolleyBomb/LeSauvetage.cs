using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeSauvetage : MonoBehaviour
{
    public WhoWin _whoWin;

    private void Start()
    {
        _whoWin = FindObjectOfType<WhoWin>();
    }
    void Update()
    {
       if (_whoWin._isFinish)
       {
           GameManager.Instance.GetPartyManager().ChangeMiniGame();
           _whoWin._isFinish = false;
       }
    }
}
