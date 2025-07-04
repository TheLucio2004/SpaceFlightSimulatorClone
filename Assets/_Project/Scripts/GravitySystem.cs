using UnityEngine;

public class GravitySystem : MonoBehaviour
{
    [Header("Riferimenti")]
    public Rigidbody2D rocketRb;
    public Transform planetTransform;
    public float planetMass = 1000f;
    public float gravitationalConstant = 0.6674f; // Valore arbitrario per il gameplay

    void FixedUpdate()
    {
        if (rocketRb == null || planetTransform == null)
            return;

        Vector2 direction = (Vector2)planetTransform.position - rocketRb.position;
        float distance = direction.magnitude;

        if (distance == 0f)
            return;

        // Legge di gravitazione universale: F = G * (m1 * m2) / r^2
        float forceMagnitude = gravitationalConstant * (planetMass * rocketRb.mass) / (distance * distance);
        Vector2 force = direction.normalized * forceMagnitude;

        rocketRb.AddForce(force);
    }
}