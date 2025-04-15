using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;

public class SoulPrompt : JsonTemplate
{
    string context = "You are on your own";
    public string message = "...";
    ActionPrompt actionPrompt;
    public List<ObjectItem> objects;
    public List<ActionItem> actions;

    public ObjectItemManager objectItemManager;
    public ActionItemManager actionItemManager;

    public SoulPrompt(ObjectItemManager objectItemManager, ActionItemManager actionItemManager)
    {
        title = "Soul prompt json";
        description = "Interaction sending json to ollama LLM";
        data = new List<DataItem>
        {
            new DataItem { key = "model", value = GameSettings.modelName },
            new DataItem { key = "messages", value = new List<Dictionary<string, object>>
                    {
                        new Dictionary<string, object>
                        {
                            { "role", "system" },
                            { "content", "content" }
                        }
                    }
                }
        };
        actionPrompt = new ActionPrompt();  
        objects = new List<ObjectItem>();
        actions = new List<ActionItem>();
        Update();

        this.objectItemManager = objectItemManager;
        this.actionItemManager = actionItemManager;
    }

    public void Update()
    {
        foreach (var item in data)
        {
            if (item.key == "messages")
            {
                List<Dictionary<string, object>> messages = new List<Dictionary<string, object>>();
                messages.Add(new Dictionary<string, object>
                {
                    { "role", "system" },
                    { "content", $"Please respond with a JSON-formatted response only, no explanations or non-JSON content. Do NOT include any code blocks, formatting, explanations, or markdown. Your response should strictly follow the JSON structure: {actionPrompt.toString()}" }
                });
                messages.Add(new Dictionary<string, object>
                {
                    { "role", "system" },
                    { "content", context }
                });
                List<JsonTemplate> objectJsonTemplates = objects.Select(objectItem => objectItem.jsonTemplate).ToList();
                string jsonTemplatesJson = JsonConvert.SerializeObject(new JsonListWrapper(objectJsonTemplates));
                messages.Add(new Dictionary<string, object>
                {
                    { "role", "system" },
                    { "content", $"You have next objects to make actions with: {jsonTemplatesJson}" }
                });
                List<JsonTemplate> actionJsonTemplates = actions.Select(actionItem => actionItem.jsonTemplate).ToList();
                string jsonTemplatesJsonActions = JsonConvert.SerializeObject(new JsonListWrapper(actionJsonTemplates));
                messages.Add(new Dictionary<string, object>
                {
                    { "role", "system" },
                    { "content", $"You have next actions to execute: {jsonTemplatesJsonActions}" }
                });
                messages.Add(new Dictionary<string, object>
                {
                    { "role", "user" },
                    { "content", message }
                });
                item.value = messages;
            }
        }
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

    public SoulResponse stringJsonToSoulResponse(string text)
    {
        JObject data = JObject.Parse(text);
        try
        {
            JObject response = JObject.Parse(data["choices"]?[0]?["message"]?["content"]?.ToObject<string>());
            ActionPrompt actionPrompt = new ActionPrompt();
            string message_ = "";
            int object_id = 0;
            int action_id = 0;
            foreach (var item in actionPrompt.data)
            {
                if (item.key == "message")
                {
                    item.value = response["message"]?.ToObject<string>();
                    message_ = response["message"].ToObject<string>();
                }
                else if (item.key == "object_id")
                {
                    item.value = response["object_id"]?.ToObject<int>();
                    object_id = response["object_id"].ToObject<int>();
                }
                else if (item.key == "action_id")
                {
                    item.value = response["action_id"]?.ToObject<int>();
                    action_id = response["action_id"].ToObject<int>();
                }
            }

            SoulResponse soulResponse = new SoulResponse
            {
                brain = data["id"]?.ToObject<string>(),
                brain_department = data["object"]?.ToObject<string>(),
                intellect = data["model"]?.ToObject<string>(),
                intellect_fingerprint = data["system_fingerprint"]?.ToObject<string>(),
                message = message_,
                object_ = objectItemManager.GetObjectItemById(object_id),
                action_ = actionItemManager.GetActionItemById(action_id),
                created = TimeConvertion.ConvertUnixTimestampToDateTime(data["created"].ToObject<long>()),
                soulTokenCount = data["usage"]["prompt_tokens"].ToObject<int>(),
                characterTokenCount = data["usage"]["completion_tokens"].ToObject<int>()
            };
            return soulResponse;
        }
        catch (Exception ex)
        {
            Debug.LogError($"An error occurred in OnValidate: {ex.Message}\n{ex.StackTrace}");
            Debug.LogWarning(data);
            SoulResponse soulResponse = new SoulResponse
            {
                brain = "brainless",
                brain_department = "stupidity",
                intellect = "low",
                intellect_fingerprint = "0",
                message = $"{ex.Message}",
                object_ = objectItemManager.GetObjectItemById(0),
                action_ = actionItemManager.GetActionItemById(0),
                created = DateTime.Now,
                soulTokenCount = 1,
                characterTokenCount = 0
            };
            return soulResponse;
        }
    }
}
