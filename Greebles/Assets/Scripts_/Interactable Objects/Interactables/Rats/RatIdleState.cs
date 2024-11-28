using System.Collections.Generic;
using UnityEngine;

public class RatIdleState : IState
{
    RatAI _rat;
    
    //Wait Check
    private float _updateRate;
    private float _updateTimer;

    private float _idleWaitTime;
    private float _waitTimer;

    private float _idleWaitChance;
    private bool _isWaiting = false;
    private bool _hasTarget = false;

    public RatIdleState(RatAI rat, float waitTime, float waitChance, float idleUpdateRate)
    {
        _rat = rat;
        _idleWaitTime = waitTime;
        _idleWaitChance = waitChance;
        _updateRate = idleUpdateRate;
    }
    public void Enter()
    {
        _isWaiting = false;
        _hasTarget = false;
        _updateTimer = _updateRate;
    }

    public void Exit()
    {
    }

    public void StateUpdate()
    {
        if (_isWaiting)
        {
            _waitTimer -= 1 * Time.deltaTime;

            if (_waitTimer <= 0)
                _isWaiting = false;
        } else
        {
            if(!_hasTarget)
                FindNewTarget();
        }
    }

    void WaitCheck()
    {
        float chance = Random.Range(0f, 1f);

        if (chance <= _idleWaitChance)
        {
            float newWaitTime = Random.Range((float)_idleWaitTime/2, (float)_idleWaitTime);
            _waitTimer = newWaitTime;
            _isWaiting = true;
        }
    }

    void FindNewTarget()
    {
        List<Transform> targets = new List<Transform>();

        foreach (Transform transform in _rat._roomDestinations)
        {
            targets.Add(transform);
        }

        int rand = Random.Range(0, _rat._roomDestinations.Length-1);

        _hasTarget = true;
        _rat.MoveToDestination(_rat._roomDestinations[rand].position);
    }

    public void ArrivedAtTarget()
    {
        WaitCheck();
        _hasTarget = false;
    }
}
