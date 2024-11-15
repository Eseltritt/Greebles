using Unity.VisualScripting;
using UnityEngine;

public class ScratchObject : InteractableObject
{
    [SerializeField] private InteractableType _interactionType = InteractableType.Scratch;

    public override InteractableType interactionType => _interactionType;


    public override void Interact()
    {
        base.Interact();

        // TO DO: Implement interaction effect

        Debug.Log("Scratchy scratchy...");
    }
}