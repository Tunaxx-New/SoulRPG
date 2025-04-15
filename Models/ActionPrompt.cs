using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class ActionPrompt : JsonTemplate
{
    public ActionPrompt()
    {
        title = "Action prompt json";
        description = "That action structure prompt result to take action in json";
        data = new List<DataItem>
        {
            new DataItem { key = "message", value = "Example of your opinion, that describes your action" },
            new DataItem { key = "action_id", value = 0 },
            new DataItem { key = "object_id", value = 0 }
        };
    }

    public string toString()
    {
        var jsonObject = new Dictionary<string, object>();
        foreach (var item in data)
        {
            jsonObject[item.key] = item.value;
        }
        return JsonConvert.SerializeObject(jsonObject);
    }
}
