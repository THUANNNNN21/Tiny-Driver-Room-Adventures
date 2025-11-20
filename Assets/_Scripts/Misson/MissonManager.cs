using System.Collections.Generic;
using UnityEngine;

public class MissionManager : TMonoBehaviour
{
    [SerializeField] protected MissionCtrl missionCtrl;
    [SerializeField] protected List<MissionData> listMissions;
    [SerializeField] protected MissionData currentMission;
    public MissionData CurrentMission => currentMission;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadMissionCtrl();
        this.LoadListMissions();
        this.GetRandomMission();
    }
    private void LoadMissionCtrl()
    {
        if (this.missionCtrl != null) return;
        this.missionCtrl = GetComponentInParent<MissionCtrl>();
        Debug.LogWarning($"MissionManager: LoadMissionCtrl in {gameObject.name} ", gameObject);
    }
    private void LoadListMissions()
    {
        if (this.listMissions != null && this.listMissions.Count > 0) return;
        this.listMissions = new List<MissionData>(Resources.LoadAll<MissionData>("Data/MissionData"));
        Debug.LogWarning($"MissionManager: LoadListMissions in {gameObject.name} ", gameObject);
    }
    public void GetRandomMission()
    {
        int index = Random.Range(0, listMissions.Count);
        this.currentMission = listMissions[index];
    }
    // public void SetCurrentMission(MissionData missionData)
    // {
    //     this.currentMission = missionData;
    // }
}