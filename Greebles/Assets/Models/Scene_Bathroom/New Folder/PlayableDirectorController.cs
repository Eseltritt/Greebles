using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

namespace NattyStuff
{
    /// <summary>
    /// This script allows you to control a Playable Director and trigger events
    /// when the animation starts and ends.
    /// </summary>
    public class PlayableDirectorController : MonoBehaviour
    {
        /// <summary>
        /// The Playable Director to control.
        /// </summary>
        [Tooltip("Assign the Playable Director you want to control.")]
        public PlayableDirector playableDirector;

        /// <summary>
        /// Event triggered when the animation starts.
        /// </summary>
        [Tooltip("Add functions or events to be called when the animation starts.")]
        public UnityEvent onAnimationStart;

        /// <summary>
        /// Event triggered when the animation ends.
        /// </summary>
        [Tooltip("Add functions or events to be called when the animation ends.")]
        public UnityEvent onAnimationEnd;

        private void OnEnable()
        {
            playableDirector.stopped += OnAnimationEnd;
            Debug.Log("Subscribed to PlayableDirector's stopped event");
        }

        private void OnDisable()
        {
            playableDirector.stopped -= OnAnimationEnd;
            Debug.Log("Unsubscribed from PlayableDirector's stopped event");
        }

        /// <summary>
        /// Starts playing the animation on the assigned Playable Director.
        /// </summary>
        public void StartAnimation()
        {
            Debug.Log("Starting animation");
            playableDirector.Play();
            onAnimationStart.Invoke();
        }

        public void ResetAnimation()
        {
            Debug.Log("Resetting animation");
            playableDirector.time = 0;
        }

        private void OnAnimationEnd(PlayableDirector director)
        {
            if (director == playableDirector) // Ensure it's the assigned director
            {
                Debug.Log("Animation ended");
                onAnimationEnd.Invoke();
            }
        }
    }
}