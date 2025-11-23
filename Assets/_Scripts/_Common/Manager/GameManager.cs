using UnityEngine;

public enum GameStateType
{
    None,
    MenuState,
    PlayState,
    PauseState,
    MissionCompleteState,
    GameOverState
}
public class GameManager : StateMachine
{
    private static GameManager instance;
    public static GameManager Instance => instance;

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

    public MenuState MenuState { get; private set; }
    public PlayState PlayState { get; private set; }
    public PauseState PauseState { get; private set; }
    public MissionCompleteState MissionCompleteState { get; private set; }
    public GameOverState GameOverState { get; private set; }
    [SerializeField] protected GameStateType currentStateType;
    protected override void InitializeStates()
    {
        MenuState = new MenuState(this);
        PlayState = new PlayState(this);
        PauseState = new PauseState(this);
        MissionCompleteState = new MissionCompleteState(this);
        GameOverState = new GameOverState(this);
        this.currentState.Enter();
        this.UpdateState();
    }
    protected override void UpdateState()
    {
        if (this.currentState is MenuState)
        {
            this.currentStateType = GameStateType.MenuState;
        }
        else if (this.currentState is PlayState)
        {
            this.currentStateType = GameStateType.PlayState;
        }
        else if (this.currentState is PauseState)
        {
            this.currentStateType = GameStateType.PauseState;
        }
        else if (this.currentState is MissionCompleteState)
        {
            this.currentStateType = GameStateType.MissionCompleteState;
        }
        else if (this.currentState is GameOverState)
        {
            this.currentStateType = GameStateType.GameOverState;
        }
        else
        {
            this.currentStateType = GameStateType.None;
        }
    }
}

