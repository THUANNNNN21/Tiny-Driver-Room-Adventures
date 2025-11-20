using System.Collections.Generic;
using UnityEngine;

public class MissionSpawner : TMonoBehaviour
{
    [SerializeField] protected MissionCtrl missionCtrl;
    [SerializeField] protected GameObject holderCheckPoints;
    [SerializeField] protected List<GameObject> checkPoints = new();
    public List<GameObject> CheckPoints => checkPoints;
    [SerializeField] private CarImpact carImpact;
    [SerializeField] protected int nextCheckpointIndex = 0;
    protected override void Awake()
    {
        base.Awake();
        carImpact.OnCompleteCheckpoint += SpawnNextCheckpoint;
    }
    private void OnDestroy()
    {
        carImpact.OnCompleteCheckpoint -= SpawnNextCheckpoint;
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadMissionCtrl();
        this.LoadSubjectToObserve();
        this.LoadHolderCheckPoints();
        this.LoadCheckPoints();
    }
    private void LoadMissionCtrl()
    {
        if (this.missionCtrl != null) return;
        this.missionCtrl = GetComponentInParent<MissionCtrl>();
        Debug.LogWarning($"MissionSpawner: LoadMissionCtrl in {gameObject.name} ", gameObject);
    }
    private void LoadSubjectToObserve()
    {
        this.LoadCarImpact();
    }
    protected virtual void LoadCarImpact()
    {
        if (this.carImpact != null) return;
        this.carImpact = FindFirstObjectByType<CarImpact>();
        Debug.LogWarning($"MissionSpawner: LoadCarImpact in {gameObject.name} ", gameObject);
    }
    private void LoadHolderCheckPoints()
    {
        if (this.holderCheckPoints != null) return;
        this.holderCheckPoints = transform.parent.Find("CheckPoints").gameObject;
        Debug.LogWarning($"MissionSpawner: LoadHolderCheckPoints in {gameObject.name} ", gameObject);
    }
    private void LoadCheckPoints()
    {
        if (this.checkPoints != null && this.checkPoints.Count > 0) return;
        this.checkPoints = new List<GameObject>(Resources.LoadAll<GameObject>("MissionPrefab"));
    }
    public void SpawnNextCheckpoint()
    {
        MissionData missionData = this.missionCtrl.MissionManager.CurrentMission;
        CheckpointData nextCheckpoint = this.GetNextCheckpoint(missionData);
        if (nextCheckpoint == null) return;
        GameObject checkpointPrefab = this.GetCheckpointPrefab(nextCheckpoint.checkpointType.ToString());
        if (checkpointPrefab == null) return;
        GameObject obj = PoolManager.Instance.Get(checkpointPrefab);
        obj.transform.SetParent(this.holderCheckPoints.transform);
        obj.transform.position = nextCheckpoint.position;
        this.nextCheckpointIndex++;
    }
    protected CheckpointData GetNextCheckpoint(MissionData missionData)
    {
        for (int i = 0; i < missionData.listCheckpoints.Count; i++)
        {
            if (i == this.nextCheckpointIndex)
            {
                return missionData.listCheckpoints[i];
            }
        }
        this.nextCheckpointIndex = 0;
        this.missionCtrl.MissionManager.GetRandomMission();
        MissionData newMission = this.missionCtrl.MissionManager.CurrentMission;

        // Không có mission mới thì return null
        if (newMission == null || newMission.listCheckpoints.Count == 0)
            return null;

        // lấy checkpoint đầu tiên của mission mới
        this.missionCtrl.MissionTimer.EnterNewMission();
        return newMission.listCheckpoints[this.nextCheckpointIndex];
    }
    protected GameObject GetCheckpointPrefab(string checkpointType)
    {
        foreach (GameObject cp in this.checkPoints)
        {
            if (cp.transform.name == checkpointType)
            {
                return cp;
            }
        }
        Debug.LogWarning("Checkpoint type not found: " + checkpointType);
        return null;
    }
    // protected GameObject GetRandomCheckPoint()
    // {
    //     int index = Random.Range(0, this.checkPoints.Count);
    //     GameObject randomCheckPoint = this.checkPoints[index];
    //     Debug.Log("Random CheckPoint: " + randomCheckPoint.name);
    //     return randomCheckPoint;
    // }
    // protected Vector3 GetRandomPosition()
    // {
    //     float x = Random.Range(-100f, 100f);
    //     float y = 0f;
    //     float z = Random.Range(-100f, 100f);
    //     return new Vector3(x, y, z);
    // }
}
