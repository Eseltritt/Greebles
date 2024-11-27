using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.Collections;
using Unity.VisualScripting;
using System.Data.Common;
using UnityEngine.AI;

enum MoveState
{
    Idle,
    Clean,
    Collect,
    Place
}

public class HumanAI : NavAgent
{
    #region Private Variables

    private MoveState _state = MoveState.Idle;

    private List<Transform> _RoomMoveTargets = new List<Transform>();
    private Transform _previousTarget;

    private List<InteractableObject> _totalMisplacedObjects = new List<InteractableObject>();
    private List<InteractableObject> _hitObjectsinSight = new List<InteractableObject>();
    private List<InteractableObject> _misplacedKnown = new List<InteractableObject>();

    private InteractableObject _targetInteractable;
    [SerializeField] private Transform _interactableHoldTransform;

    private bool _holdingInteractable = false;
    private bool _hasInteractableTarget = false;

    [SerializeField] private Animator _animator;

    #endregion
    
    #region Public Variables

    [SerializeField] private float mySpeed;
        
    #endregion

    #region Init

    public override void Start(){
        base.Start();

        GetDefaultTargets();

        StateUpdate();       
    }

    private void GetDefaultTargets()
    {
        GameObject[] _moveTargets = GameObject.FindGameObjectsWithTag("HumanMoveTarget");

        foreach (var item in _moveTargets)
        {
            _RoomMoveTargets.Add(item.transform);
        }
    }

    #endregion

    public override void Update(){
        base.Update();
    }

    #region AI

    public void StateUpdate(){
        UpdateSeenMisplaced();

        if(HasSeenMisplacedObjects()){
            _state = MoveState.Clean;
        } else
            _state = MoveState.Idle;

        AssignNewTarget();
    }

    // Tracks all Misplaced Objects

    public void OnHitObjectMisplaced(Component sender){
        if (sender is CandleInteractable)
            _totalMisplacedObjects.Add(sender.gameObject.GetComponent<InteractableObject>());
    }

    bool HasMisplacedTargets(){
        bool _return = false;

        if (_totalMisplacedObjects.Count > 0)
            _return = true;

        return _return;
    }

    // Tracks all Objects in Human's Sight
    #region Track Visible

    void OnTriggerEnter(Collider other){
        CandleInteractable _interactable = other.gameObject?.GetComponent<CandleInteractable>();

        if (_interactable == null)
            return;

        _hitObjectsinSight.Add(_interactable);
    }

    void OnTriggerExit(Collider other){
        CandleInteractable _interactable = other.gameObject?.GetComponent<CandleInteractable>();

        if (_interactable == null)
            return;

        _hitObjectsinSight.Remove(_interactable);

        if (_hitObjectsinSight.Count > 0)
            return;
    }

    #endregion

    //Tracks Human-Known Misplaced Objects and assigns Closest 
    #region Known Misplaced

    void UpdateSeenMisplaced(){
        if (HasMisplacedTargets()){
            foreach (InteractableObject item in _totalMisplacedObjects)
            {
                if (_hitObjectsinSight.Contains(item) && !_misplacedKnown.Contains(item))
                    _misplacedKnown.Add(item);
            }
        }
    }

    bool HasSeenMisplacedObjects(){
        if (_misplacedKnown.Count > 0)
            return true;
        
        return false;
    }

    void RemoveSeen(InteractableObject _object){
        _misplacedKnown.Remove(_object);
    }

    InteractableObject GetClosestMisplaced(){
        InteractableObject closest = _misplacedKnown[0];
        
        for (int i = 1; i < _misplacedKnown.Count-1; i++)
        {
            float _closestDist = Vector3.Distance(gameObject.transform.position, closest.transform.position);
            InteractableObject next = _misplacedKnown[i];

            float _nextDist = Vector3.Distance(gameObject.transform.position, next.transform.position);
            if (_nextDist < _closestDist)
                closest = next;
        }

        return closest;
    }

    #endregion

    //Assigns Target

    void AssignNewTarget(){
        switch (_state)
        {
            case MoveState.Idle:
                if(!NavHasTarget)
                    AssigneDefaultTarget();
            break;
            case MoveState.Clean:
                if (!_hasInteractableTarget){
                    _targetInteractable = GetClosestMisplaced();
                    AssigneInteractableTarget();
                }
            break;
        }

        _animator.SetTrigger("Walk");
        NavHasTarget = true;
        agent.isStopped = false;
    }
        
    private void AssigneDefaultTarget()
    {
        if (_RoomMoveTargets.Count == 0)
            return;

        List<Transform> _targets = new List<Transform>();
        foreach (var item in _RoomMoveTargets)
        {
            _targets.Add(item);
        }

        for (int i = _targets.Count - 1; i >= 0; i--)
        {
            if (_targets[i] == _previousTarget)
            {
                _targets.RemoveAt(i);
            }
        }
        
        int rand = Random.Range(0, _targets.Count);

        _previousTarget = _targets[rand];

        NavHasTarget = true;
        navTarget = _previousTarget.transform.position;
        MoveToDestination(navTarget);
    }

    private void AssigneInteractableTarget()
    {      
        navTarget = _targetInteractable.transform.position;

        NavHasTarget = true;
        _hasInteractableTarget = true;

        MoveToDestination(navTarget);
    }

    private void AssignInteractableInitialPosition()
    {
        navTarget = _targetInteractable.GetComponent<CandleInteractable>().transform.position;

        NavHasTarget = true;
        _hasInteractableTarget = true;

        MoveToDestination(navTarget);
    }

    public override void DoActionOnArrival()
    {
        base.DoActionOnArrival();

        // TO DO: play pick up animation
        _animator.SetTrigger("Stop");
        
        switch (_state)
        {
            case MoveState.Idle:
                NavHasTarget = false;
                StateUpdate();
            break;
            case MoveState.Clean:
                if (!_holdingInteractable)
                {
                    PickUpInteractable();
                    AssignInteractableInitialPosition();
                    StateUpdate();
                }else{
                    _animator.SetTrigger("Drop");
                    Invoke("PlaceInteractable", 3);
                    Invoke("Reset", 3);
                    /* PlaceInteractable();
                    Reset(); */

                    /* StateUpdate(); */
                }
            break;
        }
    }

    public void PickUpInteractable(){
        _targetInteractable.transform.parent = _interactableHoldTransform;
        _targetInteractable.transform.localPosition = Vector3.zero;
        _targetInteractable.GetComponent<Rigidbody>().AddForce(Vector3.zero);

        _animator.SetBool("IsHolding", true);
        _animator.SetTrigger("Grab");
        _holdingInteractable = true;
    }

    public void PlaceInteractable(){
        _targetInteractable.transform.parent = null;

        CandleInteractable _targetHitObject = _targetInteractable.GetComponent<CandleInteractable>();
        _targetInteractable.transform.localPosition = _targetHitObject.InitialPosition;
        _targetInteractable.transform.rotation = _targetHitObject.InitialRotation;
        _targetHitObject.ToBeCorrected = false;
        RemoveSeen(_targetInteractable);
        _totalMisplacedObjects.Remove(_targetInteractable);
        _animator.SetBool("IsHolding", false);

        /* Reset(); */
    }

    public void Reset(){
        _targetInteractable = null;
        _hasInteractableTarget = false;
        _holdingInteractable = false;
        
        NavHasTarget = false;

        /* StateUpdate(); */
    }

    #endregion
}
