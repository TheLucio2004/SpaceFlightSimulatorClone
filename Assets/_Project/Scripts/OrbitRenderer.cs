using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class OrbitRenderer : MonoBehaviour
{
    [Header("Riferimenti")]
    public Rigidbody2D rocketRb;
    public Transform planetTransform;
    public float planetMass = 1000f;
    public float gravitationalConstant = 0.6674f;

    [Header("Impostazioni Orbita")]
    public int segments = 100;
    public float timeStep = 0.1f;

    private LineRenderer lineRenderer;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void LateUpdate()
    {
        if (rocketRb == null || planetTransform == null)
        {
            lineRenderer.positionCount = 0;
            return;
        }

        SimulateAndDrawOrbit();
    }

    void SimulateAndDrawOrbit()
    {
        Vector2 position = rocketRb.position;
        Vector2 velocity = rocketRb.linearVelocity;
        lineRenderer.positionCount = segments;

        for (int i = 0; i < segments; i++)
        {
            lineRenderer.SetPosition(i, position);

            // Calcola la forza gravitazionale
            Vector2 direction = (Vector2)planetTransform.position - position;
            float distance = direction.magnitude;
            if (distance == 0f) break;

            float forceMagnitude = gravitationalConstant * (planetMass * rocketRb.mass) / (distance * distance);
            Vector2 acceleration = direction.normalized * forceMagnitude / rocketRb.mass;

            // Aggiorna velocit� e posizione (integrazione di Euler)
            velocity += acceleration * timeStep;
            position += velocity * timeStep;
        }
    }
}
