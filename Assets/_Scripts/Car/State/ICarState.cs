using UnityEngine;

public interface ICarState
{
    void Enter();
    void Execute();
    void Exit();
}