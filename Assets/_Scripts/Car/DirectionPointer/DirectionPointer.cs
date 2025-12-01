using UnityEngine;

public class DirectionPointer : TMonoBehaviour, IObserver
{
    [SerializeField] private CarCtlr carCtlr;
    [SerializeField] private float smoothRotation = 8f;     // Độ mượt xoay
    [SerializeField] private Transform checkpointTarget;
    [SerializeField] private CompleteSpawnCPSubject completeSpawnCPSubject;
    [SerializeField] private GameObject checkpointsHolder;

    // protected override void Awake()
    // {
    //     base.Awake();
    //     this.missionSpawner.OnCompleteSpawnCheckpoint += GetCheckpoint;
    // }
    // private void OnDestroy()
    // {
    //     this.missionSpawner.OnCompleteSpawnCheckpoint -= GetCheckpoint;
    // }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCarCtrl();
        this.LoadCPHolder();
        this.LoadSubjectToObserve();
        this.completeSpawnCPSubject.AddObserver(this);
    }
    private void LoadSubjectToObserve()
    {
        if (this.completeSpawnCPSubject != null) return;
        this.completeSpawnCPSubject = FindFirstObjectByType<CompleteSpawnCPSubject>();
        Debug.LogWarning($"DirectionPointer: LoadCompleteSpawnCPSubject in {gameObject.name} ", gameObject);
    }
    private void LoadCarCtrl()
    {
        if (this.carCtlr != null) return;
        this.carCtlr = GetComponentInParent<CarCtlr>();
        Debug.LogWarning($"DirectionPointer: LoadDisableCPSubject in {gameObject.name} ", gameObject);
    }
    private void LoadCPHolder()
    {
        if (this.checkpointsHolder != null) return;
        this.checkpointsHolder = GameObject.Find("CheckPoints");
        Debug.LogWarning($"DirectionPointer: LoadCPHolder in {gameObject.name} ", gameObject);
    }
    void FixedUpdate()
    {
        this.RotateToCheckpoint();
    }
    protected void RotateToCheckpoint()
    {
        if (checkpointTarget != null)
        {
            Vector3 direction = checkpointTarget.position - transform.position;
            direction.y = 0; // Không xoay theo trục Y nếu game top-down

            Quaternion targetRot = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, Time.deltaTime * smoothRotation);
        }
    }
    public void OnSujectNotice()
    {
        this.GetCheckpoint();
        Debug.Log("DirectionPointer: OnSujectNotice");
    }
    public void GetCheckpoint()
    {
        this.checkpointTarget = checkpointsHolder.GetComponentInChildren<PooledObject>().transform;
    }
}
