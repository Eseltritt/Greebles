using System;
using UnityEngine;

public abstract class BasicAnimator : MonoBehaviour
{
    [SerializeField] protected Animator _animator;

    protected void SetAnimatorTrigger(String name){
        _animator.SetTrigger(name);
    }

    protected void SetAnimatorBool(string name, bool value){
        _animator.SetBool(name, value);
    }

    protected void SetAnimatorFloat(string name, float value){
        _animator.SetFloat(name, value);
    }

    // Call Attack Event on specific Frame of Animation
}
