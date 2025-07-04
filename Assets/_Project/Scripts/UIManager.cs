using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Riferimenti")]
    public Rigidbody2D rocketRb;
    public Transform planetTransform;
    public FuelManager fuelManager;

    [Header("Elementi UI")]
    public Text speedText;
    public Text altitudeText;
    public Text fuelText;
    public Button separateStageButton;
    public Button thrustButton;

    [System.Obsolete]
    void Start()
    {
        if (separateStageButton != null)
            separateStageButton.onClick.AddListener(OnSeparateStage);

        if (thrustButton != null)
            thrustButton.onClick.AddListener(OnThrust);
    }

    void Update()
    {
        UpdateSpeed();
        UpdateAltitude();
        UpdateFuel();
    }

    void UpdateSpeed()
    {
        if (rocketRb != null && speedText != null)
        {
            float speed = rocketRb.linearVelocity.magnitude;
            speedText.text = $"Velocit�: {speed:F1} m/s";
        }
    }

    void UpdateAltitude()
    {
        if (rocketRb != null && planetTransform != null && altitudeText != null)
        {
            float altitude = Vector2.Distance(rocketRb.position, planetTransform.position);
            altitudeText.text = $"Altitudine: {altitude:F1} m";
        }
    }

    void UpdateFuel()
    {
        if (fuelManager != null && fuelText != null)
        {
            fuelText.text = $"Carburante: {fuelManager.currentFuel:F1}";
        }
    }

    [System.Obsolete]
    void OnSeparateStage()
    {
        // Trova e chiama StageManager se presente
        var stageManager = FindObjectOfType<StageManager>();
        if (stageManager != null)
        {
            var method = stageManager.GetType().GetMethod("SeparateStage", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public);
            if (method != null)
                method.Invoke(stageManager, null);
        }
    }

    void OnThrust()
    {
        // Simula la pressione della barra spaziatrice (opzionale, dipende dalla logica di RocketController)
        // Puoi implementare qui la logica per attivare la spinta tramite UI se necessario
    }
}
