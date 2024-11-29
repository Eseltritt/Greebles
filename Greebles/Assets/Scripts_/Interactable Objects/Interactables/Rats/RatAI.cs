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

    [SerializeField] public GameEvent OnRatHidden;
    
    public bool fridgeOpen = false;

    public void SetUp(GameObject escapeDest, Transform[] roomDestinations)
    {
        _escapeDestination = escapeDest.transform;
        _roomDestinations = roomDestinations;
    }

    public override void Start()
    {
        base.Start();
        fridgeOpen = false;

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
        if (_currentState == state)
            return;

        if(state == IdleState)
            OnRatHidden?.Raise_SingleParam(this, false);

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

        if (_currentState == HideState)
            OnRatHidden?.Raise_SingleParam(this, true);

        _currentState.ArrivedAtTarget();
    }

    public void SetFridgeOpen(Component sender)
    {
        fridgeOpen = true;
    }

    public void SetFridgeClosed(Component sender)
    {
        fridgeOpen = false;
        
    }

    public override void MoveToDestination(Vector3 targetTransform)
    {
        base.MoveToDestination(targetTransform);

        /* if (_currentState == HideState)
            Debug.Log(gameObject.name + " is moving to hide spot " + targetTransform); */
    }
}
