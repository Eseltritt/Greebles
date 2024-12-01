using System.Data.Common;
using NUnit.Framework.Constraints;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PlayerBehaviour : NavAgent
{
    [SerializeField] private InteractableObject _targetInteractable;

    [SerializeField] private Animator animator;
    private PlayerAnimationManager _animationController;

    private float _normalSpeed;
    private float _runSpeed;

    public GameObject _heldObject { get; private set; }
    public Transform holdObjectTransform;

    #region Init

    public override void Start()
    {
        base.Start();

        _animationController = new PlayerAnimationManager(animator);

        _normalSpeed = navSpeed;
        _runSpeed = navSpeed * 2;
    }

    void OnEnable(){
        InputReader.onGameInput_DoubleClick += DoubleClickRegistered;
        InputReader.onGameInput_SingleClick += SingleClickRegistered;
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
        NavHasTarget = true;
        
        navTarget = _targetPosition;

        if (_clickTarget.interactionType == InteractableType.Walk)
        {
            _targetInteractable = null;
            WalkObject walkObject = _clickTarget as WalkObject;

            if (walkObject.overrideDestination != null)
            {
                navTarget = walkObject.overrideDestination.position;
            }
        } else{
            _targetInteractable = _clickTarget;
            /* navTarget = _clickTarget.transform.position; */
        }
        
        _animationController.StartRunning();
        SetNavSpeed(_runSpeed);
        MoveToDestination(navTarget);
    }

    private void SingleClickRegistered(InteractableObject _clickTarget, Vector3 _targetPosition){
        NavHasTarget = true;

        navTarget = _targetPosition;

        if (_clickTarget.interactionType == InteractableType.Walk)
        {
            // Attempt to cast _clickTarget to WalkObject
            WalkObject walkObject = _clickTarget as WalkObject;

            // Check if the cast was successful and overrideDestination is set
            if (walkObject.overrideDestination != null)
            {
                navTarget = walkObject.overrideDestination.position;
            }
        } else{
            _targetInteractable = _clickTarget;
            navTarget = _clickTarget.transform.position;
        }
        
        _animationController.StartWalking();
        SetNavSpeed(_normalSpeed);
        MoveToDestination(navTarget);
    }

    #endregion

    public override void DoActionOnArrival()
    {
        if(_targetInteractable != null)
        {
            _targetInteractable.Catinteraction();
            if (_targetInteractable.interactionType == InteractableType.Hit)
                _animationController.Hit();

            if (_targetInteractable.interactionType == InteractableType.Scratch)
                _animationController.Scratch();

            _targetInteractable = null;
        }
        
        _animationController.Idle();
    }

    private void TriggerTargetAnimation()
    {
        _targetInteractable.Catinteraction();
        NavHasTarget = false;
    }

    public void OnObjectPickedUp(Component sender, object pickedUp) //pickedUp should be a bool that determines if the object is picked up or dropped
    {
        if (pickedUp is bool)
        {
            if ((bool)pickedUp)
            {
                _heldObject = sender.gameObject;
            }else
            {
                _heldObject = null;
            }
        }
    }

    public GameObject CheckoutPayload()
    {
        return _heldObject;
    }
}