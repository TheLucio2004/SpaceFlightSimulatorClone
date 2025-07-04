using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class RocketBuilder : MonoBehaviour
{
    [Header("Assemblaggio Razzo")]
    public List<GameObject> availableParts; // Prefab dei pezzi disponibili
    public Transform assemblyRoot; // Nodo root dove vengono attaccati i pezzi
    public float snapDistance = 0.5f;

    private GameObject draggingPart;
    private Vector3 offset;

    [System.Serializable]
    public class RocketDesign
    {
        public List<string> partNames = new();
        public List<Vector3> positions = new();
        public List<Quaternion> rotations = new();
    }

    public void StartDrag(GameObject partPrefab)
    {
        if (draggingPart != null) return;
        draggingPart = Instantiate(partPrefab, Input.mousePosition, Quaternion.identity, assemblyRoot);
        offset = Vector3.zero;
    }

    public void OnDrag()
    {
        if (draggingPart == null) return;
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f; // Distanza dalla camera
        draggingPart.transform.position = Camera.main.ScreenToWorldPoint(mousePos) + offset;

        // Snap magnetico ai punti di aggancio degli altri pezzi
        foreach (Transform child in assemblyRoot)
        {
            if (child == draggingPart.transform) continue;
            float dist = Vector3.Distance(child.position, draggingPart.transform.position);
            if (dist < snapDistance)
            {
                draggingPart.transform.position = child.position;
                break;
            }
        }
    }

    public void EndDrag()
    {
        draggingPart = null;
    }

    public void SaveDesign(string fileName)
    {
        RocketDesign design = new();
        foreach (Transform part in assemblyRoot)
        {
            design.partNames.Add(part.name.Replace("(Clone)", ""));
            design.positions.Add(part.localPosition);
            design.rotations.Add(part.localRotation);
        }

        string path = Path.Combine(Application.persistentDataPath, fileName + ".rocket");
        using (FileStream fs = new(path, FileMode.Create))
        {
            BinaryFormatter bf = new();
            bf.Serialize(fs, design);
        }
    }

    public void LoadDesign(string fileName)
    {
        string path = Path.Combine(Application.persistentDataPath, fileName + ".rocket");
        if (!File.Exists(path)) return;

        foreach (Transform child in assemblyRoot)
            Destroy(child.gameObject);

        using (FileStream fs = new(path, FileMode.Open))
        {
            BinaryFormatter bf = new();
            RocketDesign design = (RocketDesign)bf.Deserialize(fs);

            for (int i = 0; i < design.partNames.Count; i++)
            {
                GameObject prefab = availableParts.Find(p => p.name == design.partNames[i]);
                if (prefab != null)
                {
                    GameObject part = Instantiate(prefab, assemblyRoot);
                    part.transform.localPosition = design.positions[i];
                    part.transform.localRotation = design.rotations[i];
                }
            }
        }
    }

    internal RocketDesign GetCurrentDesign()
    {
        throw new NotImplementedException();
    }

    internal object LoadDesignFromData(RocketDesign rocketDesign)
    {
        throw new NotImplementedException();
    }
}
