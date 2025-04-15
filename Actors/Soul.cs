using System.Collections.Generic;
using UnityEngine;

public class Soul : MonoBehaviour
{
    public float interactionRadius = 3f;
    private List<int> pickedUpObjectIds = new List<int>();

    public void PickUp(ObjectItem item)
    {
        Debug.Log($"Picked up: {item.title} (ID: {item.id})");
        pickedUpObjectIds.Add(item.id);
        Destroy(item.gameObject); // Optional: remove item from scene
    }

    public void Ignore(ObjectItem item)
    {
        Debug.Log($"Ignored: {item.title} (ID: {item.id})");
        // You could also trigger a different event here if desired
    }
}
