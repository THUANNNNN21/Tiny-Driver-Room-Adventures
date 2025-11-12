using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class CarCtlr : TMonoBehaviour
{
    [SerializeField] Rigidbody carRigidbody;
    public Rigidbody CarRigidbody { get => carRigidbody; }
    [SerializeField] CarEnergy carEnergy;
    public CarEnergy CarEnergy { get => carEnergy; }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadRigidbody();
        this.LoadCarEnergy();
    }
    protected virtual void LoadRigidbody()
    {
        if (this.carRigidbody != null) return;
        this.carRigidbody = GetComponent<Rigidbody>();
        Debug.LogWarning($"CarCtlr: LoadRigidbody in {gameObject.name} ", gameObject);
    }
    protected virtual void LoadCarEnergy()
    {
        if (this.carEnergy != null) return;
        this.carEnergy = GetComponentInChildren<CarEnergy>();
        Debug.LogWarning($"CarCtlr: LoadCarEnergy in {gameObject.name} ", gameObject);
    }
}
