using UnityEngine;

public class CarEnergy : TMonoBehaviour
{
    [SerializeField] protected float maxEnergy = 100f;
    public float MaxEnergy => maxEnergy;
    [SerializeField] protected float currentEnergy;
    public float CurrentEnergy => currentEnergy;
    [SerializeField] protected bool isExhausted = false;
    public bool IsExhausted => isExhausted;
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
        isExhausted = this.currentEnergy <= 0f;
    }
    public void RechargeEnergy(float amount)
    {
        this.currentEnergy += amount;
        this.currentEnergy = Mathf.Min(this.currentEnergy, this.maxEnergy);
    }
}
