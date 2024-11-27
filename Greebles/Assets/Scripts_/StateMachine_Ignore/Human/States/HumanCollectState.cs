using UnityEngine;

public class HumanCollectState : IState
{
    public Human _human { get; private set; }
    private bool _hasTarget;
    private Animator _animatorController;
    
    public HumanCollectState(Human human, Animator animator) :base()
    {
        _human = human;
        _animatorController = animator;
    }

    public void StateUpdate()
    {
        if (!_hasTarget)
            AssignTargetInteractable();
    }

    public void AssignTargetInteractable()
    {
        InteractableObject target = _human.targetInteractable;

        if (_human.targetInteractable == null)
            _human.SetState(_human.IdleState);
        else
        {
            _human.MoveToDestination(target.transform.position);
            _hasTarget = true;
        }
            
    }

    public void Enter()
    {
        _hasTarget = false;
    }

    public void Exit()
    {
        
    }

    public void ArrivedAtTarget()
    {
        _human.SetState(_human.PlaceState);
    }
}
