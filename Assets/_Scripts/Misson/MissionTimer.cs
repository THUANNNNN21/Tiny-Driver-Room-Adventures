using System;
using UnityEngine;

public class MissionTimer : Cooldown
{
    [SerializeField] protected MissionCtrl missionCtrl;
    public event Action<int> OnLimitTimer;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadMissionCtrl();
    }
    protected override void LoadValues()
    {
        base.LoadValues();
        this.LoadMissionTime();
    }
    private void LoadMissionCtrl()
    {
        if (this.missionCtrl != null) return;
        this.missionCtrl = GetComponentInParent<MissionCtrl>();
        Debug.LogWarning($"MissionTimer: LoadMissionCtrl in {gameObject.name} ", gameObject);
    }
    protected virtual void LoadMissionTime()
    {
        MissionData missionData = this.missionCtrl.MissionManager.CurrentMission;
        if (missionData != null)
        {
            this.SetCooldownTime(missionCtrl.MissionManager.MissionLevel.GetTimeForLevel(missionData.timeLimit));
        }
    }
    protected override void ResetCooldown()
    {
        base.ResetCooldown();
        Time.timeScale = 0f;
    }
    public void EnterNewMission()
    {
        this.LoadMissionTime();
        this.StartCooldown();
    }
    protected override void UpdateCooldown()
    {
        base.UpdateCooldown();
        int timer = Mathf.RoundToInt(this.cooldownTime - this.currentTime);
        OnLimitTimer?.Invoke(timer);
    }
}
