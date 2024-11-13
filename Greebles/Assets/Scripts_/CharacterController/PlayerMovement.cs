using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    NavMeshAgent _agent;

    [SerializeField]
    private float _moveSpeed = 2;
    [SerializeField]
    private float _runSpeed = 5;

    void OnEnable(){
        InputReader.onGameInput_DoubleClick += DoubleClickRegistered;
        InputReader.onGameInput_SingleClick += SingleClickRegistered;

        _agent = gameObject.GetComponent<NavMeshAgent>();
    }

    void OnDisable(){
        InputReader.onGameInput_DoubleClick -= DoubleClickRegistered;
        InputReader.onGameInput_SingleClick -= SingleClickRegistered;
    }

    private void DoubleClickRegistered(Vector3 _target){
        MoveToDestination(_target, _runSpeed);
    }

    private void SingleClickRegistered(Vector3 _target){
        MoveToDestination(_target, _moveSpeed);
    }

    private void MoveToDestination(Vector3 _destination, float _speed){
        _agent.destination = _destination;
        _agent.speed = _speed;
    }
}