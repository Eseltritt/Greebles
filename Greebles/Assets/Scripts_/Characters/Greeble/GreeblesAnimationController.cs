using UnityEngine;

public class GreeblesAnimationController : BasicAnimator
{
    [SerializeField] private GameEvent Event_GreebleAttack;

    // Sends Attack
    public void StartAttack(){
        SetAnimatorTrigger("Attack");

        //Play on specific 
        Event_GreebleAttack?.Raise_WithoutParam(this);
    }

    public void AttackCompleted()
    {
        Event_GreebleAttack?.Raise_WithoutParam(this);
    }
}
