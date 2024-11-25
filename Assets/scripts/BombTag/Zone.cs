using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    [SerializeField] BombTagManager _bombTagManager;
    [SerializeField] float _shrinkRate = 1.0f; // La vitesse de réduction en unités par seconde
    [SerializeField] float _minSize = 1.0f; // La taille minimum de la zone

    private Vector3 initialScale; // Sauvegarder l'échelle initiale

    [SerializeField] float _startWall = 15f;

    void Start()
    {
        // Sauvegarde de l'échelle initiale de la zone
        initialScale = transform.localScale;
    }
    void Update()
    {
        if (_bombTagManager.GameReady && _bombTagManager.BombTime <= _startWall) 
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
