using UnityEngine;

/// <summary>
/// Attach the script to the secret door. which has the animator to move the door to the side. 
/// </summary>
public class DoorInteraction : MonoBehaviour
{
    private Animator doorAnimator; // Animator for the door
    public bool isOpen = false; // State to check if the door is open

    private void Start()
    {
        doorAnimator = GetComponentInParent<Animator>();
        if (doorAnimator == null)
        {
            Debug.LogError("Animator component not found on the door!");
        }
    }

    public void OpenDoor()
    {
        if (!isOpen)
        {
            Debug.Log("Opening door.");
            doorAnimator.Play("OpenSecretDoor"); // Play opening animation
            isOpen = true; // Update state
        }
        else
        {
            Debug.Log("Door is already open.");
        }
    }

    public void CloseDoor()
    {
        if (isOpen)
        {
            Debug.Log("Closing door.");
            doorAnimator.Play("CloseSecretDoor"); // Play closing animation (if you have one)
            isOpen = false; // Update state
        }
        else
        {
            Debug.Log("Door is already closed.");
        }
    }
}