using Unity.VisualScripting;
using UnityEngine;

public class CarImpact : Cooldown
{
    [Header("Force Settings")]
    [SerializeField] protected CarCtlr carCtlr;
    [SerializeField] protected float impactForceThreshold = 10f;
    [Header("Checkpoint Settings")]
    [SerializeField] protected GameObject currentCheckpoint;
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

    public void ApplyImpactForce(Collision collision)
    {
        float impactForce = this.carCtlr.CarMovement.Speed / 50f; // Example calculation for impact force
        if (impactForce < this.impactForceThreshold) return;
        Vector3 bounce = 10f * impactForce * collision.contacts[0].normal;
        this.carCtlr.CarRigidbody.AddForce(bounce, ForceMode.Impulse);
        this.carCtlr.CarMovement.SetSpeed(0f); // Reset speed on impact
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
        this.currentCheckpoint = checkpoint.gameObject;
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
        checkpoint.transform.parent.gameObject.SetActive(false);

    }
}

