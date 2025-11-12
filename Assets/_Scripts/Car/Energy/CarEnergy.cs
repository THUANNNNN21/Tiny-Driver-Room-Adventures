using UnityEngine;

public class CarEnergy : TMonoBehaviour
{
    [SerializeField] protected float maxEnergy = 100f;
    [SerializeField] protected float currentEnergy;
    protected override void LoadComponents()
    {
        base.LoadComponents();
    }
    protected override void LoadValues()
    {
        base.LoadValues();
        this.currentEnergy = this.maxEnergy;
    }
    public void ConsumeEnergy(float amount)
    {
        this.currentEnergy -= amount;
        this.currentEnergy = Mathf.Max(this.currentEnergy, 0f);
    }
}
