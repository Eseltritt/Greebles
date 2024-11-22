using UnityEngine;

public class Human : MonoBehaviour
{
    private StateMachine _stateMachine;
    public Transform Target { get; private set;}
    

    void Start()
    {
        _stateMachine = new StateMachine();

        var waitState = new HumanWaitState(this);
        var chaseState = new HumanChaseState(this);
        var idleWalkState = new HumanIdleWalkState(this);

        InitializeStates();
    }

    void InitializeStates()
    {
        /* _stateMachine.AddTransition(); */
    }

    void Update() => _stateMachine.StateMachineUpdate();
}
