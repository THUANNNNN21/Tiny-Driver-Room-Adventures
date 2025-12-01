using System;
using Unity.VisualScripting;
using UnityEngine;

public class CarImpact : Cooldown
{
    [Header("Force Settings")]
    [SerializeField] protected CarCtlr carCtlr;
    [SerializeField] protected float impactForceThreshold = 10f;
    [Header("Checkpoint Settings")]
    [SerializeField] protected GameObject currentCheckpoint;
    public event Action OnCompleteCheckpoint;
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

    public void EnterCheckPoint(Collider other)
    {
        if (other.CompareTag("CheckPoint"))
        {
            this.StartCheckpointTimer(other);
        }
    }
    public void ExitCheckPoint(Collider other)
    {
        if (other.CompareTag("CheckPoint"))
        {
            this.ResetTimer();
        }
    }
    protected virtual void StartCheckpointTimer(Collider checkpoint)
    {
        this.currentCheckpoint = checkpoint.transform.parent.gameObject;
        // this.SetCooldownTime(currentCheckpoint.GetComponent<>.TimeToDisable);
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
}

