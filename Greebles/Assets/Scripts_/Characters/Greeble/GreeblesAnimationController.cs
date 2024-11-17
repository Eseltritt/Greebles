using UnityEngine;

public class GreeblesAnimationController : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void PlayAttackAnim(){
        _animator.SetTrigger("Attack");
    }
}
