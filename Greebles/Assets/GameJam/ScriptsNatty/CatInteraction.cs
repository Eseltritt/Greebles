using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace NattyStuff
{
    /// <summary>
    /// Attach this script to the cat, create Tag WebBarrier in inspector (check all webbarriers are tagged with it)
    /// </summary>
    public class CatInteraction : MonoBehaviour
    {

        [TextArea(3, 5)]
        public string testNote = "This class is a temporary placeholder for testing purposes. The actual interaction calss will be implemented by Ryan in a separate class.";

        public float interactionDistance = 2f;    //CatInteraction
        private Animator catAnimator;
        private bool isInteracting = false;

        // List to store all active WebBarriers
        [SerializeField]
        private List<ScratchInteraction> activeScratchables = new List<ScratchInteraction>();
        [SerializeField]
        private List<WebBarrier> activeWebBarriers = new List<WebBarrier>();

        public static CatInteraction Instance { get; private set; }



        private void Awake()
        {
            //there can be only one cat in the scene!
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                Debug.Log("CatInteraction instance created.");
            }
            else
            {
                Debug.LogWarning("Another instance of CatInteraction exists! Destroying this one.");
                Destroy(gameObject);
            }
        }



        //Method to remove a WebBarrier from the list(called by WebBarrier on Disable)
        //Method to add a WebBarrier to the list
        public void RegisterWebBarrier(WebBarrier webBarrier)
        {
            if (!activeWebBarriers.Contains(webBarrier))
            {
                activeWebBarriers.Add(webBarrier);
            }
        }

        //Method to remove a WebBarrier from the list
        public void UnregisterWebBarrier(WebBarrier webBarrier)
        {
            activeWebBarriers.Remove(webBarrier);    
        }


        //SCRATCHABLE OBJECTS
        public void RegisterScratchable(ScratchInteraction scratchableObject)
        {
            if (!activeScratchables.Contains(scratchableObject))
            {
                activeScratchables.Add(scratchableObject);
            }
        } 

        public void UnregisterScratchable(ScratchInteraction scratchableObject)
        {
            activeScratchables.Remove(scratchableObject);
        }



        private void Start()
        {
            catAnimator = GetComponent<Animator>();
            if (catAnimator == null)
            {
                Debug.LogError("Animator component not found on the cat!");
            }

            FindAllWebBarriers();
            FindAllScratchableInteractables();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return) && !isInteracting)
            {
                WebBarrier nearestWeb = FindNearestWeb();
                if (nearestWeb != null)
                {
                    StartCoroutine(InteractWithWeb(nearestWeb));
                }
            }

            //we can use space to find scratchables
            if (Input.GetKeyDown(KeyCode.Space) && !isInteracting)
            {
                ScratchInteraction nearestScratchy = FindNearestScratchable();
                if (nearestScratchy != null)
                {
                    StartCoroutine(InteractWithScratchable(nearestScratchy));
                }
            }
        }


        private void FindAllScratchableInteractables()
        {
            activeScratchables.Clear();
            GameObject[] scratchableObjects = GameObject.FindGameObjectsWithTag("Scratchables");
            foreach (GameObject obj in scratchableObjects)
            {
                ScratchInteraction scratchable = obj.GetComponent<ScratchInteraction>();
                if (scratchable != null)
                {
                    RegisterScratchable(scratchable);
                }
            }
            Debug.Log($"Found {activeScratchables.Count} WebBarriers in the scene.");
        }


        private void FindAllWebBarriers()
        {
            activeWebBarriers.Clear();
            GameObject[] webBarrierObjects = GameObject.FindGameObjectsWithTag("WebBarrier");
            foreach (GameObject obj in webBarrierObjects)
            {
                WebBarrier webBarrier = obj.GetComponent<WebBarrier>();
                if (webBarrier != null)
                {
                    RegisterWebBarrier(webBarrier);
                }
            }
            Debug.Log($"Found {activeWebBarriers.Count} WebBarriers in the scene.");
        }



        private ScratchInteraction FindNearestScratchable()
        {
            ScratchInteraction nearestScratchableInteractable = null;
            float nearestDistance = float.MaxValue;

            // Check distance to each active WebBarrier
            foreach (var scratchable in activeScratchables)
            {
                if (scratchable.IsPointInRange(transform.position, interactionDistance))
                {
                    float distance = Vector3.Distance(transform.position, scratchable.transform.position);
                    if (distance < nearestDistance)
                    {
                        nearestScratchableInteractable = scratchable;
                        nearestDistance = distance;
                    }
                }
            }

            return nearestScratchableInteractable;
        }

        private WebBarrier FindNearestWeb()
        {
            WebBarrier nearest = null;
            float nearestDistance = float.MaxValue;

            // Check distance to each active WebBarrier
            foreach (var web in activeWebBarriers)
            {
                if (web.IsPointInRange(transform.position, interactionDistance))
                {
                    float distance = Vector3.Distance(transform.position, web.transform.position);
                    if (distance < nearestDistance)
                    {
                        nearest = web;
                        nearestDistance = distance;
                    }
                }
            }

            return nearest;
        }

        private IEnumerator InteractWithScratchable(ScratchInteraction scratchy)
        {
            isInteracting = true;

            // Play scratch animation and pause for its duration
            catAnimator.Play("Scratch");
            yield return new WaitForSeconds(catAnimator.GetCurrentAnimatorStateInfo(0).length);

            // Scratch the object
            scratchy.Scratch();

            if (scratchy.IsInteractionComplete())
            {
                // Determine completion animation based on revealed object type
                //animations can be chained by adding the animation completed script...
                string completionAnimation;

                switch (scratchy.RevealedObjectType)
                {
                    case RevealedObjectReactionType.Boop:
                        completionAnimation = "Boop";
                        break;
                    case RevealedObjectReactionType.Jump:
                        completionAnimation = "root|Cat_Jump_NoZ";
                        break;
                    case RevealedObjectReactionType.Idle:
                        completionAnimation = "root|Idle";
                        break;
                    case RevealedObjectReactionType.Hiss:
                        completionAnimation = "root|Hiss";
                        break;
                    default:
                        completionAnimation = "Boop";
                        break;
                }

                // Play completion animation and pause for its duration
                catAnimator.Play(completionAnimation);
                yield return new WaitForSeconds(catAnimator.GetCurrentAnimatorStateInfo(0).length);
            }

            isInteracting = false;
        }

        //private IEnumerator InteractWithScratchable(ScratchInteraction scratchy)
        //{
        //    isInteracting = true;
        //    //Play "Boop" animation as the default completion animation
        //    catAnimator.Play("Scratch");
        //    yield return new WaitForSeconds(1f); // Duration of scratch animation

        //    scratchy.Scratch();

        //    if (scratchy.IsInteractionComplete())
        //    {
        //        catAnimator.Play("Boop"); //default animation that reacts to completion stated (
        //        yield return new WaitForSeconds(1f); // Duration of boop animation Open door, reveal hidden item
        //    }

        //    isInteracting = false;
        //}

        private IEnumerator InteractWithWeb(WebBarrier web)
        {
            isInteracting = true;

            catAnimator.Play("Scratch");
            Debug.LogError($"Animation '{catAnimator.name}.Scratch' not found!");
            yield return new WaitForSeconds(1f); // Duration of scratch animation

            web.TakeHit();

            if (web.IsDestroyed())
            {
                catAnimator.Play("Boop");
                Debug.LogError($"Animation '{catAnimator.name}.Boop' not found!");
                yield return new WaitForSeconds(1f); // Duration of boop animation
            }

            isInteracting = false;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, interactionDistance);

            foreach (WebBarrier web in activeWebBarriers)
            {
                if (web == null) continue;

                bool inRange = web.IsPointInRange(transform.position, interactionDistance);
                Gizmos.color = inRange ? Color.green : Color.red;

                BoxCollider activeCollider = web.GetActiveCollider();
                if (activeCollider != null)
                {
                    Gizmos.DrawWireCube(activeCollider.bounds.center, activeCollider.bounds.size);
                }

                if (inRange)
                {
                    Gizmos.color = Color.yellow;
                    Gizmos.DrawLine(transform.position, web.GetClosestPoint(transform.position));

                    // Draw the name of the WebBarrier using Handles
                    Vector3 closestPoint = web.GetClosestPoint(transform.position);
                    Gizmos.DrawLine(transform.position, closestPoint);
                    Handles.Label(closestPoint + Vector3.up * 0.5f, web.gameObject.name + " (Scratachble Webs Interaction)"); // Offset label slightly above the closest point
                }
            }


            //find all our scratchable and show the range
            foreach (ScratchInteraction scratch in activeScratchables)
            {
                if (scratch == null) continue;

                bool inRange = scratch.IsPointInRange(transform.position, interactionDistance);
                Gizmos.color = inRange ? Color.green : Color.red;

                BoxCollider activeCollider = scratch.GetActiveCollider();
                if (activeCollider != null)
                {
                    Gizmos.DrawWireCube(activeCollider.bounds.center, activeCollider.bounds.size);
                }

                if (inRange)
                {
                    Gizmos.color = Color.yellow;
                    Vector3 closestPoint = scratch.GetClosestPoint(transform.position);
                    Gizmos.DrawLine(transform.position, closestPoint);

                    // Draw the name of the ScratchInteraction using Handles
                    Handles.Label(closestPoint + Vector3.up * 0.5f, scratch.gameObject.name + " (Scratch Interaction)");
                }
            }
        }
    }
}