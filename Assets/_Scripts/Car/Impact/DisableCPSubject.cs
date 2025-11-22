using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(CarImpact))]
public class DisableCPSubject : TMonoBehaviour
{
    [Header("Subject")]
    [SerializeField] private CarImpact subject;

    [Header("Observers")]
    [SerializeField] private List<IDisableCPObserver> observers = new();
    protected override void Awake()
    {
        base.Awake();
        subject.OnCompleteCheckpoint += OnCheckpointComplete;
    }
    private void OnDestroy()
    {
        subject.OnCompleteCheckpoint -= OnCheckpointComplete;
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSubject();
    }
    protected void LoadSubject()
    {
        if (this.subject != null) return;
        this.subject = GetComponent<CarImpact>();
        Debug.LogWarning($"CarMovement: LoadSubject in {gameObject.name} ", gameObject);
    }
    public void AddObserver(IDisableCPObserver observer)
    {
        this.observers.Add(observer);
        Debug.Log("Add observer: " + observer);
    }
    protected void OnCheckpointComplete()
    {
        foreach (IDisableCPObserver observer in this.observers)
        {
            observer.OnCheckpointComplete();
        }
    }
}