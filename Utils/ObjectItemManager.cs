using System;
using UnityEngine;

public class ObjectItemManager : MonoBehaviour
{
    // This method will find the ObjectItem by ID
    public ObjectItem GetObjectItemById(int id)
    {
        // Find all objects with the ObjectItem component
        ObjectItem[] objectsInScene = FindObjectsOfType<ObjectItem>();

        ObjectItem foundObject = null;

        // First, try to find the object with the requested ID
        foreach (var item in objectsInScene)
        {
            if (item.id == id)  // requestedId is the id you're looking for
            {
                foundObject = item;
                break;
            }
        }

        // If the requested ID wasn't found, fallback to the object with id 0
        if (foundObject == null)
        {
            foundObject = Array.Find(objectsInScene, item => item.id == 0); // Find object with id 0
        }

        if (foundObject == null)
        {
            Debug.LogWarning("No ObjectItem found with ID: " + id + " and no ObjectItem with ID 0 found.");
        }

        return foundObject;
    }
}
