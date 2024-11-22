using System.Collections;
using UnityEngine;
using UnityEngine.Events;



/*
ScratchInteraction Setup Guide

1. **Scene Setup**:
   - Ensure a CatInteraction script is present on the cat GameObject.
   - Identify scratchable objects (e.g., furniture, doors).

2. **Applying ScratchInteraction**:
   - Add this script to each scratchable object.
   - The script automatically tags the object as "Scratchables".

3. **Inspector Configuration**:
   - Assign `scratchableObject` (initial state) and `scratchObjectInteracted` (scratched state).
   - Set `useVisuals` to toggle visibility.
   - Adjust `scratchesToComplete` for the number of scratches required.

4. **Event Setup**:
   - Configure `onScratchedOnce` and `onInteractionComplete` events in the Inspector to trigger animations or sounds.

5. **Collider**:
   - Ensure each scratchable object has a BoxCollider for interaction detection.

6. **Testing**:
   - Play the scene and use the cat to interact with scratchable objects.
   - Check the console for event messages during testing.

Note: This script works with CatInteraction to manage interactions with objects tagged "Scratchables".
*/


//tag scratchble interaction items "Scratchables" (and add ScratchInteraction script"
public class ScratchInteraction : MonoBehaviour
{
  
    public GameObject scratchableObject;  // Reference to the intact object (optional)
    public GameObject scratchObjectInteracted; // Reference to the damaged object (optional)
    public bool useVisuals = true;   // - works for 2 objects that form a sequence such as intact door and a door with scratches, it will hide the game object previous and show the next (increments with currentScratches)
    public int scratchesToComplete = 2;
    public UnityEvent onScratchedOnce;
    public UnityEvent onInteractionComplete;
    public UnityEvent pickupItem;

    private int currentScratches = 0;
    private BoxCollider intactCollider;
    private BoxCollider damagedCollider;

    //testing
    [SerializeField] private string onScratchedOnceMessage = "Object scratched once!";
    [SerializeField] private string onInteractionCompleteMessage = "Interaction completed!";


    private void Awake()
    {
        gameObject.tag = "Scratchables";
    }

    private void OnEnable()
    {
        StartCoroutine(RegisterScratchableCoroutine());
        ResetObject();
    }

    private IEnumerator RegisterScratchableCoroutine()
    {
        yield return new WaitForSeconds(0.1f);

        if (CatInteraction.Instance != null)
        {
            CatInteraction.Instance.RegisterScratchable(this);
            Debug.Log($"Registered ScratchInteraction for {gameObject.name}.");
        }
        else
        {
            Debug.LogError($"CatInteraction instance not found for {gameObject.name}!");
        }
    }

    //private void OnEnable()
    //{
    //    if (CatInteraction.Instance != null)
    //    {
    //        CatInteraction.Instance.RegisterScratchable(this);
    //        Debug.Log($"Registered ScratchInteraction for {gameObject.name}.");
    //    }
    //    else
    //    {
    //        Debug.LogError($"CatInteraction instance not found for {gameObject.name}!");
    //    }
    //    ResetObject();
    //}

    private void OnDisable()
    {
        if (CatInteraction.Instance != null)
        {
            CatInteraction.Instance.UnregisterScratchable(this);
        }
    }



    private void Start()
    {
            //this can be the box,too drapes etc (or the images that you swap pout)

            intactCollider = scratchableObject?.GetComponent<BoxCollider>();
            damagedCollider = scratchObjectInteracted?.GetComponent<BoxCollider>();  

            if (intactCollider == null && scratchableObject != null)
            {
                Debug.LogWarning("BoxCollider not found on scratchImageNoDamage!");
            }
            if (damagedCollider == null && scratchObjectInteracted != null)
            {
                Debug.LogWarning("BoxCollider not found on scratchImageDamaged!");
            }

        ResetObject();
    }


    public void Scratch()
    {
        currentScratches++;

        if (currentScratches >= scratchesToComplete)
        {
            CompleteInteraction();
            DisplayEventMessage(onInteractionCompleteMessage);


            if (currentScratches == 3)
            {
                if (pickupItem != null)
                {
                    pickupItem.Invoke();
                    Debug.Log("Object attached to bone and event invoked.");
                }
                else
                {
                    Debug.LogWarning("pickupItem event is null. Please assign an event in the Inspector.");
                }
            }
        }
        else
        {
            if (onScratchedOnce != null)
            {
                onScratchedOnce.Invoke();
                Debug.Log("onScratchedOnce event invoked.");
            }
            else
            {
                Debug.LogWarning("onScratchedOnce event is null. Please assign an event in the Inspector.");
            }
        }

        UpdateObjectState();
    }



    //testing
    public void DisplayEventMessage(string message)
    {
        Debug.Log($"[{gameObject.name}] Event Message: {message}");
    }


    public void SetEventMessage(bool isOnScratchedOnce, string message)
    {
        if (isOnScratchedOnce)
        {
            onScratchedOnceMessage = message;
        }
        else
        {
            onInteractionCompleteMessage = message;
        }
    }

    private void CompleteInteraction()
    {
        Debug.Log($"Interaction completed for {gameObject.name}!");
        onInteractionComplete.Invoke(); // Trigger completion event
        DisplayEventMessage(onInteractionCompleteMessage); // Log message
    }

    private void UpdateObjectState()
    {
        Debug.Log($"UpdateObjectState for {gameObject.name}: Scratches = {currentScratches}");

        if (useVisuals)
        {
            UpdateVisuals(); // Call to update visuals based on current scratches, if applicable
        }
    }

    public void ResetObject()
    {
        currentScratches = 0;
        UpdateObjectState();
    }


    public bool IsInteractionComplete()
    {
        return currentScratches >= scratchesToComplete;
    }

    public BoxCollider GetActiveCollider()
    {
        return currentScratches == 0 ? intactCollider : damagedCollider;
    }

    public Vector3 GetClosestPoint(Vector3 position)
    {
        BoxCollider activeCollider = GetActiveCollider();
        if (activeCollider != null)
        {
            return activeCollider.ClosestPoint(position);
        }
        return transform.position;
    }

    public bool IsPointInRange(Vector3 position, float range)
    {
        Vector3 closestPoint = GetClosestPoint(position);
        return Vector3.Distance(position, closestPoint) <= range;
    }

    private void UpdateVisuals()
    {
        if (useVisuals)
        {
            Debug.Log("Updating visuals...");
            Debug.Log("Current scratch level: " + currentScratches);

            // Check if objects are the same before switch statement somebody might have forgotten to turn the bool off!
            if (scratchableObject != scratchObjectInteracted)
            {
                switch (currentScratches)
                {
                    case 0:
                        Debug.Log("Showing initial state");
                        scratchableObject.SetActive(true);
                        scratchObjectInteracted.SetActive(false);
                        break;
                    case 1:
                        Debug.Log("Showing first damaged state");
                        scratchableObject.SetActive(false);
                        scratchObjectInteracted.SetActive(true);
                        break;
                        // Add more cases for additional damage states if needed
                }
            }
            else
            {
                Debug.LogError("Error: scratchableObject and scratchObjectInteracted are the same object and not a sequence - skipping use sequential visuals to show interaction or damage.");
            }


            // Add more cases for additional damage states if needed
            // For example, for a third damage state:
            // case 2:
            //     Debug.Log("Showing second damaged state");
            //     heavilyDamagedObject.SetActive(true);
            //     scratchableObject.SetActive(false);
            //     scratchObjectInteracted.SetActive(false);
            //     break;
        
        }
    }


}


/// <summary>
/// Initializes the ScratchInteraction component.
/// </summary>
/// <remarks>
/// This method sets up the colliders for the scratchable object and its interacted state.
/// It's designed to be flexible for various types of scratch interactions:
/// 
/// 1. Damageable Objects:
///    - scratchableObject: The initial, undamaged state (e.g., intact furniture)
///    - scratchObjectInteracted: The damaged state after scratching (e.g., scratched furniture)
/// 
/// 2. Openable Objects:
///    - scratchableObject: The closed state (e.g., closed door)
///    - scratchObjectInteracted: The partially open state (e.g., door ajar)
/// 
/// 3. Revealable Objects:
///    - scratchableObject: The concealed state (e.g., hidden compartment)
///    - scratchObjectInteracted: The revealed state (e.g., exposed compartment)
/// 
/// 4. State-Changing Objects:
///    - scratchableObject: Initial state (e.g., unactivated device)
///    - scratchObjectInteracted: Changed state (e.g., activated device)
/// 
/// Note: If visual changes are not needed, both objects can reference the same GameObject,
/// or scratchObjectInteracted can be left null. The script will still track scratch counts
/// and trigger events accordingly.

/// <summary>
/// Handles the scratching interaction on the object.
/// </summary>
/// <remarks>
/// This method provides two key Unity Events for developers to use:
/// 
/// 1. onScratchedOnce Event:
///    - Triggered on the first scratch
///    - Useful for initial reaction animations or sounds
///    - Example uses:
///      * Play a "first scratch" sound effect
///      * Trigger a slight object wobble animation
///      * Show a visual indicator that the object can be interacted with
/// 
/// 2. onInteractionComplete Event:
///    - Triggered when the required number of scratches is reached
///    - Ideal for final interaction completion
///    - Example uses:
///      * Open a door
///      * Reveal a hidden object
///      * Trigger a major state change in the scene
/// 
/// How to set up in Unity Inspector:
/// - Drag and drop methods from other scripts
/// - Add Animation triggers
/// - Call custom methods that change game state
/// 
/// Customization:
/// - Modify 'scratchesRequired' to change interaction complexity
/// - Add more case statements for more nuanced interactions
/// </remarks>
/// <summary>
/// Sets a custom message for either the initial scratch or the interaction completion event.
/// </summary>
/// <param name="isOnScratchedOnce">If true, sets the message for the first scratch. If false, sets the message for interaction completion.</param>
/// <param name="message">The custom message to be displayed when the event occurs.</param>
/// <remarks>
/// Primary Purpose: Prototyping
/// 
/// This method is designed to help developers quickly prototype and verify 
/// interaction mechanics before implementing final sound effects, animations, 
/// or visual changes.
/// 
/// Key Use Cases:
/// - Confirm that interaction events are firing correctly
/// - Validate the sequence and timing of scratch interactions
/// - Provide temporary feedback during early development stages
/// 
/// Example Workflow:
/// 1. Initial Development: Use custom messages to verify interaction flow
/// 2. Temporary Debugging: Track when and how interactions occur
/// 3. Placeholder for Future Implementation: 
///    Replace messages with actual sound/animation calls as development progresses
/// 
/// Usage Example:
/// // During prototyping
/// scratchInteraction.SetEventMessage(true, "First scratch - door should start to open");
/// scratchInteraction.SetEventMessage(false, "Interaction complete - door fully opened");
/// </remarks>
// Method to set custom messages at runtime
/// <summary>
/// Checks if the scratch interaction is complete.
/// </summary>
/// <returns>
/// True if the required number of scratches has been reached, false otherwise.
/// </returns>
/// <remarks>
/// This method is used to determine if the cat's interaction with the object
/// has reached its completion state. It can be used to check if:
/// - A door has been scratched enough times to open
/// - An object has been sufficiently interacted with to reveal something
/// - Any scratch-based interaction has reached its final state
/// 
/// Usage example in the cat class:
/// if (scratchableObject.IsInteractionComplete())
/// {
///     // Perform action based on completed interaction
///     // e.g., Open door, reveal hidden item, etc.
/// }
/// </remarks>