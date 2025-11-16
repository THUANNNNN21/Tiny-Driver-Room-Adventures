using UnityEngine;
public enum CarStateType
{
    None,
    Idle,
    Driving,
    Crash
}
public class CarStateMachine : StateMachine
{
    [SerializeField] protected CarCtlr carCtlr;
    public CarCtlr CarCtlr => carCtlr;
    public CarIdleState CarIdleState { get; private set; }
    public CarDrivingState CarDrivingState { get; private set; }
    public CarCrashState CarCrashState { get; private set; }
    [SerializeField] protected CarStateType currentStateType;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCarController();
    }
    protected virtual void LoadCarController()
    {
        if (this.carCtlr != null) return;
        this.carCtlr = GetComponentInParent<CarCtlr>();
    }
    protected override void InitializeStates()
    {
        this.CarIdleState = new CarIdleState(this);
        this.CarDrivingState = new CarDrivingState(this);
        this.CarCrashState = new CarCrashState(this);
        this.currentState = this.CarIdleState;
        this.currentState.Enter();
        this.UpdateState();
    }
    protected override void UpdateState()
    {
        if (this.currentState is CarIdleState)
        {
            this.currentStateType = CarStateType.Idle;
        }
        else if (this.currentState is CarDrivingState)
        {
            this.currentStateType = CarStateType.Driving;
        }
        else if (this.currentState is CarCrashState)
        {
            this.currentStateType = CarStateType.Crash;
        }
        else
        {
            this.currentStateType = CarStateType.None;
        }
    }
}
