using System;
using UnityEngine;

public abstract class BasicAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    protected void SetAnimatorTrigger(String triggerName){
        _animator.SetTrigger(triggerName);
    }
}
