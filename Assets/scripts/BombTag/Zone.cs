using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    public float shrinkRate = 1.0f; // La vitesse de réduction en unités par seconde
    public float minSize = 1.0f; // La taille minimum de la zone

    private Vector3 initialScale; // Sauvegarder l'échelle initiale

    void Start()
    {
        // Sauvegarde de l'échelle initiale de la zone
        initialScale = transform.localScale;
    }
    void Update()
    {
        // Rétrécir la zone progressivement si elle n'a pas atteint la taille minimum
        if (transform.localScale.x > minSize && transform.localScale.y > minSize && transform.localScale.z > minSize)
        {
            float shrinkAmount = shrinkRate * Time.deltaTime;
            transform.localScale -= new Vector3(shrinkAmount, shrinkAmount, shrinkAmount);

            // Empêche la zone de rétrécir au-delà de la taille minimale
            transform.localScale = new Vector3(
                Mathf.Max(transform.localScale.x, minSize),
                Mathf.Max(transform.localScale.y) ,
                Mathf.Max(transform.localScale.z, minSize)
            );
        }
    }
    public void ResetZone()
    {
        // Remet la zone à sa taille initiale si besoin
        transform.localScale = initialScale;
    }
}
