using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

public class DiaryListView : MonoBehaviour
{
    public UIDocument uiMainDocument;
    public VisualTreeAsset diaryRecordTemplate;
    private VisualElement sidePanel;    
    private List<SoulResponse> soulResponses;

    void OnEnable()
    {
        var root = uiMainDocument.rootVisualElement;
        sidePanel = root.Q("side-panel");
        soulResponses = new List<SoulResponse>();
    }

    void PopulateSidePanel()
    {
        sidePanel.Clear();

        foreach (SoulResponse soulResponse in soulResponses)
        {
            VisualElement recordInstance = new VisualElement();
            recordInstance = diaryRecordTemplate.CloneTree();

            var brainLabel = recordInstance.Q<Label>("brain");
            if (brainLabel != null) brainLabel.text = $"{soulResponse.brain}";
            var brainDepartmentLabel = recordInstance.Q<Label>("brain-department");
            if (brainDepartmentLabel != null) brainDepartmentLabel.text = $"{soulResponse.brain_department}";
            var intellectLabel = recordInstance.Q<Label>("intellect");
            if (intellectLabel != null) intellectLabel.text = $"{soulResponse.intellect}";
            var intellectFingerprintLabel = recordInstance.Q<Label>("intellect-fingerprint");
            if (intellectFingerprintLabel != null) intellectFingerprintLabel.text = $"{soulResponse.intellect_fingerprint}";
            var messageLabel = recordInstance.Q<Label>("message");
            if (messageLabel != null) messageLabel.text = $"{soulResponse.message}";

            var objectLabel = recordInstance.Q<Label>("object-name");
            if (objectLabel != null) objectLabel.text = $"{soulResponse.object_.title}";
            var actionLabel = recordInstance.Q<Label>("action-name");
            if (actionLabel != null) actionLabel.text = $"{soulResponse.action_.title}";

            var createdLabel = recordInstance.Q<Label>("time");
            if (createdLabel != null) createdLabel.text = soulResponse.created.ToString("g");

            var confrontationSlider = recordInstance.Q<Slider>("confrontation-ratio");
            if (confrontationSlider != null && soulResponse.characterTokenCount > 0)
            {
                float ratio = (float)soulResponse.soulTokenCount / soulResponse.characterTokenCount;
                confrontationSlider.SetEnabled(false);
                confrontationSlider.value = Mathf.Clamp01(ratio); // ensures it's between 0 and 1
            }

            sidePanel.Add(recordInstance);
        }
    }

    public void addToSoulResponses(SoulResponse soulResponse)
    {
        soulResponses.Add(soulResponse);
        PopulateSidePanel();
    }
}
