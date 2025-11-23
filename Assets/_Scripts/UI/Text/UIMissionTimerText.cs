using UnityEngine;

public class UIMissionTimerText : BaseText
{
    [SerializeField] MissionTimer missionTimer;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadMissionTimer();
    }
    private void LoadMissionTimer()
    {
        if (missionTimer != null) return;
        this.missionTimer = GameObject.Find("MissionTimer").GetComponent<MissionTimer>();
        Debug.LogWarning($"UIMissionTimerText: LoadMissionTimer in {gameObject.name} ", gameObject);
    }
    protected override void Awake()
    {
        base.Awake();
        missionTimer.OnLimitTimer += UpdateTimer;
    }
    private void OnDestroy()
    {
        missionTimer.OnLimitTimer -= UpdateTimer;
    }
    protected void UpdateTimer(int timer)
    {
        uiText.text = "Time: " + timer.ToString();
    }
}
