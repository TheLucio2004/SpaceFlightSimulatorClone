using UnityEngine;

public class RocketController : MonoBehaviour
{
    [Header("Impostazioni Razzo")]
    public float thrustForce = 5f;
    public float rotationSpeed = 200f;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Rotazione con frecce destra/sinistra
        float rotationInput = -Input.GetAxis("Horizontal");
        rb.MoveRotation(rb.rotation + rotationInput * rotationSpeed * Time.deltaTime);

        // Spinta con barra spaziatrice
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(transform.up * thrustForce);
        }
    }
}