using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class GreeblesAI : NavAgent
{
    private GameObject _human;
    [SerializeField] float attackDistance = 5;
    [SerializeField] float positionUpdateTime = 3;

    // Follow Human

    public GameEvent GreebleAttack;

    public override void Start()
    {
        base.Start();

        _human = GameObject.FindGameObjectWithTag("Human");

        InvokeRepeating("UpdatePosition", 0, positionUpdateTime);
        
        hasTarget = true;
    }

    void UpdatePosition(){
        targetPosition = _human.transform.position;
        MoveToDestination(speed);
    }

    public override void DoActionOnArrival()
    {
        base.DoActionOnArrival();

        AttackHuman();
    }

    private void AttackHuman(){
        gameObject.transform.LookAt(_human.transform);

        GreebleAttack?.Raise_WithoutParam(this);
        // Play Animation

        // Damage Human
    }
}
