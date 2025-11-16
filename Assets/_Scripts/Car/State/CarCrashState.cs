using UnityEngine;

public class CarCrashState : IState
{
    protected CarStateMachine carStateMachine;
    protected CarCtlr carCtlr;
    float carshTime = 5f;
    float currentTime = 0;
    public CarCrashState(CarStateMachine carStateMachine)
    {
        this.carStateMachine = carStateMachine;
        this.carCtlr = carStateMachine.CarCtlr;
    }
    public void Enter()
    {
        // Code to execute when entering the crash state
        this.currentTime = 0;
        this.carCtlr.CarMovement.SetSpeed(0);
    }

    public void Execute()
    {
        this.currentTime += Time.deltaTime;
        if (this.currentTime >= this.carshTime)
        {
            carStateMachine.ChangeState(carStateMachine.CarIdleState);
        }
    }
    public void Exit()
    {
        // Code to execute when exiting the crash state
    }
}