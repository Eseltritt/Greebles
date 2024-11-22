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

    public void Exit()
    {
        Debug.Log("exit");
    }

    public void StateUpdate()
    {
        Debug.Log("tick => " + gameObject.name);
    }
}
