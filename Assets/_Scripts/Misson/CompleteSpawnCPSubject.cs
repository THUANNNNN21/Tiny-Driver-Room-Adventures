using UnityEngine;

[RequireComponent(typeof(MissionSpawner))]
public class CompleteSpawnCPSubject : Subject
{
    [Header("Subject")]
    [SerializeField] private MissionSpawner subject;
    protected override void LoadSubject()
    {
        if (this.subject != null) return;
        this.subject = GetComponent<MissionSpawner>();
        Debug.LogWarning($"MissionSpawner: LoadSubject in {gameObject.name} ", gameObject);
    }
    protected override void Subcribe()
    {
        subject.OnCompleteSpawnCheckpoint += OnCompleteSpawnCheckpoint;
    }
    protected override void Unsubcribe()
    {
        subject.OnCompleteSpawnCheckpoint -= OnCompleteSpawnCheckpoint;
    }
    protected void OnCompleteSpawnCheckpoint()
    {
        this.NoticeObserver();
    }
}
