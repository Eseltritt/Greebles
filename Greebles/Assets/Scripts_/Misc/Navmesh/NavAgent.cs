
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class NavAgent : MonoBehaviour
{
    [SerializeField] private float navStoppingDistance;
    public bool NavHasTarget { get; protected set; }
    protected Vector3 navTarget;
    protected NavMeshAgent agent;
    [SerializeField] protected float navSpeed;

    bool destReached = false;


    public virtual void Start(){
        agent = gameObject.GetComponent<NavMeshAgent>();
        if (navStoppingDistance == 0)
            navStoppingDistance = agent.stoppingDistance;
        else
            agent.stoppingDistance = navStoppingDistance;
    }

    public virtual void Update(){
        if (NavHasTarget)
        {
            if (NavPathDestinationReached() && !destReached)
            {
                destReached = true;
                DoActionOnArrival();
            }
        }
            
    }

    protected bool NavPathDestinationReached()
    {
        if (agent.remainingDistance <= navStoppingDistance && agent.remainingDistance != 0)
            return true;
        
        return false;
    }

    public virtual void MoveToDestination(Vector3 targetTransform){
        NavHasTarget = true;
        destReached = false;
        agent.isStopped = false;
        agent.speed = navSpeed;
        agent.destination = targetTransform;
    }

    public void SetNavSpeed(float speed)
    {
        navSpeed = speed;
    }

    public virtual void DoActionOnArrival(){
        destReached = true;
        NavHasTarget = false;
        agent.speed = 0;
        agent.isStopped = true;
    }
}
