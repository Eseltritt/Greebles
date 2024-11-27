using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public abstract class BaseState : IState
{
    protected GameObject gameObject;
    protected Transform transform;

    void Update() => StateUpdate();


    void SetState(IState _state)
    {
        /* if (state != null && state != _state)
        {
            state.Exit();

            state = _state;
            state.Enter();
        } */
    }

    public void Enter()
    {
        Debug.Log("enter");
    }

    public abstract void Exit();

    public abstract void StateUpdate();

    public void ArrivedAtTarget()
    {
        throw new System.NotImplementedException();
    }
}
