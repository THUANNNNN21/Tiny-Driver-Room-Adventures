using UnityEngine;

public class CarIntegrity : TMonoBehaviour
{
    [SerializeField] protected float maxIntegrity = 100f;
    public float MaxIntegrity => maxIntegrity;
    [SerializeField] protected float currentIntegrity;
    public float CurrentIntegrity => currentIntegrity;
    [SerializeField] protected bool isBroken = false;
    public bool IsBroken => isBroken;
    protected override void LoadValues()
    {
        base.LoadValues();
        this.currentIntegrity = this.maxIntegrity;
    }
    public void DecreaseIntegrity(float amount)
    {
        this.currentIntegrity -= amount;
        this.currentIntegrity = Mathf.Max(this.currentIntegrity, 0f);
        this.isBroken = this.currentIntegrity <= 0f;
    }
}
