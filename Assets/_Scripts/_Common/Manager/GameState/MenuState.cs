using UnityEngine.SceneManagement;

public class MenuState : IState
{
    private GameManager gm;

    public MenuState(GameManager gm) => this.gm = gm;

    public void Enter()
    {
        // Load MainMenuScene nếu chưa ở đó
        if (SceneManager.GetActiveScene().name != "MainMenuScene")
        {
            SceneManager.LoadScene("MainMenuScene");
        }
    }

    public void Execute() { }
    public void Exit() { }
}
