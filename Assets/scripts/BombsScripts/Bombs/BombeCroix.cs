using UnityEngine;

public class BombeCroix : MonoBehaviour
{
    public GameObject explosionPrefab;     // Pr�fabriqu� de l'explosion
    public float explosionDelay = 2f;      // D�lai avant que la bombe explose
    public int explosionRange = 3;         // Port�e de l'explosion en unit�s
    public LayerMask obstaclesLayer;       // Layer pour d�tecter les obstacles
    public bool showGizmo = true;          // Afficher ou non le Gizmo dans la sc�ne
    public Color gizmoColor = Color.red;   // Couleur du Gizmo


    void Explode()
    {
        // G�n�rer l'explosion au centre
        CreateExplosion(transform.position);
        SoundManager.Instance.PlaySFX("Explosion");
        // Exploser dans les quatre directions : haut, bas, gauche, droite
        ExplodeInDirection(transform.forward);
        ExplodeInDirection(-transform.forward);
        ExplodeInDirection(-transform.right);
        ExplodeInDirection(transform.right);

        // D�truire la bombe apr�s l'explosion
        Destroy(gameObject);
    }

    void ExplodeInDirection(Vector3 direction)
    {
        for (int i = 1; i <= explosionRange; i++)
        {
            Vector3 explosionPosition = transform.position + direction * i;

            // V�rifier s'il y a un obstacle qui bloque l'explosion
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, i, obstaclesLayer);
            if (hit.collider != null)
            {
                // Arr�ter l'explosion dans cette direction s'il y a un obstacle
                break;
            }

            // Cr�er une explosion � la position actuelle

            CreateExplosion(explosionPosition);
        }
    }

    void CreateExplosion(Vector3 position)
    {
        // Instancier l'explosion � la position sp�cifi�e
        Instantiate(explosionPrefab, position, Quaternion.identity);
    }

    // Dessiner le Gizmo pour visualiser la port�e de l'explosion
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
        // D�clenche l'explosion apr�s un d�lai
        SoundManager.Instance.PlaySFX("PushSurBomb");
        Invoke(nameof(Explode), explosionDelay);
    }
}
