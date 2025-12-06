using System;
using System.Collections;
using UnityEngine;

public class CarImpact : Cooldown
{
    [Header("Force Settings")]
    [SerializeField] protected CarCtlr carCtlr;
    [SerializeField] protected float impactForceThreshold = 10f;
    [Header("Checkpoint Settings")]
    [SerializeField] protected GameObject currentCheckpoint;
    public event Action OnCompleteCheckpoint;
    [Header("Recharge Settings")]
    [SerializeField] protected float rechargeStayDuration = 3f; // thời gian cần đứng trong vùng để recharge
    [SerializeField] protected bool autoFullRecharge = true;    // nếu true thì recharge full khi hoàn tất

    protected Coroutine rechargeCoroutine;
    protected bool isInRechargePlace = false;
    public event Action OnCompleteRecharge; // optional event khi recharge xong

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCarController();
    }
    protected virtual void LoadCarController()
    {
        if (this.carCtlr != null) return;
        this.carCtlr = GetComponentInParent<CarCtlr>();
        Debug.LogWarning($"CarImpact: LoadCarController in {gameObject.name} ", gameObject);
    }
    //==================Enter and Exit==================
    public void OnEnterTrigger(Collider other)
    {
        this.EnterCheckPoint(other);
        this.EnterRechargePlace(other);
    }
    public void OnExitTrigger(Collider other)
    {
        this.ExitCheckPoint(other);
        this.ExitRechargePlace(other);
    }
    //==================Impact Force==================

    public bool ApplyImpactForce(Collision collision)
    {
        float impactForce = this.carCtlr.CarMovement.Speed / 50f; // Example calculation for impact force
        this.carCtlr.CarIntegrity.DecreaseIntegrity(impactForce * 5f); // Decrease integrity based on impact force
        bool shouldCrash = impactForce >= this.impactForceThreshold;
        if (shouldCrash)
        {
            Vector3 bounce = 10f * impactForce * collision.contacts[0].normal;
            this.carCtlr.CarRigidbody.AddForce(bounce, ForceMode.Impulse);
            Debug.Log("Impact force: " + impactForce);
        }
        return shouldCrash;
    }

    //==================Impact CheckPoint==================

    private void EnterCheckPoint(Collider other)
    {
        if (other.CompareTag("CheckPoint"))
        {
            this.StartCheckpointTimer(other);
        }
    }
    private void ExitCheckPoint(Collider other)
    {
        if (other.CompareTag("CheckPoint"))
        {
            this.ResetTimer();
        }
    }
    protected virtual void StartCheckpointTimer(Collider checkpoint)
    {
        this.currentCheckpoint = checkpoint.transform.parent.gameObject;
        this.StartCooldown();
    }
    protected override void ResetCooldown()
    {
        base.ResetCooldown();
        this.DisableCheckpoint(this.currentCheckpoint);
        this.currentCheckpoint = null;
    }
    protected void ResetTimer()
    {
        this.isCooldownActive = false;
        this.currentCheckpoint = null;
        this.currentTime = 0f;
    }
    protected virtual void DisableCheckpoint(GameObject checkpoint)
    {
        checkpoint.GetComponent<PooledObject>().Release();
        OnCompleteCheckpoint?.Invoke();
    }
    //==================Impact Recharge Place==================
    private void EnterRechargePlace(Collider other)
    {
        if (!other.CompareTag("RechargePlace")) return;

        if (rechargeCoroutine != null) return;
        isInRechargePlace = true;
        rechargeCoroutine = StartCoroutine(RechargeRoutine());
        Debug.Log($"⏱️ Entered RechargePlace. Need to stay {rechargeStayDuration}s to recharge.");
    }
    private void ExitRechargePlace(Collider other)
    {
        if (!other.CompareTag("RechargePlace")) return;

        if (rechargeCoroutine != null)
        {
            StopCoroutine(rechargeCoroutine);
            rechargeCoroutine = null;
        }
        isInRechargePlace = false;
        Debug.Log("⚠️ Left RechargePlace before completing recharge.");
    }
    protected IEnumerator RechargeRoutine()
    {
        float elapsed = 0f;

        while (elapsed < rechargeStayDuration)
        {
            // nếu bị rời khỏi vùng, dừng coroutine
            if (!isInRechargePlace)
            {
                yield break;
            }

            elapsed += Time.deltaTime;
            // (tùy chọn) bạn có thể gửi progress event ở đây
            yield return null;
        }
        if (carCtlr != null && carCtlr.CarEnergy != null)
        {
            if (autoFullRecharge)
            {
                carCtlr.CarEnergy.RechargeEnergy(carCtlr.CarEnergy.MaxEnergy);
            }
            else
            {
                carCtlr.CarEnergy.RechargeEnergy(carCtlr.CarEnergy.MaxEnergy);
            }

            Debug.Log("✅ Recharge completed: energy full.");
            OnCompleteRecharge?.Invoke();
        }
        rechargeCoroutine = null;
        isInRechargePlace = false;
    }
}

