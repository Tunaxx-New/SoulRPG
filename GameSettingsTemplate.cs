using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings
{
    // Configurable Settings with default values
    public static int MaxHealth = 100;
    public static float Gravity = 9.81f;
    public static string Version = "0.0.1";
    public static string promptHost = "https://api.openai.com";
    public static string promptRoute = "/v1/chat/completions";
    public static string authorizationBearer = "";
    public static string openaiOrganization = "";
    public static string openaiProject = "";
    public static string modelName = "gpt-4o-mini-2024-07-18";

    public static void SaveSettings()
    {
        PlayerPrefs.SetInt("MaxHealth", MaxHealth);
        PlayerPrefs.SetFloat("Gravity", Gravity);
        PlayerPrefs.Save();
    }

    public static void LoadSettings()
    {
        MaxHealth = PlayerPrefs.GetInt("MaxHealth", 100); // Default to 100 if not saved
        Gravity = PlayerPrefs.GetFloat("Gravity", 9.81f);  // Default to 9.81 if not saved
    }
}
