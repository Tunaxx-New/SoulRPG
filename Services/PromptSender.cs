using System;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PromptSender : MonoBehaviour
{
    public UnityEngine.UI.Button sendButton;
    public InputField messageField;
    public Transform diaryTransform;
    public GameObject diaryRecord;

    [SerializeField]
    public DiaryListView diaryListView;

    public ObjectItemManager objectItemManager;
    public ActionItemManager actionItemManager;

    void Start()
    {
        if (sendButton != null)
        {
            sendButton.onClick.AddListener(() => StartCoroutine(SendJson()));
        }
        SoulResponse soulResponse = new SoulResponse
            {
                brain = "brainless",
                brain_department = "stupidity",
                intellect = "low",
                intellect_fingerprint = "0",
                message = "asd",
                object_ = objectItemManager.GetObjectItemById(0),
                action_ = actionItemManager.GetActionItemById(0),
                created = DateTime.Now,
                soulTokenCount = 1,
                characterTokenCount = 0
            };
        AddMessage(soulResponse);
    }

    IEnumerator SendJson()
    {
        ActionPrompt actionPrompt = new ActionPrompt();
        string responseTemplate = JsonConvert.SerializeObject(actionPrompt);

        SoulPrompt soulPrompt = new SoulPrompt(objectItemManager, actionItemManager);
        soulPrompt.message = messageField.text;
        soulPrompt.objects = new List<ObjectItem>(FindObjectsOfType<ObjectItem>());
        soulPrompt.actions = new List<ActionItem>(FindObjectsOfType<ActionItem>());
        soulPrompt.Update();
        Debug.Log(JObject.Parse(soulPrompt.toString()));

        using (UnityWebRequest www = new UnityWebRequest(GameSettings.promptHost + GameSettings.promptRoute, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(soulPrompt.toString());
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Authorization", $"Bearer {GameSettings.authorizationBearer}");
            www.SetRequestHeader("OpenAI-Organization", $"{GameSettings.openaiOrganization}");
            www.SetRequestHeader("OpenAI-Project", $"{GameSettings.openaiProject}");

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + www.error + ", " + www.downloadHandler.text);
            }
            else
            {
                SoulResponse soulResponse = soulPrompt.stringJsonToSoulResponse(www.downloadHandler.text);
                soulResponse.action_.Execute(soulResponse.object_);
                AddMessage(soulResponse);
            }
        }
    }

    void AddMessage(SoulResponse soulResponse)
    {
        diaryListView.addToSoulResponses(soulResponse);
    }
}