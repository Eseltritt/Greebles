using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.Collections;
using Unity.VisualScripting;
using System.Data.Common;

enum MoveState
{
    Idle,
    Clean,
    Collect,
    Place
}

public class HumanAI : NavAgent
{
    private MoveState _state = MoveState.Idle;

    private List<Transform> _RoomMoveTargets = new List<Transform>();
    private Transform _previousTarget;

    private List<InteractableObject> _totalMisplacedObjects = new List<InteractableObject>();
    private List<InteractableObject> _hitObjectsinSight = new List<InteractableObject>();
    private List<InteractableObject> _misplacedKnown = new List<InteractableObject>();

    /* private bool _hasHitObjectInSight = false; */

    private InteractableObject _targetInteractable;
    [SerializeField] private Transform _interactableHoldTransform;

    private bool _holdingInteractable = false;
    private bool _hasInteractableTarget = false;

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

        /* while (_hasHitObjectInSight)
        {
            
        } */
        /* if(!_hasInteractableTarget)
            StateUpdate(); */

        /* if (HasMisplacedTargets() && !_hasInteractableTarget)
            AssigneInteractableTarget(); */
    }

    #region AI

    void StateUpdate(){
        /* if (_hasInteractableTarget)
            return; */
        UpdateSeenMisplaced();

        if(HasSeenMisplacedObjects()){
            _state = MoveState.Clean;
            /* if (_hasInteractableTarget)
                    _state = MoveState.Place;
                else
                    _state = MoveState.Collect; */
        } else
            _state = MoveState.Idle;

        AssignNewTarget();
    }

    // Tracks all Misplaced Objects

    public void OnHitObjectMisplaced(Component sender){
        if (sender is HitObject)
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
        HitObject _interactable = other.gameObject?.GetComponent<HitObject>();

        if (_interactable == null)
            return;

        _hitObjectsinSight.Add(_interactable);
    }

    void OnTriggerExit(Collider other){
        HitObject _interactable = other.gameObject?.GetComponent<HitObject>();

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
                if(!hasTarget)
                    AssigneDefaultTarget();
            break;
            case MoveState.Clean:
                if (!_hasInteractableTarget){
                    _targetInteractable = GetClosestMisplaced();
                    AssigneInteractableTarget();
                }
            break;
            /* case MoveState.Collect:
                if (!_hasInteractableTarget)
                    AssigneInteractableTarget();
            break;
            case MoveState.Place:
                AssignInteractableInitialPosition();
            break; */
        }

        /* if (HasMisplacedTargets()){
            _state = MoveState.Collect;
            AssigneInteractableTarget();
        }else{
            _state = MoveState.Idle;
            AssigneDefaultTarget();
        } */

        hasTarget = true;
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

        hasTarget = true;
        targetPosition = _previousTarget.transform.position;
        MoveToDestination(speed);
    }

    private void AssigneInteractableTarget()
    {
        /* _interactableTarget = _misplacedObjects.Remove(); */
        
        targetPosition = _targetInteractable.transform.position;

        hasTarget = true;
        _hasInteractableTarget = true;

        MoveToDestination(speed);
    }

    private void AssignInteractableInitialPosition()
    {
        targetPosition = _targetInteractable.GetComponent<HitObject>().InitialPosition;

        hasTarget = true;
        _hasInteractableTarget = true;

        MoveToDestination(speed);
    }

    public override void DoActionOnArrival()
    {
        base.DoActionOnArrival();
        Debug.Log("arrived at location");
        // TO DO: play pick up animation


        switch (_state)
        {
            case MoveState.Idle:
                hasTarget = false;
                StateUpdate();
            break;
            case MoveState.Clean:
                Debug.Log("State = Clean");
                if (!_holdingInteractable)
                {
                    Debug.Log("holding Interactable");
                    PickUpInteractable();
                    AssignInteractableInitialPosition();
                }else{
                    PlaceInteractable();
                    Reset();

                    StateUpdate();
                }
                
                /* targetPosition = _interactableTarget.GetComponent<HitObject>().InitialPosition;
                MoveToDestination(speed); */
            /* break;
            case MoveState.Collect:
                PickUpInteractable();
                AssignInteractableInitialPosition(); */
                /* targetPosition = _interactableTarget.GetComponent<HitObject>().InitialPosition;
                MoveToDestination(speed); */
            /* break;
            case MoveState.Place:
                PlaceInteractable(); */

                /* _targetInteractable = null;
                _hasInteractableTarget = false;
                hasTarget = false; */
            break;
        }

        /* if (_state == MoveState.Idle){
            
        }else if(_state == MoveState.Collect){
            PickUpInteractable();
            _state = MoveState.Place;

            targetPosition = _interactableTarget.GetComponent<HitObject>().InitialPosition;
            MoveToDestination(speed);
        }else{
            PlaceInteractable();

            _interactableTarget = null;
            _hasInteractableTarget = false;
            hasTarget = false;

            AssignNewTarget();
        } */

        StateUpdate();
    }

    public void PickUpInteractable(){
        _targetInteractable.transform.parent = _interactableHoldTransform;

        _targetInteractable.transform.localPosition = Vector3.zero;

        _holdingInteractable = true;
    }

    public void PlaceInteractable(){
        _targetInteractable.transform.parent = null;

        HitObject _targetHitObject = _targetInteractable.GetComponent<HitObject>();
        _targetInteractable.transform.localPosition = _targetHitObject.InitialPosition;
        _targetInteractable.transform.rotation = _targetHitObject.InitialRotation;
        _targetHitObject.IsMisplaced = false;
        RemoveSeen(_targetInteractable);
        _totalMisplacedObjects.Remove(_targetInteractable);
    }

    public void Reset(){
        _targetInteractable = null;
        _hasInteractableTarget = false;
        _holdingInteractable = false;
        
        hasTarget = false;
    }

    #endregion
}
