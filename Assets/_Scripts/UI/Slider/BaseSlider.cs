using UnityEngine;
using UnityEngine.UI;

public abstract class BaseSlider : TMonoBehaviour
{
    [Header("Slider Settings")]
    [SerializeField] protected Slider uiSlider;
    void Start()
    {
        this.AddOnClickEvent();
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadUISlider();
    }
    private void LoadUISlider()
    {
        if (uiSlider != null) return;
        uiSlider = GetComponent<Slider>();
        Debug.LogWarning("Load UI Slider: " + uiSlider.name, gameObject);
    }
    protected virtual void AddOnClickEvent()
    {
        this.uiSlider.onValueChanged.AddListener(this.OnChanged);
    }
    protected abstract void OnChanged(float value);
}
