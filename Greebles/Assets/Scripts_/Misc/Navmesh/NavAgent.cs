
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class NavAgent : MonoBehaviour
{
    protected bool hasTarget;
    protected Vector3 targetPosition; // Set this variable to target position in order to move the 
    protected NavMeshAgent agent;

    protected float speed;
    /* protected abstract float interactionDistance { get; } */

    public virtual void Start(){
        agent = gameObject.GetComponent<NavMeshAgent>();

        speed = agent.speed;
    }

    public virtual void Update(){
        if (hasTarget)
        {
            if (IsDistanceReached())
                DoActionOnArrival();
        }
            
    }

    private bool IsDistanceReached()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
            return true;

        return false;
    }

    public void MoveToDestination(float _speed){
        agent.destination = targetPosition;
        agent.speed = _speed;
    }

    public virtual void DoActionOnArrival(){
        agent.speed = 0;
        agent.isStopped = true;
    }
}
