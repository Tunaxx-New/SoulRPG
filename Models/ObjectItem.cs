using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectItem : MonoBehaviour
{
    [Header("Object Information")]
    [SerializeField] public string title = "Object item model"; // Serialized field, private but editable in the Inspector
    [SerializeField] public string description = "Object model"; // Serialized field, private but editable in the Inspector
    [SerializeField] public float massG = 1; // Serialized field, private but editable in the Inspector
    [SerializeField] public float volumeSM3 = 1.0F; // Serialized field, private but editable in the Inspector
    [SerializeField] public float temperatureRelaticeEnviromentC = 0; // Serialized field, private but editable in the Inspector

    [Header("Object ID")]
    [SerializeField] public int id = 0; // Serialized field for ID, editable in the Inspector

    public JsonTemplate jsonTemplate;
    
    private List<ObjectItem> inventory = new List<ObjectItem>();

    void Start()
    {
        jsonTemplate = new JsonTemplate();
        jsonTemplate.title = title;
        jsonTemplate.description = description;
        jsonTemplate.data = new List<DataItem>
        {
            new DataItem { key = "id", value = id },
            new DataItem { key = "title", value = title },
            new DataItem { key = "description", value = description },
            new DataItem { key = "mass_kg", value = massG },
            new DataItem { key = "volume_m3", value = volumeSM3 },
            new DataItem { key = "temperature_c0", value = temperatureRelaticeEnviromentC }
        };
    }

    public void PickUp(ObjectItem item)
    {
        Debug.Log($"Picked up: {item.title} (ID: {item.id})");
        inventory.Add(item);
        Debug.Log(item.gameObject);
        Destroy(item.gameObject); // Optional: remove item from scene
    }

    public void Ignore(ObjectItem item)
    {
        Debug.Log($"Ignored: {item.title} (ID: {item.id})");
        // You could also trigger a different event here if desired
    }
}
