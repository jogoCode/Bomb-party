using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    [SerializeField] BombTagManager _bombTagManager;
    [SerializeField] float _shrinkRate = 1.0f; // La vitesse de r�duction en unit�s par seconde
    [SerializeField] float _minSize = 1.0f; // La taille minimum de la zone

    private Vector3 initialScale; // Sauvegarder l'�chelle initiale

    [SerializeField] float _startWall = 15f;

    void Start()
    {
        // Sauvegarde de l'�chelle initiale de la zone
        initialScale = transform.localScale;
    }
    void Update()
    {
        if (_bombTagManager.GameReady && _bombTagManager.BombTime <= _startWall) 
        {
            // R�tr�cir la zone progressivement si elle n'a pas atteint la taille minimum
            if (transform.localScale.x > _minSize && transform.localScale.y > _minSize && transform.localScale.z > _minSize)
            {
                float shrinkAmount = _shrinkRate * Time.deltaTime;
                transform.localScale -= new Vector3(shrinkAmount, shrinkAmount, shrinkAmount);

                // Emp�che la zone de r�tr�cir au-del� de la taille minimale
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
        // Remet la zone � sa taille initiale si besoin
        transform.localScale = initialScale;
    }
}
