public class MissionCompleteState : IState
{
    private GameManager gm;

    public MissionCompleteState(GameManager gm) => this.gm = gm;

    public void Enter()
    {
        // UIManager.Instance.ShowMissionComplete(true);
        // MissionManager.Instance.GiveReward();
    }

    public void Execute()
    {
        // Chờ người chơi bấm Next Mission
    }

    public void Exit()
    {
        // UIManager.Instance.ShowMissionComplete(false);
    }
}
