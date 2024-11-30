using UnityEngine;
using UnityEngine.Events;
using Unity.Cinemachine;
using System.Collections;
using TMPro;

namespace NattyStuff
{
    public class RoomTransitionTrigger : MonoBehaviour
    {  
        
        
        //incomplete script as it needs to talk to navmesh to determin destination camera to trigger (and to not trigger at the entry of that doorway)
        //TODO - link to nave mesh to determin next available camera at destination point- and also not trigger the return doorway by accident

        /// <summary>
        /// The Cinemachine Virtual Camera to activate when entering the new room.
        /// </summary>
        [Tooltip("The next camera to switch to")]
        public CinemachineVirtualCamera targetCamera;

        /// <summary>
        /// The current Cinemachine Virtual Camera to deactivate.
        /// </summary>
        [Tooltip("The current main camera")]
        public CinemachineVirtualCamera mainCamera;

        [Tooltip("how long to wait before switiching camera")]
        public float transitionDelay;

        /// <summary>
        /// An event triggered when the room transition starts. 
        /// Use this to perform actions specific to the character or room, such as playing animations, loading new content, or triggering dialogue.
        /// </summary>
        [Tooltip("Event triggered when the room transition starts. Use it to perform actions like playing animations, loading new content")]
        public UnityEvent OnRoomTransition; //see at end example use

        [Tooltip("A unity event for when a player cat or greenle trigger this doorway")]
        public UnityEvent OnCharacterInteracted;


        [Tooltip("Animator component for the door opening animation")]
        public Animator doorAnimator;

        [Tooltip("Whether to make trigger visible during gameplay")]
        public bool showTrigger = false;
        private MeshRenderer meshRenderer;
        private TextMeshPro textMesh;

        private void Start()
        {
            gameObject.tag = "RoomTransitionTrigger"; // Ensure the tag is set

            meshRenderer = GetComponent<MeshRenderer>();
            textMesh = GetComponentInChildren<TextMeshPro>();

            if (meshRenderer != null && !showTrigger)
            {
                meshRenderer.enabled = false;

            }
            if (textMesh != null && !showTrigger)
            {
                textMesh.enabled = false;
            }
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Player entered the trigger zone.");

                if (doorAnimator != null)
                {
                    Debug.Log("Triggering door opening animation.");
                    doorAnimator.Play("Armature|Open");
                    StartCoroutine(TriggerCameraTransition());
                }
                else
                {
                    Debug.LogError("Door animator is not assigned. Please assign the correct animator component.");
                   
                   
                }
            }
        }


        IEnumerator TriggerCameraTransition()
        {
            Debug.Log($"Player at doorway Triggering camera transition with a {transitionDelay} second delay.");

            if (targetCamera != null)
            {
                Debug.Log("Target cinemachine camera set and transition with delay initiated.");

                // Optional delay
                yield return new WaitForSeconds(transitionDelay); // Adjust the delay time as needed
            }
            else
            {
                Debug.LogWarning("Target destination cinemachine camera is not assigned. Skipping camera transition.");
            }

            OnRoomTransition.Invoke();
            yield return null;
        }


     
    }
}


/*

public class CharacterController : MonoBehaviour
{
    public Animator animator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RoomTransitionTrigger"))
        {
            RoomTransitionTrigger roomTransitionTrigger = other.GetComponent<RoomTransitionTrigger>();
            if (roomTransitionTrigger != null)
            {
                // Trigger the character's door opening animation
                animator.SetTrigger("OpenDoorTrigger");

                // Trigger the room transition
                roomTransitionTrigger.OnRoomTransition.Invoke();
            }
        }
    }
}

*/