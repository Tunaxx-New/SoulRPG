using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;

public class SlidePanel : MonoBehaviour
{
    private UIDocument uiDocument;
    private VisualElement sidePanel;
    private VisualElement button;
    private bool isVisible = false;

    void OnEnable()
    {
        uiDocument = GetComponent<UIDocument>();
        StartCoroutine(DelayedUISetup());
    }

    IEnumerator DelayedUISetup()
    {
        while (uiDocument == null || uiDocument.rootVisualElement.childCount == 0)
            yield return null;

        button = uiDocument.rootVisualElement.Q("open-button");
        sidePanel = uiDocument.rootVisualElement.Q("side-panel");

        // Example: toggle on key press
        button.RegisterCallback<ClickEvent>(evt =>
        {
            TogglePanel();
        });
    }

    public void TogglePanel()
    {
        isVisible = !isVisible;
        if (isVisible)
        {
            sidePanel.RemoveFromClassList("hidden");
            sidePanel.AddToClassList("show");
        }
        else
        {
            sidePanel.RemoveFromClassList("show");
            sidePanel.AddToClassList("hidden");
        }
    }
}
