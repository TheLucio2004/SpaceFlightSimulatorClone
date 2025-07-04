using UnityEngine;
using System.IO;

public class SaveSystem : MonoBehaviour
{
    [System.Serializable]
    public class SaveData
    {
        public RocketBuilder.RocketDesign rocketDesign;
        public float fuel;
        public int progressLevel;
    }

    public RocketBuilder rocketBuilder;
    public FuelManager fuelManager;
    public int progressLevel = 0; // Puoi aggiornare questo valore in base ai progressi del gioco

    private string SavePath => Path.Combine(Application.persistentDataPath, "savegame.json");

    void Awake()
    {
        LoadGame();
    }

    public void SaveGame()
    {
        SaveData data = new SaveData();

        // Salva design razzo
        if (rocketBuilder != null)
        {
            data.rocketDesign = rocketBuilder.GetCurrentDesign();
        }

        // Salva carburante
        if (fuelManager != null)
        {
            data.fuel = fuelManager.currentFuel;
        }

        // Salva progressi
        data.progressLevel = progressLevel;

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(SavePath, json);
    }

    public void LoadGame()
    {
        if (!File.Exists(SavePath))
            return;

        string json = File.ReadAllText(SavePath);
        SaveData data = JsonUtility.FromJson<SaveData>(json);

        // Carica design razzo
        if (rocketBuilder != null && data.rocketDesign != null)
        {
            _ = rocketBuilder.LoadDesignFromData(data.rocketDesign);
        }

        // Carica carburante
        if (fuelManager != null)
        {
            fuelManager.currentFuel = data.fuel;
        }

        // Carica progressi
        progressLevel = data.progressLevel;
    }
}
