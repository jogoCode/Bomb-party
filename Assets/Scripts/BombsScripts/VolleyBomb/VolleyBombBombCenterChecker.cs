using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolleyBombBombCenterChecker : MonoBehaviour
{
    VolleyBomb _volleyBomb;
    private void Start()
    {
        _volleyBomb = FindObjectOfType<VolleyBomb>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "VolleyBombZoneP1") { _volleyBomb._playerId = 0; }
        if (other.tag == "VolleyBombZoneP2") { _volleyBomb._playerId = 1; }
        if (other.tag == "VolleyBombZoneP3") { _volleyBomb._playerId = 2; }
        if (other.tag == "VolleyBombZoneP4") { _volleyBomb._playerId = 3; }
    }
}
