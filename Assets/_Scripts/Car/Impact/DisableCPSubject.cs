using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(CarImpact))]
public class DisableCPSubject : Subject
{
    [Header("Subject")]
    [SerializeField] private CarImpact subject;
    protected override void LoadSubject()
    {
        if (this.subject != null) return;
        this.subject = GetComponent<CarImpact>();
        Debug.LogWarning($"CarMovement: LoadSubject in {gameObject.name} ", gameObject);
    }
    protected override void Subcribe()
    {
        subject.OnCompleteCheckpoint += OnCheckpointComplete;
    }
    protected override void Unsubcribe()
    {
        subject.OnCompleteCheckpoint -= OnCheckpointComplete;
    }
    protected void OnCheckpointComplete()
    {
        this.NoticeObserver();
    }
}
