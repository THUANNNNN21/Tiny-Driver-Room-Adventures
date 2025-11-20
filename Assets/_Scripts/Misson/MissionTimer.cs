using UnityEngine;

public class MissionTimer : Cooldown
{
    [SerializeField] protected MissionCtrl missionCtrl;
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
            this.SetCooldownTime(missionData.timeLimit);
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
}
