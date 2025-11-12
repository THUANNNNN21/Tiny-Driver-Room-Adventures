using UnityEngine;
using UnityEngine.UI;

public class EnergySlider : BaseSlider
{
    [SerializeField] protected float maxEnergy = 100f;
    [SerializeField] protected float currentEnergy = 100f;
    private void FixedUpdate()
    {
        this.UpdateEnergySlider();
    }
    protected void UpdateEnergySlider()
    {
        currentEnergy = CarCtlr.Instance.CarEnergy.CurrentEnergy;
        maxEnergy = CarCtlr.Instance.CarEnergy.MaxEnergy;
        uiSlider.value = currentEnergy / maxEnergy;
    }
    protected override void OnChanged(float value)
    {
        //
    }
}
