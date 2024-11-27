using System;
using Unity.VisualScripting;
using UnityEngine;

public enum RatState
{
    Idle,
    Escape,
    Wait
}

public class RatAI : NavAgent
{
    private RatState _state;
    private StateMachine _stateMachine;
    private Transform _escapeDestination;
    private Transform _roomDestination;

    [SerializeField] private float _hideTime;
    [SerializeField] private float _idleWaitTime;
    [SerializeField] private float _idleWaitChance;
    [SerializeField] private float _idleUpdateRate;

    //StateMachine Test
    
    private Animator _animator;
    private bool hasBeenScared { get; set; }
    

    public RatAI(Transform escapeDest, Transform roomDest)
    {
        _escapeDestination = escapeDest;
        _roomDestination = roomDest;
    }

    void Start()
    {
        /* _state = RatState.Idle; */
        _stateMachine = new StateMachine();

        var ratIdleState = new RatIdleState(this, agent, _idleWaitTime, _idleWaitChance, _idleUpdateRate);
        var ratHideState = new RatHideState(this, agent, _escapeDestination);

        _stateMachine.AddTransition(ratIdleState, ratHideState, isScared());
        _stateMachine.AddTransition(ratHideState, ratIdleState, hideWaitEnded());

        Func<bool> isScared() => () => hasBeenScared == true;
        Func<bool> hideWaitEnded() => () => ratHideState.WaitEnded(_hideTime);
    }

    void OnEnable()
    {
        //Event when cat interacts with this Rat
    }

    void OnDisable()
    {
        //Event when cat interacts with this Rat
    }

    public override void Update() /* => _stateMachine.StateMachineUpdate(); */
    {

    }

    /* void SetState(RatState state)
    {
        switch (state)
        {
            case RatState.Idle:

            break;
            case RatState.Escape:

            break;
            case RatState.Wait:
                Invoke("EndWait", _waitTime);
            break;
        }
    } */

    void Escape()
    {
        navTarget = _escapeDestination.position;
        NavHasTarget = true;

        agent.SetDestination(navTarget);
    }

    public override void DoActionOnArrival()
    {
    }

    void EndWait()
    {

    }
}
