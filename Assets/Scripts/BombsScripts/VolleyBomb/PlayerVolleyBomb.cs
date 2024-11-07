using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVolleyBomb : MonoBehaviour
{
    [SerializeField] private int _points;

    public void IncresePoints()
    {
        _points ++;
    }
}
