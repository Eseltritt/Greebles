using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class GreeblesAI : NavAgent
{
    private GameObject _human;
    [SerializeField] float attackRange = 10;
    [SerializeField] private float attackRate = 3;
    private bool _isAttacking;

    [SerializeField] float positionUpdateTime = 3;

    // Follow Human

    [SerializeField] private GreeblesAnimationController animationController;

    public override void Start()
    {
        base.Start();
        //animationController = gameObject.GetComponent<BasicAnimator>();

        _human = GameObject.FindGameObjectWithTag("Human");
        
        StartChasing();

        hasTarget = true;
    }

    public override void Update(){
        if (!_isAttacking)
        {
            base.Update();
        }else{
            if (IsOutOfReach(_human, attackRange))
            {
                StartChasing();
            }
        }
    }

    public override void DoActionOnArrival()
    {
        base.DoActionOnArrival();

        StartAttacking();
    }

    private void StartChasing(){
        _isAttacking = false;
        CancelInvoke("AttackHuman");
        InvokeRepeating("FollowHuman", 0, positionUpdateTime);
    }

    private void StartAttacking(){
        _isAttacking = true;
        CancelInvoke("FollowHuman");
        InvokeRepeating("AttackHuman", 0, attackRate);
    }

    private bool IsOutOfReach(GameObject _other, float _range){
        bool outOfReach = false;
        
        float dist = Vector3.Distance(_other.transform.position, this.transform.position);

        if (dist > _range){
            outOfReach = true;
        }

        return outOfReach;
    }

    private void AttackHuman(){

        gameObject.transform.LookAt(_human.transform);

        animationController.StartAttack();
    }

    void FollowHuman(){
        targetPosition = _human.transform.position;
        MoveToDestination(speed);
    }
}
