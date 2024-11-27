using System.Collections.Generic;
using UnityEngine;


public class Human : NavAgent
{
    public float waitTime = 5;

    private IState _currentState;

    public IState IdleState { get; private set; }
    public IState CollectState { get; private set; }
    public IState PlaceState { get; private set; }
    
    public Transform NavTarget { get; private set;}
    private List<Transform> _RoomMoveTargets = new List<Transform>();
    public List<InteractableObject> InteractablesInRange { get; private set; } = new List<InteractableObject>();
    public InteractableObject targetInteractable;

    public Transform InteractableHoldTransform;

    [SerializeField] private Animator _animator;

    public override void Start()
    {
        base.Start();

        GetDefaultRoomTargets();
        InitializeStates();
    }

    void InitializeStates()
    {
        IdleState = new HumanIdleWalkState(this, _RoomMoveTargets, waitTime, _animator);
        CollectState = new HumanCollectState(this, _animator);
        PlaceState = new HumanPlaceInteractableState(this, waitTime, _animator);

        _currentState = IdleState;
        _currentState.Enter();
    }

    private void GetDefaultRoomTargets()
    {
        GameObject[] _moveTargets = GameObject.FindGameObjectsWithTag("HumanMoveTarget");

        foreach (var item in _moveTargets)
        {
            _RoomMoveTargets.Add(item.transform);
        }
    }

    public override void Update(){
        base.Update();

        _currentState.StateUpdate();
    }

    public void SetState(IState state)
    {
        NavHasTarget = false;

        _currentState.Exit();        
        _currentState = state;
        _currentState.Enter();
    }

    // Human AI

    public override void DoActionOnArrival()
    {
        base.DoActionOnArrival();

        _currentState.ArrivedAtTarget();
    }

    void OnTriggerEnter(Collider other){
        InteractableObject _interactable = other.gameObject?.GetComponent<InteractableObject>();

        if (_interactable != null && _interactable is IHumanInteractable)
        {
            InteractablesInRange.Add(_interactable);
        }
    }

    void OnTriggerExit(Collider other){
        InteractableObject _interactable = other.gameObject?.GetComponent<InteractableObject>();

        if (_interactable != null && _interactable is IHumanInteractable)
        {
            InteractablesInRange.Remove(_interactable);
        }
    }
}