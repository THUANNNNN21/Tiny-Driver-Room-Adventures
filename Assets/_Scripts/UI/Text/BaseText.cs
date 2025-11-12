using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class BaseText : TMonoBehaviour
{
    [Header("Base Text Settings")]
    [SerializeField] protected TextMeshProUGUI uiText;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadUIText();
    }
    private void LoadUIText()
    {
        if (uiText != null) return;
        uiText = GetComponent<TextMeshProUGUI>();
        Debug.LogWarning("Load UIText: " + uiText.name, gameObject);
    }
}
