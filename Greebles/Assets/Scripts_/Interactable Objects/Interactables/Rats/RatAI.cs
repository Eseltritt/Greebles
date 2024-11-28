using System;
using Unity.VisualScripting;
using UnityEngine;

public class RatAI : NavAgent
{
    private IState _currentState;
    public IState IdleState { get; private set; }
    public IState HideState { get; private set; }

    public Transform _escapeDestination;
    public Transform[] _roomDestinations;

    [SerializeField] private float _hideTime;
    [SerializeField] private float _idleWaitTime;
    [SerializeField] private float _idleWaitChance;
    [SerializeField] private float _idleUpdateRate;

    //StateMachine Test
    
    private Animator _animator;
    public bool fridgeOpen;

    public void SetUp(GameObject escapeDest, Transform[] roomDestinations)
    {
        _escapeDestination = escapeDest.transform;
        _roomDestinations = roomDestinations;
    }

    public override void Start()
    {
        base.Start();
        IdleState = new RatIdleState(this, _idleWaitTime, _idleWaitChance, _idleUpdateRate);
        HideState = new RatHideState(this, _escapeDestination, _hideTime);

        _currentState = IdleState;
        _currentState.Enter();
    }

    public override void Update()
    {
        base.Update();
        _currentState.StateUpdate();
    }

    public void SetState(IState state)
    {
        if (fridgeOpen)
            return;
            
        if (_currentState == state)
            return;

        _currentState.Exit();
        _currentState = state;
        _currentState.Enter();
    }

    public void Escape()
    {
        SetState(HideState);
    }

    public override void DoActionOnArrival()
    {
        base.DoActionOnArrival();
        _currentState.ArrivedAtTarget();

    }

    /* public override void MoveToDestination(Vector3 targetTransform)
    {
        base.MoveToDestination(targetTransform);
    } */
}
