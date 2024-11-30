using System.Collections;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Add this script to the object you want to call the animation from and chain it inside of the ScratchInteraction class to call this animation player active.

/// *How to Use:

/*Attach AnimationPlayer Script:
Attach an AnimationPlayer script to the GameObject that contains the desired animation (ie a door).
Set the animationName property to the name of the animation clip you want to play. (ie "Open")
Optional: Add listeners to the OnAnimationComplete event to trigger additional actions or animations after the current animation finishes.

Attach ScratchInteraction Script:
Attach a ScratchInteraction script to the GameObject that triggers the interaction.
Set the revealedObjectReactionType property to the appropriate enum value (e.g., Boop).

Triggering the Interaction:
When the ScratchInteraction is triggered, it checks the revealedObjectReactionType.

Playing the Animation:
Based on the revealedObjectReactionType, the ScratchInteraction script can:
Play the animation directly using the AnimationPlayer script.
Trigger a specific event or action, such as opening a door or playing another animation.

Chaining Animations*/
/// - To chain animations, use the `OnAnimationComplete` event in the `AnimationPlayer` script to trigger additional actions, such as playing another animation or activating a game event.
/// - Add listeners to the `OnAnimationComplete` event and execute the desired actions within those listeners.

/// </summary>
namespace NattyStuff
{
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
}