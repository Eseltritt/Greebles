using UnityEngine;

public class PlayerAnimationManager : BasicAnimator
{
    /* private Animator _animator; */

    public PlayerAnimationManager(Animator animator)
    {
        _animator = animator;
    }

    public void StartWalking()
    {
        SetAnimatorTrigger("Trigger_Walk");
    }

    public void StartRunning()
    {
        SetAnimatorTrigger("Trigger_Run");
    }

    public void Hit()
    {
        SetAnimatorTrigger("Trigger_Hit");
    }

    public void Scratch()
    {
        SetAnimatorTrigger("Trigger_Scratch");
    }

    public void Idle()
    {
        SetAnimatorTrigger("Trigger_Idle");
    }
}
