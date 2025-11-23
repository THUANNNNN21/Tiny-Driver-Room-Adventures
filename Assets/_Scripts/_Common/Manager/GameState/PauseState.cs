public class PauseState : IState
{
    private GameManager gm;

    public PauseState(GameManager gm) => this.gm = gm;

    public void Enter()
    {
        // Time.timeScale = 0;
        // UIManager.Instance.ShowPauseMenu(true);
    }

    public void Execute()
    {
        // Listen nút Continue
    }

    public void Exit()
    {
        // UIManager.Instance.ShowPauseMenu(false);
        // Time.timeScale = 1;
    }
}
