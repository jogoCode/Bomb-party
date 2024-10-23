using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTag : MonoBehaviour
{
    public float _bombTimer = 10;
    public bool _boom = false;

    void Update()
    {
        BombTimer();

    }
    void BombTimer()
    {
        if (_bombTimer > 0)
        {
            _bombTimer = Mathf.Clamp(_bombTimer, 0, 10) - Time.deltaTime; // diminue le titer de la bombe au fine du temps 
        }
        else
        {
            _boom = true;
            //Debug.Log("Timer of Bombe is = to : " + _bombTimer);
            //Debug.Log("Boom !");
        }
    }
}
