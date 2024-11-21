using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    public BombTagManager _bombTagManager;
    public float _shrinkRate = 1.0f; // La vitesse de réduction en unités par seconde
    public float _minSize = 1.0f; // La taille minimum de la zone

    private Vector3 initialScale; // Sauvegarder l'échelle initiale

    public float _startWall = 15f;

    void Start()
    {
        // Sauvegarde de l'échelle initiale de la zone
        initialScale = transform.localScale;
    }
    void Update()
    {
        if (_bombTagManager._gameReady && _bombTagManager._bombTimer <= _startWall) 
        {
            // Rétrécir la zone progressivement si elle n'a pas atteint la taille minimum
            if (transform.localScale.x > _minSize && transform.localScale.y > _minSize && transform.localScale.z > _minSize)
            {
                float shrinkAmount = _shrinkRate * Time.deltaTime;
                transform.localScale -= new Vector3(shrinkAmount, shrinkAmount, shrinkAmount);

                // Empêche la zone de rétrécir au-delà de la taille minimale
                transform.localScale = new Vector3(
                    Mathf.Max(transform.localScale.x, _minSize),
                    Mathf.Max(transform.localScale.y) ,
                    Mathf.Max(transform.localScale.z, _minSize)
                );
            }
        }
    }
    public void ResetZone()
    {
        // Remet la zone à sa taille initiale si besoin
        transform.localScale = initialScale;
    }
}
