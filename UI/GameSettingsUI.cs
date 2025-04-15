using UnityEngine;
using UnityEngine.UI;

public class GameSettingsUI : MonoBehaviour
{
    // UI Elements
    public Slider healthSlider;
    public Slider gravitySlider;
    public Text healthText;
    public Text gravityText;
    public Button saveButton;

    void Start()
    {
        // Load saved settings at the start
        GameSettings.LoadSettings();

        // Initialize UI elements with saved values
        healthSlider.value = GameSettings.MaxHealth;
        gravitySlider.value = GameSettings.Gravity;

        // Update the text to reflect the current values
        healthText.text = "Max Health: " + GameSettings.MaxHealth.ToString();
        gravityText.text = "Gravity: " + GameSettings.Gravity.ToString();

        // Add listeners for sliders
        healthSlider.onValueChanged.AddListener(UpdateMaxHealth);
        gravitySlider.onValueChanged.AddListener(UpdateGravity);

        // Add listener for save button
        saveButton.onClick.AddListener(SaveSettings);
    }

    // Method to update the MaxHealth value when the slider value changes
    void UpdateMaxHealth(float value)
    {
        GameSettings.MaxHealth = Mathf.RoundToInt(value);
        healthText.text = "Max Health: " + GameSettings.MaxHealth.ToString();
    }

    // Method to update the Gravity value when the slider value changes
    void UpdateGravity(float value)
    {
        GameSettings.Gravity = value;
        gravityText.text = "Gravity: " + GameSettings.Gravity.ToString();
    }

    // Save the settings when the button is clicked
    void SaveSettings()
    {
        GameSettings.SaveSettings();
        Debug.Log("Settings saved!");
    }
}
