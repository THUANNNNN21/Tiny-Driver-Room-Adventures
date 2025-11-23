using UnityEngine;

public class DirectionPointer : TMonoBehaviour
{
    [SerializeField] private CarCtlr carCtlr;
    [SerializeField] private MissionSpawner missionSpawner;
    [SerializeField] private float smoothRotation = 8f;     // Độ mượt xoay
    [SerializeField] private Transform checkpointTarget;
    protected override void Awake()
    {
        base.Awake();
        this.missionSpawner.OnCompleteSpawnCheckpoint += GetCheckpoint;
    }
    private void OnDestroy()
    {
        this.missionSpawner.OnCompleteSpawnCheckpoint -= GetCheckpoint;
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCarCtrl();
        this.LoadMissionSpawner();
    }
    void FixedUpdate()
    {
        this.RotateToCheckpoint();
    }
    private void LoadCarCtrl()
    {
        if (this.carCtlr != null) return;
        this.carCtlr = GetComponentInParent<CarCtlr>();
        Debug.LogWarning($"DirectionPointer: LoadDisableCPSubject in {gameObject.name} ", gameObject);
    }
    private void LoadMissionSpawner()
    {
        this.missionSpawner = carCtlr.MissionCtrl.MissionSpawner;
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
    public void GetCheckpoint()
    {
        GameObject CheckpointsHolder = GameObject.Find("CheckPoints");
        this.checkpointTarget = CheckpointsHolder.GetComponentInChildren<PooledObject>().transform;
    }
}
