using UnityEngine;

public class RiddleDummy : InteractableObject
{
    //This Dummy exists out of testing reasons for the riddle system.

    [SerializeField] private InteractableType _interactionType;
    public override InteractableType interactionType => _interactionType;

    public override void Interact()
    {
        base.Interact();
        
        Debug.Log("Riddle");
    }
}
