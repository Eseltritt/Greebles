using UnityEngine;
using UnityEngine.AI;

public class RatIdleState : IState
{
    RatAI _rat;
    NavMeshAgent _navAgent;
    private float _idleWaitTime;
    private float _idleWaitChance;
    private float _updateRate;

    private bool _isWaiting = false;
    private float _totalWaitTime;
    private float _updateTime;

    public RatIdleState(RatAI rat, NavMeshAgent agent, float waitTime, float waitChance, float updateRate) :base()
    {
        _rat = rat;
        _navAgent = agent;
        _idleWaitTime = waitTime;
        _idleWaitChance = waitChance;
        _updateRate = updateRate;
    }
    public void Enter()
    {
        Debug.Log(this + " entered");
        _isWaiting = false;
    }

    public void Exit()
    {
        Debug.Log(this + " exited");
    }

    public void StateUpdate()
    {
        if (_isWaiting)
        {
            _totalWaitTime += 1 * Time.deltaTime;
            if (_totalWaitTime >= _idleWaitTime)
            {
                IdleUpdate();
            }
        } else
        {
            _updateTime += 1 * Time.deltaTime;
            if (_updateTime >= _updateRate)
            {
                IdleUpdate();
            }
        }
    }

    private void IdleUpdate()
    {
        float chance = Random.Range(0, 1);
        if (chance <= _idleWaitChance)
        {
            Stop();
            _isWaiting = true;
        }
    }

    private void Stop()
    {
        _navAgent.isStopped = true;
    }

    private void SetDestination()
    {
        _navAgent.isStopped = false;
    }

    public void ArrivedAtTarget()
    {
        throw new System.NotImplementedException();
    }
}
