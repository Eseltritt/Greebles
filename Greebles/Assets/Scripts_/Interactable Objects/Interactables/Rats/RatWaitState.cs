using UnityEngine;

public class RatWaitState : IState
{
    private RatAI _rat;
    private float _hideTime;
    private float _idleWaitTime;

    private float _totalWaitTime;
    private float _maxWaitTime;

    public RatWaitState(RatAI rat) :base()
    {
        _rat = rat;
    }

    public void ArrivedAtTarget()
    {
        throw new System.NotImplementedException();
    }

    public void Enter()
    {
        Debug.Log(this + " entered");
        _totalWaitTime = 0;

    }

    public void Exit()
    {
        Debug.Log(this + " exited");
    }

    public void StateUpdate()
    {
        // Check if Wait Time Reached, then enter Idle State
        if (_totalWaitTime < _maxWaitTime)
        {
            _totalWaitTime += 1 * Time.deltaTime;
        }
    }

    public bool WaitEnded(float time)
    {
        if (_totalWaitTime >= time)
            return true;

        return false;
    }
}
