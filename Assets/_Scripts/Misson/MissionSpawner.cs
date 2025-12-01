using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class MissionSpawner : TMonoBehaviour, IObserver
{
    [SerializeField] protected MissionCtrl missionCtrl;
    [SerializeField] protected GameObject holderCheckPoints;
    [SerializeField] protected List<GameObject> checkPoints = new();
    public List<GameObject> CheckPoints => checkPoints;
    [SerializeField] private DisableCPSubject disableCPSubject;
    [SerializeField] protected int nextCheckpointIndex = 0;
    public event Action OnCompleteSpawnCheckpoint;
    // public event Action OnEnterNewMission;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadMissionCtrl();
        this.LoadSubjectToObserve();
        this.LoadHolderCheckPoints();
        this.LoadCheckPoints();
        this.disableCPSubject.AddObserver(this);
    }
    private void LoadMissionCtrl()
    {
        if (this.missionCtrl != null) return;
        this.missionCtrl = GetComponentInParent<MissionCtrl>();
        Debug.LogWarning($"MissionSpawner: LoadMissionCtrl in {gameObject.name} ", gameObject);
    }
    private void LoadSubjectToObserve()
    {
        this.LoadDisableCPSubject();
    }
    private void LoadDisableCPSubject()
    {
        if (this.disableCPSubject != null) return;
        this.disableCPSubject = FindFirstObjectByType<DisableCPSubject>();
        Debug.LogWarning($"MissionSpawner: LoadDisableCPSubject in {gameObject.name} ", gameObject);
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
    public void OnSujectNotice()
    {
        this.SpawnNextCheckpoint();
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
        OnCompleteSpawnCheckpoint?.Invoke();
        Debug.Log("MissionSpawner: SpawnNextCheckpoint " + nextCheckpoint.checkpointType.ToString());
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
        MissionData newMission = this.GetNewMission();
        this.missionCtrl.MissionTimer.EnterNewMission();
        return newMission.listCheckpoints[this.nextCheckpointIndex];
    }
    protected MissionData GetNewMission()
    {
        MissionManager missionManager = this.missionCtrl.MissionManager;
        this.CompleteMission(missionManager);
        missionManager.GetRandomMission();
        MissionData newMission = missionManager.CurrentMission;
        missionManager.MissionLevel.SetLevel(missionManager.MissionLevel.Level + 1);
        // Không có mission mới thì return null
        if (newMission != null || newMission.listCheckpoints.Count != 0)
            return newMission;
        else return null;
    }
    protected void CompleteMission(MissionManager missionManager)
    {
        this.nextCheckpointIndex = 0;
        int scoreToAdd = missionManager.MissionLevel.GetScoreForLevel(missionManager.CurrentMission.rewardScores);
        ScoreManager.Instance.AddScore(scoreToAdd);
        int coinToAdd = missionManager.MissionLevel.GetCoinForLevel(missionManager.CurrentMission.rewardCoins);
        CoinManager.Instance.AddCoins(coinToAdd);
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
