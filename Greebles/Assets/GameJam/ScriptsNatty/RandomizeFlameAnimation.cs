using UnityEngine;

namespace NattyStuff
{
    public class RandomizeFlameAnimation : MonoBehaviour
    {
        private Animator animator;
        // Reference to the Animator component

        void Start()
        {
            animator = GetComponent<Animator>();

            if (animator == null)
            {
                Debug.LogError("Animator component not found on this GameObject.");
                return;
            }


            // Randomize the starting time of the animation
            float randomStartTime = Random.Range(0f, 1f);
            animator.Play("FlameAnimation", 0, randomStartTime); // Replace "FlameAnimation" with your animation's name
        }
    }
}