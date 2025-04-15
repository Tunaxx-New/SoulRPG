using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionItem : MonoBehaviour
{
    [Header("Object ID")]
    [SerializeField] public int id = ActionEnumerable.IGNORE; // Serialized field for ID, editable in the Inspector

    [Header("Object Information")]
    [SerializeField] public string title; // Serialized field, private but editable in the Inspector
    [SerializeField] public string description; // Serialized field, private but editable in the Inspector

    [Header("Action")]
    [SerializeField] public ObjectItemEvent onAction;

    private void OnValidate() 
    {
        if (ActionEnumerable.titles == null || ActionEnumerable.descriptions == null)
        {
            new ActionEnumerable(); // Ensure ActionEnumerable is initialized
        }
        // Automatically update title and description based on the id whenever it changes
        if ((int)id < ActionEnumerable.titles.Count)
        {
            title = ActionEnumerable.titles[(int)id];
            description = ActionEnumerable.descriptions[(int)id];
        }
        else
        {
            title = "Unknown Title";
            description = "Unknown Description";
        }
    }

    public JsonTemplate jsonTemplate;

    void Start()
    {
        jsonTemplate = new JsonTemplate();
        jsonTemplate.title = "Action item model";
        jsonTemplate.description = "Action model";
        jsonTemplate.data = new List<DataItem>
        {
            new DataItem { key = "id", value = id },
            new DataItem { key = "title", value = title },
            new DataItem { key = "description", value = description },
        };
    }

    public void Execute(ObjectItem item)
    {
        onAction?.Invoke(item);
    }
}
