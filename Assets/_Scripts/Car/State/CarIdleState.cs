using UnityEngine;

public class CarIdleState : IState
{
    protected CarStateMachine carStateMachine;
    protected CarCtlr carCtlr;
    protected CarMovement carMovement;

    public CarIdleState(CarStateMachine carStateMachine)
    {
        this.carStateMachine = carStateMachine;
        this.carCtlr = carStateMachine.CarCtlr;
        this.carMovement = carCtlr.CarMovement;
    }

    public void Enter()
    {
        this.carMovement.SetSpeed(0f);
        // Code to execute when entering the idle state
    }

    public void Execute()
    {
        if (this.carMovement.CheckIsMoving())
        {
            carStateMachine.ChangeState(carStateMachine.CarDrivingState);
        }
    }

    public void Exit()
    {
        // Code to execute when exiting the idle state
    }
}