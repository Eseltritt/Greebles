using System;
using UnityEngine;

public class HumanPlaceInteractableState : IState
{
    private Human _human;
    private float _waitTime;
    private float timer;
    private bool _hasTarget;
    private Animator _animatorController;
    private InteractableObject _targetInteractable;

    public HumanPlaceInteractableState(Human human, float waitTime, Animator animator)
    {
        _human = human;
        _waitTime = waitTime;
        _animatorController = animator;
    }

    public void Enter()
    {
        timer = _waitTime;
        _hasTarget = false;

        _targetInteractable = _human.targetInteractable;

        if (_targetInteractable is IHumanInteractable humanInteractable && humanInteractable.IsMisplaceable)
        {
            _targetInteractable.transform.parent = _human.InteractableHoldTransform;
            _targetInteractable.transform.localPosition = Vector3.zero;
        }
    }

    public void StateUpdate()
    {
        if (timer > 0)
        {
            timer -= 1* Time.deltaTime;
        }
        else
        {
            if (!_hasTarget)
            {
                _hasTarget = true;
                if (_targetInteractable is IHumanInteractable humanInteractable)
                {
                    if (humanInteractable.IsMisplaceable)
                    {
                        
                        _human.MoveToDestination(humanInteractable.InitialPosition);
                    }
                    else
                    {
                        humanInteractable.CorrectInteractable();
                        _human.SetState(_human.IdleState);
                    }
                }
                _hasTarget = true;
            }
        }
    }

    public void Exit()
    {
        
    }

    public void ArrivedAtTarget()
    {
        IHumanInteractable interactable = _targetInteractable as IHumanInteractable;
        _targetInteractable.transform.parent = null;
        interactable.CorrectInteractable();

        _human.SetState(_human.IdleState);
    }
}
