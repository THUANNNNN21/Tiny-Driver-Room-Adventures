using UnityEngine;

public class PlayState : IState
{
    private GameManager gm;

    public PlayState(GameManager gm) => this.gm = gm;

    public void Enter()
    {
        Time.timeScale = 1;
        // UIManager.Instance.ShowGameplayUI(true);
        // UIManager.Instance.ShowPauseMenu(false);
    }

    public void Execute()
    {
        // Cập nhật thời gian mission
        // MissionManager.Instance.UpdateMissionTimer();

        // if (MissionManager.Instance.IsMissionComplete)
        //     gm.ChangeState(gm.MissionCompleteState);

        // if (MissionManager.Instance.TimeOut)
        //     gm.ChangeState(gm.GameOverState);
    }

    public void Exit()
    {
        // Nothing
    }
}
