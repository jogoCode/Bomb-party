using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombeTh : Bombe
{
    public float maxRadius = 5f;                    // Taille maximale du cercle
    public float expansionSpeed = 2f;                // Vitesse � laquelle le cercle grandit
    public float duration = 3f;                      // Dur�e avant que le cercle disparaisse
    public Color gizmoColor = Color.green;           // Couleur du Gizmo dans la sc�ne

    public GameObject explosionVFXPrefab;            // Pr�fabriqu� pour le VFX d'explosion
    public bool _isStarted = false;
    private GameObject currentVFX;

    public SphereCollider sphereCollider;
    private float timer;

    void Update()
    {
        if (_isStarted)
        {
            // Instancier le VFX d'explosion
            TriggerExplosionVFX();
            // La sph�re grandit jusqu'� la taille max
            if (sphereCollider.radius < maxRadius)
            {
                sphereCollider.radius += expansionSpeed * Time.deltaTime;
                if (currentVFX != null)
                {
                    float scale = sphereCollider.radius / maxRadius;
                    currentVFX.transform.localScale = new Vector3(scale, scale, scale);
                }
            }




            // Augmenter le timer
            timer += Time.deltaTime;

            // Apr�s une certaine dur�e, d�truire l'objet ou d�sactiver le collider
            if (timer >= duration)
            {


                // D�truire l'objet
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision != null) // && collision != gameObject.GetComponent<PlayerController>())
        {
            // D�marrer l'expansion de la sph�re
            _isStarted = true;
        }
    }

    void OnDrawGizmos()
    {
        if (sphereCollider != null)
        {
            // Couleur du gizmo
            Gizmos.color = gizmoColor;

            // Dessiner une sph�re dans la sc�ne, correspondant au collider
            Gizmos.DrawWireSphere(transform.position, sphereCollider.radius);
        }
    }

    private void TriggerExplosionVFX()
    {
        // V�rifier si le prefab du VFX est assign�, puis l'instancier au centre de la sph�re
        if (explosionVFXPrefab != null)
        {
            currentVFX = Instantiate(explosionVFXPrefab, transform.position, Quaternion.identity);
        }
    }
}
