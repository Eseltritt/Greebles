
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class NavAgent : MonoBehaviour
{
    private float stoppingDistance;
    protected bool hasTarget;
    protected Vector3 targetPosition; // Set this variable to target position in order to move the 
    protected NavMeshAgent agent;
    [SerializeField] protected float speed;

    /* protected abstract float interactionDistance { get; } */

    public virtual void Start(){
        agent = gameObject.GetComponent<NavMeshAgent>();
        if (stoppingDistance == 0)
            stoppingDistance = agent.stoppingDistance;
        else
            agent.stoppingDistance = stoppingDistance;
    }

    public virtual void Update(){
        if (hasTarget)
        {
            if (NavPathDestinationReached())
                DoActionOnArrival();
        }
            
    }

    protected bool NavPathDestinationReached()
    {
        if (agent.remainingDistance <= stoppingDistance && agent.remainingDistance != 0)
            return true;
        
        return false;
    }

    public void MoveToDestination(float _speed){
        agent.isStopped = false;
        agent.destination = targetPosition;
        agent.speed = _speed;
    }

    public virtual void DoActionOnArrival(){
        agent.speed = 0;
        agent.isStopped = true;
        /* agent.ResetPath(); */
    }
}
