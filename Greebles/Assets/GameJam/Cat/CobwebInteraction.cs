using System.Collections;
using UnityEngine;

public class CobwebInteraction : MonoBehaviour
{
    public GameObject pressurePlate; // Reference to the pressure plate object
    public GameObject doorCobweb; // Reference to the cobweb object
    public GameObject door; // Reference to the door object
    public float interactionDistance = 2f; // Distance at which interaction is possible

    private bool isCobwebRemoved = false; // State to check if cobweb has been removed
    private bool isInteracting = false; // State to check if an interaction is ongoing
    private Animator catAnimator; // Animator for the cat
    private DoorInteraction doorInteractionScript; // Reference to DoorInteraction script

    private void Start()
    {
        catAnimator = GetComponent<Animator>();
        if (catAnimator == null)
        {
            Debug.LogError("Animator component not found on the cat!");
        }

        if (door != null)
        {
            doorInteractionScript = door.GetComponent<DoorInteraction>();
            if (doorInteractionScript == null)
            {
                Debug.LogError("DoorInteraction component not found on the door!");
            }
        }

        Debug.Log("CobwebInteraction initialized.");
    }

    private void Update()
    {
       

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space key pressed. Checking interactions...");

            // Check proximity to cobweb
            bool nearCobweb = IsNearCobweb();

            // Log proximity status
            Debug.Log($"Near Cobweb: {nearCobweb}");

            if (!isCobwebRemoved) // If cobweb hasn't been removed yet
            {
                if (nearCobweb)
                {
                    CheckCobwebInteraction();
                }
                else
                {
                    Debug.Log("Cat is too far from the cobweb to interact.");
                }
            }
            else // If cobweb has been removed, allow interaction with door
            {
                if (nearCobweb) // If near cobweb, allow door interaction
                {
                    CheckDoorInteraction();
                }
                else
                {
                    Debug.Log("Cat is too far from both objects to interact.");
                }
            }
        }
    }



    private bool IsNearCobweb()
    {
        float distanceToCobweb = Vector3.Distance(transform.position, doorCobweb.transform.position);
        Debug.Log($"Distance to cobweb: {distanceToCobweb}"); // Log distance for debugging
        return distanceToCobweb <= interactionDistance;
    }

   

    private void CheckCobwebInteraction()
    {
        Debug.Log("Cat is near the cobweb. Initiating cobweb interaction.");

        if (!isInteracting)
        {
            isInteracting = true;
            StartCoroutine(ScratchCobweb());
        }
        else
        {
            Debug.Log("Already interacting with cobweb.");
        }
    }

    private void CheckDoorInteraction()
    {
        Debug.Log("Cat is near the door. Initiating door interaction.");

        if (!isInteracting)
        {
            isInteracting = true;
            StartCoroutine(OpenDoor());
        }
        else
        {
            Debug.Log("Already interacting with door.");
        }
    }

    private IEnumerator ScratchCobweb()
    {
        Debug.Log("Cat is scratching the cobweb.");

        if (catAnimator != null)
        {
            catAnimator.Play("Scratch");
            yield return new WaitForSeconds(1f); // Wait for animation duration
        }

        RemoveCobweb(); // Remove cobweb after scratching
        isInteracting = false; // Reset interaction state
    }

    private IEnumerator OpenDoor()
    {
        // Play the cat's open door animation
        if (catAnimator != null)
        {
            catAnimator.Play("Boop"); // Ensure this matches your Animator state name
            Debug.Log("Playing cat's open door animation.");
        }

        if (doorInteractionScript != null)
        {
            doorInteractionScript.OpenDoor(); // Call method from DoorInteraction script
            yield return new WaitForSeconds(1f); // Wait for animation duration
            isInteracting = false; // Reset interaction state after opening
        }
    }

    private void RemoveCobweb()
    {
        Debug.Log("Removing cobweb and activating pressure plate.");

        // Deactivate the cobweb and activate pressure plate
        doorCobweb.SetActive(false);

        Collider plateCollider = pressurePlate.GetComponent<Collider>();

        if (plateCollider != null)
        {
            plateCollider.isTrigger = true; // Set pressure plate collider as trigger
            Debug.Log("Pressure plate collider set to trigger.");

            pressurePlate.SetActive(true); // Activate pressure plate
            Debug.Log("Pressure plate activated.");

            isCobwebRemoved = true; // Update state indicating cobweb has been removed
            Debug.Log($"Cobweb removed. isCobwebRemoved: {isCobwebRemoved}");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionDistance); // Draw sphere around cat's position

        if (door != null && door.GetComponent<BoxCollider>() != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(door.GetComponent<BoxCollider>().bounds.center, door.GetComponent<BoxCollider>().bounds.size); // Draw wire cube around door collider
        }
    }
}