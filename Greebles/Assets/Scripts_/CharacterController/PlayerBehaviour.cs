using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PlayerBehaviour : NavAgent
{
    private InteractableObject _targetInteractable;

    [SerializeField]
    private float _moveSpeed = 2;
    [SerializeField]
    private float _runSpeed = 5;
    [SerializeField]
    private float _interactionDistance = 0.7f;
    protected override float interactionDistance => _interactionDistance;

    /* public PlayerBehaviour(){
        interactionDistance = 0.5f;
    } */

    void OnEnable(){
        InputReader.onGameInput_DoubleClick += DoubleClickRegistered;
        InputReader.onGameInput_SingleClick += SingleClickRegistered;

        /* _agent = gameObject.GetComponent<NavMeshAgent>(); */
    }

    void OnDisable(){
        InputReader.onGameInput_DoubleClick -= DoubleClickRegistered;
        InputReader.onGameInput_SingleClick -= SingleClickRegistered;
    }

    public override void Update()
    {
        base.Update();
    }

    private void DoubleClickRegistered(InteractableObject _newTarget, Vector3 _targetPosition){
        hasTarget = true;

        _targetInteractable = _newTarget;
        targetPosition = _targetPosition;

        if (_targetInteractable.interactionType != InteractableType.Walk){
            targetPosition = ((MonoBehaviour)_newTarget).transform.position;
        }
        
        MoveToDestination(targetPosition, _runSpeed);
    }

    private void SingleClickRegistered(InteractableObject _newTarget, Vector3 _targetPosition){
        hasTarget = true;

        _targetInteractable = _newTarget;
        targetPosition = _targetPosition;

        if(_targetInteractable.interactionType != InteractableType.Walk){
            targetPosition = ((MonoBehaviour)_newTarget).transform.position;
        }
        
        MoveToDestination(targetPosition, _moveSpeed);
    }

    public override void DoActionOnArrival()
    {
        _targetInteractable.Interact();

        // TO DO: Implement interaction effect

        hasTarget = false;
    }
}