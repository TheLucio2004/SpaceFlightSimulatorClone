using UnityEngine;

public class StageManager : MonoBehaviour
{
    [Header("Stadi del razzo")]
    public GameObject[] rocketStages; // Ordine: dal basso verso l'alto
    public KeyCode separateKey = KeyCode.X;

    private int currentStage = 0;

    void Update()
    {
        if (Input.GetKeyDown(separateKey))
        {
            SeparateStage();
        }
    }

    void SeparateStage()
    {
        if (currentStage < rocketStages.Length)
        {
            GameObject stage = rocketStages[currentStage];

            // Scollega lo stadio dal razzo principale
            stage.transform.parent = null;

            // Aggiungi Rigidbody2D se non presente per farlo cadere
            if (stage.GetComponent<Rigidbody2D>() == null)
            {
                stage.AddComponent<Rigidbody2D>();
            }

            currentStage++;
        }
    }
}
