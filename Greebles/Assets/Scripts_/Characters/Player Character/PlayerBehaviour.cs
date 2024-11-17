using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PlayerBehaviour : NavAgent
{
    private InteractableObject _targetInteractable;

    [SerializeField]
    private float _runSpeed;

    #region Init

    public override void Start()
    {
        base.Start();

        _runSpeed = speed * 4;
    }

    void OnEnable(){
        InputReader.onGameInput_DoubleClick += DoubleClickRegistered;
        InputReader.onGameInput_SingleClick += SingleClickRegistered;

        /* _agent = gameObject.GetComponent<NavMeshAgent>(); */
    }

    void OnDisable(){
        InputReader.onGameInput_DoubleClick -= DoubleClickRegistered;
        InputReader.onGameInput_SingleClick -= SingleClickRegistered;
    }

    #endregion

    public override void Update()
    {
        base.Update();
    }

    #region Click Events

    private void DoubleClickRegistered(InteractableObject _clickTarget, Vector3 _targetPosition){
        hasTarget = true;
        
        targetPosition = _targetPosition;

        if (_clickTarget.interactionType == InteractableType.Walk)
        {
            _targetInteractable = null;
            WalkObject walkObject = _clickTarget as WalkObject;

            if (walkObject.overrideDestination != null)
            {
                targetPosition = walkObject.overrideDestination.position;
            }
        } else{
            _targetInteractable = _clickTarget;
            targetPosition = _clickTarget.transform.position;
        }
        
        MoveToDestination(_runSpeed);
    }

    private void SingleClickRegistered(InteractableObject _clickTarget, Vector3 _targetPosition){
        hasTarget = true;

        targetPosition = _targetPosition;

        if (_clickTarget.interactionType == InteractableType.Walk)
        {
            // Attempt to cast _clickTarget to WalkObject
            WalkObject walkObject = _clickTarget as WalkObject;

            // Check if the cast was successful and overrideDestination is set
            if (walkObject.overrideDestination != null)
            {
                targetPosition = walkObject.overrideDestination.position;
            }
        } else{
            _targetInteractable = _clickTarget;
            targetPosition = _clickTarget.transform.position;
        }
        
        MoveToDestination(speed);
    }

    #endregion

    public override void DoActionOnArrival()
    {
        if(_targetInteractable != null)
            _targetInteractable.Catinteraction();

        // TO DO: Implement interaction effect

        // Play Animation
        // Call Interact on Anim End

        hasTarget = false;
    }
}