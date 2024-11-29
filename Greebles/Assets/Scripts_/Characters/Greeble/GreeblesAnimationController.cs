using UnityEngine;

public class GreeblesAnimationController : BasicAnimator
{
    [SerializeField] private GameEvent OnHumanHealthChanged;

    // Sends Attack
    public void StartAttack(){
        SetAnimatorBool("Attacking", true);

        //Play on specific 
        /* OnHumanHealthChanged?.?(this, HumanHealthChangedValue.Dropped); */
    }

    public void StopAttacking()
    {
        SetAnimatorBool("Attacking", false);
    }

    public void AttackCompleted()
    {
        Debug.Log("attacked");
        OnHumanHealthChanged?.Raise_SingleParam(this, HumanHealthChangedValue.Dropped);
    }
}
