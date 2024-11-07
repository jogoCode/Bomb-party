using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopCollid : MonoBehaviour
{
    [SerializeField] BoxCollider _collider;

    void Start()
    {
        StartCoroutine(AttendreUneSeconde());
    }

    IEnumerator AttendreUneSeconde()
    {
        yield return new WaitForSeconds(1f); // Attendre 1 seconde
        _collider.enabled = false;

    }
}
