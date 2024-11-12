using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombeTh : Bombe
{
    public float maxRadius = 1f;                    // Taille maximale du cercle
    public float expansionSpeed = 2f;                // Vitesse à laquelle le cercle grandit
    public float duration = 3f;                      // Durée avant que le cercle disparaisse
    public Color gizmoColor = Color.green;           // Couleur du Gizmo dans la scène

    public GameObject explosionVFXPrefab;            // Préfabriqué pour le VFX d'explosion
    public bool _isStarted = false;
    private GameObject currentVFX;

    public SphereCollider sphereCollider;
    private float timer = 3f;


    void Update()
    {
        if (_isStarted)
        {
            // La sphère grandit jusqu'à la taille max
            if (sphereCollider.radius < maxRadius)
            {
                sphereCollider.radius += expansionSpeed * Time.deltaTime;
                if (currentVFX != null)
                {
                    float scale = sphereCollider.radius / maxRadius;
                    currentVFX.transform.localScale = new Vector3(scale, scale, scale);
                }
            }
            timer -= Time.deltaTime;

            // Après une certaine durée, détruire l'objet
            if (timer <= 0)
            {
                // Détruire l'objet
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null) // && collision != gameObject.GetComponent<PlayerController>())
        {
            TriggerExplosionVFX();

            // Démarrer l'expansion de la sphère
            _isStarted = true;
        }
    }


    private void TriggerExplosionVFX()
    {
        // Vérifier si le prefab du VFX est assigné, puis l'instancier au centre de la sphère
        if (explosionVFXPrefab != null)
        {

            currentVFX = Instantiate(explosionVFXPrefab, transform.position, Quaternion.identity);
            Destroy(currentVFX, timer+1);
        }
    }
    void OnDrawGizmos()
    {
        if (sphereCollider != null)
        {
            // Couleur du gizmo
            Gizmos.color = gizmoColor;
            // Dessiner une sphère dans la scène, correspondant au collider
            Gizmos.DrawWireSphere(transform.position, sphereCollider.radius*3f);
        }
    }
}
