using UnityEngine;

public class UIManager : TMonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance => instance;

    protected override void Awake()
    {
        base.Awake();
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
    }

    public GameObject mainMenu;
    public GameObject gameplayUI;
    public GameObject pauseMenu;
    public GameObject missionCompleteUI;
    public GameObject gameOverUI;

    public void ShowMainMenu(bool b) => mainMenu.SetActive(b);
    public void ShowGameplayUI(bool b) => gameplayUI.SetActive(b);
    public void ShowPauseMenu(bool b) => pauseMenu.SetActive(b);
    public void ShowMissionComplete(bool b) => missionCompleteUI.SetActive(b);
    public void ShowGameOver(bool b) => gameOverUI.SetActive(b);
}
