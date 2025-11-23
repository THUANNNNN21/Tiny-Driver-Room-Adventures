public class GameOverState : IState
{
    private GameManager gm;

    public GameOverState(GameManager gm) => this.gm = gm;

    public void Enter()
    {
        // Time.timeScale = 0;
        // UIManager.Instance.ShowGameOver(true);
    }

    public void Execute()
    {
        // Chờ Restart hoặc Back to Menu
    }

    public void Exit()
    {
        // UIManager.Instance.ShowGameOver(false);
        // Time.timeScale = 1;
    }
}
