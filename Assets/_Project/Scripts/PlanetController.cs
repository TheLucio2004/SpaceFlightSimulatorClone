using UnityEngine;
    
[RequireComponent(typeof(Collider2D))]
public class PlanetController : MonoBehaviour
{
    [Header("Gravit�")]
    public float planetMass = 1000f;

    [Header("Atmosfera")]
    public bool hasAtmosphere = true;
    public float atmosphereRadius = 5f;
    public float dragInAtmosphere = 2f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Applica attrito se l'oggetto entra nell'atmosfera
        if (hasAtmosphere && IsWithinAtmosphere(other.transform.position))
        {
            var rb = other.attachedRigidbody;
            if (rb != null)
            {
                rb.linearDamping = dragInAtmosphere;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Rimuove l'attrito quando esce dall'atmosfera
        if (hasAtmosphere)
        {
            var rb = other.attachedRigidbody;
            if (rb != null)
            {
                rb.linearDamping = 0f;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Gestione collisione con la superficie del pianeta
        // Puoi aggiungere effetti, danni, game over, ecc.
        Debug.Log($"{collision.gameObject.name} ha colpito il pianeta!");
    }

    private bool IsWithinAtmosphere(Vector2 position)
    {
        return Vector2.Distance(transform.position, position) <= atmosphereRadius;
    }

    private void OnDrawGizmosSelected()
    {
        if (hasAtmosphere)
        {
            Gizmos.color = new Color(0, 0.5f, 1f, 0.2f);
            Gizmos.DrawSphere(transform.position, atmosphereRadius);
        }
    }
}
