using UnityEngine;

public class FuelManager : MonoBehaviour
{
    [Header("Carburante")]
    public float maxFuel = 100f;
    public float currentFuel;
    public float fuelConsumptionRate = 10f; // unità per secondo

    private RocketController rocketController;

    void Awake()
    {
        currentFuel = maxFuel;
        rocketController = GetComponent<RocketController>();
    }

    void Update()
    {
        if (rocketController == null)
            return;

        // Controlla se il razzo sta spingendo
        bool isThrusting = Input.GetKey(KeyCode.Space);

        if (isThrusting && currentFuel > 0f)
        {
            ConsumeFuel(Time.deltaTime);
        }
        else if (currentFuel <= 0f)
        {
            rocketController.enabled = false; // Disabilita la propulsione
        }
    }

    public void ConsumeFuel(float deltaTime)
    {
        currentFuel -= fuelConsumptionRate * deltaTime;
        if (currentFuel < 0f)
            currentFuel = 0f;
    }
}
