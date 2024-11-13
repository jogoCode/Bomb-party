using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    public float shrinkRate = 1.0f; // La vitesse de r�duction en unit�s par seconde
    public float minSize = 1.0f; // La taille minimum de la zone

    private Vector3 initialScale; // Sauvegarder l'�chelle initiale

    void Start()
    {
        // Sauvegarde de l'�chelle initiale de la zone
        initialScale = transform.localScale;
    }
    void Update()
    {
        // R�tr�cir la zone progressivement si elle n'a pas atteint la taille minimum
        if (transform.localScale.x > minSize && transform.localScale.y > minSize && transform.localScale.z > minSize)
        {
            float shrinkAmount = shrinkRate * Time.deltaTime;
            transform.localScale -= new Vector3(shrinkAmount, shrinkAmount, shrinkAmount);

            // Emp�che la zone de r�tr�cir au-del� de la taille minimale
            transform.localScale = new Vector3(
                Mathf.Max(transform.localScale.x, minSize),
                Mathf.Max(transform.localScale.y) ,
                Mathf.Max(transform.localScale.z, minSize)
            );
        }
    }
    public void ResetZone()
    {
        // Remet la zone � sa taille initiale si besoin
        transform.localScale = initialScale;
    }
}
