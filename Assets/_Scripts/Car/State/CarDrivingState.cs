using UnityEngine;

public class CarDrivingState : IState
{
    protected CarStateMachine carStateMachine;
    protected CarCtlr carCtlr;

    public CarDrivingState(CarStateMachine carStateMachine)
    {
        this.carStateMachine = carStateMachine;
        this.carCtlr = carStateMachine.CarCtlr;
    }

    public void Enter()
    {
        // Code to execute when entering the driving state

    }

    public void Execute()
    {
        CarMovement carMovement = carCtlr.CarMovement;
        if (!carMovement.CheckIsMoving())
        {
            carStateMachine.ChangeState(carStateMachine.CarIdleState);
        }
        if (carCtlr.CarEnergy != null && carCtlr.CarEnergy.IsExhausted)
        {
            carStateMachine.ChangeState(carStateMachine.CarIdleState);
        }
    }

    public void Exit()
    {
        // Code to execute when exiting the driving state
    }
}