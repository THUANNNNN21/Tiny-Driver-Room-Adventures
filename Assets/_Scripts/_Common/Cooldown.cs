using UnityEngine;

public abstract class Cooldown : TMonoBehaviour
{
    [Header("Cooldown Info")]
    [SerializeField] protected float currentTime = 0f;
    [SerializeField] protected float cooldownTime;
    public float CooldownTime => cooldownTime;
    [SerializeField] protected bool isCooldownActive = false;
    protected virtual void Update()
    {
        this.UpdateCooldown();
    }
    protected virtual void StartCooldown()
    {
        this.isCooldownActive = true;
        this.currentTime = 0f;
    }
    protected virtual void UpdateCooldown()
    {
        if (!isCooldownActive) return;
        currentTime += Time.deltaTime;
        if (currentTime >= cooldownTime)
        {
            this.ResetCooldown();
        }
    }
    protected virtual void ResetCooldown()
    {
        if (!isCooldownActive) return;
        this.isCooldownActive = false;
        this.currentTime = 0f;
    }
    public void SetCooldownTime(float time)
    {
        this.cooldownTime = time;
    }
}