using UnityEngine;

public abstract class StateMachine : TMonoBehaviour
{
    protected IState currentState;
    public IState CurrentState => currentState;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.InitializeStates();
    }
    protected abstract void InitializeStates();

    protected void Update()
    {
        this.currentState?.Execute();
    }
    public virtual void ChangeState(IState newState)
    {
        if (this.currentState == newState) return;

        this.currentState?.Exit();
        this.currentState = newState;
        this.currentState?.Enter();
        this.UpdateState();
    }
    protected abstract void UpdateState();
}