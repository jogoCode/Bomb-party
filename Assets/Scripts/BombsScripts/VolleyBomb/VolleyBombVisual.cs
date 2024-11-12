using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TreeEditor;
using UnityEngine;

public class VolleyBombVisual : MonoBehaviour
{
    [SerializeField] private GameObject _xAxis;
    [SerializeField] private GameObject _yAxis;
    [SerializeField] private GameObject _zAxis;
    void Update()
    {
        if(_xAxis.transform.localRotation.x >= 0)
        {
            _xAxis.transform.Rotate(0.5f, 0, 0);
        }
        else if (_xAxis.transform.localRotation.x < 0)
        {
            _xAxis.transform.Rotate(0.5f, 0, 0);
        }

        if (_yAxis.transform.localRotation.y >= 0)
        {
            _yAxis.transform.Rotate(0, 0, 0.5f);
        }
        else if (_yAxis.transform.localRotation.y < 0)
        {
            _yAxis.transform.Rotate(0, 0, 0.5f);
        }

        if (_zAxis.transform.localRotation.z >= 0)
        {
            _zAxis.transform.Rotate(0, 0, 0.25f);
        }
        else if (_zAxis.transform.localRotation.z < 0)
        {
            _zAxis.transform.Rotate(0, 0, 0.25f);
        }
    }
}
