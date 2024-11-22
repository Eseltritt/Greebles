using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class AnimationPlayer : MonoBehaviour
{
    public string animationName;
    public UnityEvent OnAnimationComplete; // Public event for animation completion

    private Animator animator;
    private Coroutine animationCoroutine;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayAnimation()
    {
        if (animator != null)
        {
            animator.Play(animationName);
            animator.speed = 1f; // Adjust animation speed as needed

            if (animationCoroutine != null)
            {
                StopCoroutine(animationCoroutine);
            }
            animationCoroutine = StartCoroutine(WaitForAnimationCompletion());
        }
        else
        {
            Debug.LogError("Animator component not found on this GameObject.");
        }
    }

    private IEnumerator WaitForAnimationCompletion()
    {
        float animationLength = animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        yield return new WaitForSeconds(animationLength); 

        OnAnimationComplete.Invoke();
        animationCoroutine = null;
    }
}