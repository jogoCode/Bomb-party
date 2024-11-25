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
       if (_whoWin.IsFinish)
       {
           GameManager.Instance.GetPartyManager().ChangeMiniGame();
           _whoWin.IsFinish = false;
       }
    }
}
