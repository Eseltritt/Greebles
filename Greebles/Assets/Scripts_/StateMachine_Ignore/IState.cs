using UnityEngine;

public interface IState
{
    public void StateUpdate();

    public void Enter();

    public void Exit();

    public void ArrivedAtTarget();
}
