using System;
using UnityEngine;

public class ActionItemManager : MonoBehaviour
{
    // This method will find the ObjectItem by ID
    public ActionItem GetActionItemById(int id)
    {
        // Find all objects with the ObjectItem component
        ActionItem[] actionsInScene = FindObjectsOfType<ActionItem>();

        ActionItem foundAction = null;

        foreach (var item in actionsInScene)
        {
            if (item.id == id)
            {
                foundAction = item;
                break;
            }
        }

        // If the requested ID wasn't found, fallback to the object with id 0
        if (foundAction == null)
        {
            foundAction = Array.Find(actionsInScene, item => item.id == 0); // Find object with id 0
        }

        if (foundAction == null)
        {
            Debug.LogWarning("No ObjectItem found with ID: " + id + " and no ObjectItem with ID 0 found.");
        }

        return foundAction;
    }
}
