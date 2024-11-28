using UnityEngine;
using UnityEngine.AI;

public class RatHideState : IState
{
    private RatAI _rat;
    private NavMeshAgent _navAgent;
    private Transform _hideDestination;

    private float _hideTime;
    private float _timer;
    private bool _isHideDestReached = false;

    public RatHideState(RatAI rat, Transform escapeDest, float hideTime)
    {
        _rat = rat;
        _hideDestination = escapeDest;
        _hideTime = hideTime;
    }

    public void Enter()
    {
        _timer = _hideTime;
        _isHideDestReached = false;
        
        _rat.MoveToDestination(_hideDestination.position);
    }

    public void Exit()
    {
    }

    public void StateUpdate()
    {
        if(_isHideDestReached)
        {
            _timer -= 1 * Time.deltaTime;

            if (_timer <= 0)
                _rat.SetState(_rat.IdleState);
        }
    }

    public void ArrivedAtTarget()
    {
        _isHideDestReached = true;
    }
}
