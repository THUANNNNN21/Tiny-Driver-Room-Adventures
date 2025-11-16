using System.Collections.Generic;
using UnityEngine;

public class MissionCtrl : TMonoBehaviour
{
    [SerializeField] protected MissionSpawner missionSpawner;
    public MissionSpawner MissionSpawner => missionSpawner;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadMissionSpawner();
    }
    private void LoadMissionSpawner()
    {
        if (this.missionSpawner != null) return;
        this.missionSpawner = GetComponentInChildren<MissionSpawner>();
        Debug.LogWarning($"MissionCtrl: LoadMissionSpawner in {gameObject.name} ", gameObject);
    }
}
