using UnityEngine;
using UnityEngine.AI;

public class RatHideState : IState
{
    private RatAI _rat;
    private NavMeshAgent _navAgent;
    private Transform _hideDestination;

    private float _totalWaitTime;

    public RatHideState(RatAI rat, NavMeshAgent agent, Transform escapeDest) :base()
    {
        _rat = rat;
        _navAgent = agent;
        _hideDestination = escapeDest;
    }

    public void ArrivedAtTarget()
    {
        throw new System.NotImplementedException();
    }

    public void Enter()
    {
        Debug.Log(this + " entered");
        _totalWaitTime = 0;
        _navAgent.destination = _hideDestination.position;
    }

    public void Exit()
    {
        Debug.Log(this + " exited");
    }

    public void StateUpdate()
    {
        if(HideAreaReached())
        {
            _totalWaitTime += 1 * Time.deltaTime;
        }
    }

    public bool WaitEnded(float time)
    {
        if (HideAreaReached() && _totalWaitTime >= time)
            return true;

        return false;
    }

    private bool HideAreaReached()
    {
        float dist = Vector3.Distance(_navAgent.transform.position, _hideDestination.position);
        
        if (_navAgent.remainingDistance <= _navAgent.stoppingDistance && _navAgent.remainingDistance != 0)
            return true;
        
        return false;
    }
}
