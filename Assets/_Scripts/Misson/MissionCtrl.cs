using System.Collections.Generic;
using UnityEngine;

public class MissionCtrl : TMonoBehaviour
{
    [SerializeField] protected MissionSpawner missionSpawner;
    public MissionSpawner MissionSpawner => missionSpawner;
    [SerializeField] protected MissionManager missionManager;
    public MissionManager MissionManager => missionManager;
    [SerializeField] protected MissionTimer missionTimer;
    public MissionTimer MissionTimer => missionTimer;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadMissionSpawner();
        this.LoadMissionManager();
        this.LoadMissionTimer();
    }
    private void LoadMissionSpawner()
    {
        if (this.missionSpawner != null) return;
        this.missionSpawner = GetComponentInChildren<MissionSpawner>();
        Debug.LogWarning($"MissionCtrl: LoadMissionSpawner in {gameObject.name} ", gameObject);
    }
    private void LoadMissionManager()
    {
        if (this.missionManager != null) return;
        this.missionManager = GetComponentInChildren<MissionManager>();
        Debug.LogWarning($"MissionCtrl: LoadMissionManager in {gameObject.name} ", gameObject);
    }
    private void LoadMissionTimer()
    {
        if (this.missionTimer != null) return;
        this.missionTimer = GetComponentInChildren<MissionTimer>();
        Debug.LogWarning($"MissionCtrl: LoadMissionTimer in {gameObject.name} ", gameObject);
    }
    void Start()
    {
        this.MissionTimer.EnterNewMission();
        this.MissionSpawner.SpawnNextCheckpoint();
    }
}
