using System;
using System.ComponentModel.Design;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public abstract class NavAgent : MonoBehaviour
{
    protected bool hasTarget;
    protected Vector3 targetPosition;
    protected NavMeshAgent agent;
    /* protected abstract float interactionDistance { get; } */

    public virtual void Start(){
        agent = gameObject.GetComponent<NavMeshAgent>();
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
        float dist = Vector3.Distance(targetPosition, agent.gameObject.transform.position);

        if (agent.stoppingDistance >= dist)
            return true;

        return false;
    }

    public void MoveToDestination(Vector3 _destination, float _speed){
        agent.destination = _destination;
        agent.speed = _speed;
    }

    public virtual void DoActionOnArrival(){
        agent.speed = 0;
        agent.isStopped = true;
    }
}
