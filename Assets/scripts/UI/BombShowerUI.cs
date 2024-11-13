using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BombShowerUI : MonoBehaviour
{
    BombShowerManager _bombShower;
    public TMP_Text _timer;

    private void Start()
    {
        _bombShower = FindObjectOfType<BombShowerManager>();
    }
    private void Update()
    {
        _timer.text = Mathf.Ceil(_bombShower._time).ToString() + " s";
    }
}
