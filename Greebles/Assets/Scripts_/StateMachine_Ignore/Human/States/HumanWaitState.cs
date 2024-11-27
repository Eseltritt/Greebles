using UnityEngine;

public class HumanWaitState : IState
{
    private Human _human;

    private float waitTime;

    public HumanWaitState(Human human)
    {
        _human = human;
    }

    public void StateUpdate()
    {
        if (waitTime > 0)
        {
            waitTime -= 1 * Time.deltaTime;
        } else
        {
            
        }
    }

    public void Enter()
    {
        _human.waitTime = waitTime;
    }

    public void Exit()
    {
        Debug.Log("Exited Wait State");
    }

    public void ArrivedAtTarget()
    {
        throw new System.NotImplementedException();
    }
}
