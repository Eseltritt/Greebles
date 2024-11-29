using System.Collections.Generic;
using UnityEngine;

public class HumanIdleWalkState : IState
{
    public Human _human { get; private set; }
    public bool _hasTarget = false;
    private float _waitTime;
    private float timer;
    private List<Transform> _RoomMoveTargets = new List<Transform>();
    private Transform _target;
    private Transform _previousTarget;
    private Animator _animatorController;

    public HumanIdleWalkState(Human human, List<Transform> targets, float waitTime, Animator animator)
    {
        _human = human;
        _RoomMoveTargets = targets;
        _waitTime = waitTime;
        _animatorController = animator;
    }

    public void StateUpdate()
    {
        CheckInteractablesInRange();

        if (timer > 0)
            timer -= 1* Time.deltaTime;
        else
        {
            if (!_hasTarget)
            {
                AssignRoomTarget();
            }
        }
    }

    public void AssignRoomTarget()
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

        _target = _targets[rand];
        
        _previousTarget = _target;

        _hasTarget = true;
        _human.MoveToDestination(_target.transform.position);
    }

    public void CheckInteractablesInRange()
    {
        foreach (var interactable in _human.InteractablesInRange)
        {
            if (interactable is IHumanInteractable humanInteractable && humanInteractable.ToBeCorrected)
            {
                Debug.Log("interactable to be corrected found");
                _human.targetInteractable = humanInteractable as InteractableObject;
                _human.SetState(_human.CollectState);
            }
        }
    }

    public void Enter()
    {
        _hasTarget = false;
        /* AssignRoomTarget(); */
    }

    public void Exit()
    {
        
    }

    public void ArrivedAtTarget()
    {
        timer = _waitTime;
        _hasTarget = false;
    }

    public void UpdateInteractables()
    {

    }
}
