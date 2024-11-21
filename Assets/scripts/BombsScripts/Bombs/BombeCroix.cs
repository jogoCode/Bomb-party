using UnityEngine;

public class BombeCroix : MonoBehaviour
{
    public GameObject explosionPrefab;     // Préfabriqué de l'explosion
    public float explosionDelay = 2f;      // Délai avant que la bombe explose
    public int explosionRange = 3;         // Portée de l'explosion en unités
    public LayerMask obstaclesLayer;       // Layer pour détecter les obstacles
    public bool showGizmo = true;          // Afficher ou non le Gizmo dans la scène
    public Color gizmoColor = Color.red;   // Couleur du Gizmo


    void Explode()
    {
        // Générer l'explosion au centre
        CreateExplosion(transform.position);
        SoundManager.Instance.PlaySFX("Explosion");
        // Exploser dans les quatre directions : haut, bas, gauche, droite
        ExplodeInDirection(transform.forward);
        ExplodeInDirection(-transform.forward);
        ExplodeInDirection(-transform.right);
        ExplodeInDirection(transform.right);

        // Détruire la bombe après l'explosion
        Destroy(gameObject);
    }

    void ExplodeInDirection(Vector3 direction)
    {
        for (int i = 1; i <= explosionRange; i++)
        {
            Vector3 explosionPosition = transform.position + direction * i;

            // Vérifier s'il y a un obstacle qui bloque l'explosion
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, i, obstaclesLayer);
            if (hit.collider != null)
            {
                // Arrêter l'explosion dans cette direction s'il y a un obstacle
                break;
            }

            // Créer une explosion à la position actuelle

            CreateExplosion(explosionPosition);
        }
    }

    void CreateExplosion(Vector3 position)
    {
        // Instancier l'explosion à la position spécifiée
        Instantiate(explosionPrefab, position, Quaternion.identity);
    }

    // Dessiner le Gizmo pour visualiser la portée de l'explosion
    void OnDrawGizmos()
    {
        if (showGizmo)
        {
            Gizmos.color = gizmoColor;

            // Dessiner une ligne de Gizmo dans les quatre directions de l'explosion
            Gizmos.DrawLine(transform.position, transform.position + transform.forward * explosionRange);
            Gizmos.DrawLine(transform.position, transform.position + -transform.forward * explosionRange);
            Gizmos.DrawLine(transform.position, transform.position + transform.right * explosionRange);
            Gizmos.DrawLine(transform.position, transform.position + -transform.right * explosionRange);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Déclenche l'explosion après un délai
        SoundManager.Instance.PlaySFX("PushSurBomb");
        Invoke(nameof(Explode), explosionDelay);
    }
}
