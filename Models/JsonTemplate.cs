using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class JsonTemplate
{
    public string title;
    public string description;
    public List<DataItem> data;

    public void SetValueByKey(string key, object newValue)
    {
        // Find the DataItem with the specified key
        foreach (var item in data)
        {
            if (item.key == key)
            {
                // Update the value of the found DataItem
                item.value = newValue;
                return; // Exit after updating
            }
        }

        // If the key wasn't found, optionally add a new DataItem
        data.Add(new DataItem { key = key, value = newValue });
    }
}