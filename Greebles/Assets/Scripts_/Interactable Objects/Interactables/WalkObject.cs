using Unity.VisualScripting;
using UnityEngine;

public class WalkObject : InteractableObject
{
    [SerializeField] private InteractableType _interactionType = InteractableType.Walk;

    public override InteractableType interactionType => _interactionType;

    public override void Interact()
    {
        base.Interact();

        // TO DO: Implement interaction effect

        Debug.Log("This is a nice spot to sit");
    }
}
