using UnityEngine;

public class CarImpact : TMonoBehaviour
{
    [SerializeField] protected CarCtlr carCtlr;
    [SerializeField] protected float impactForceThreshold = 10f;
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
    public void ApplyImpactForce(Collision collision, float force)
    {
        float impactForce = force;
        if (impactForce < impactForceThreshold) return;
        Vector3 bounce = 10f * impactForce * collision.contacts[0].normal;
        carCtlr.CarRigidbody.AddForce(bounce, ForceMode.Impulse);
        carCtlr.CarMovement.SetSpeed(0f); // Reset speed on impact
    }
}

