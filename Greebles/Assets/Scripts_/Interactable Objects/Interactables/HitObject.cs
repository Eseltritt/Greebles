using Unity.VisualScripting;
using UnityEngine;

public class HitObject : InteractableObject
{
    [SerializeField] private InteractableType _interactionType = InteractableType.Hit;

    public override InteractableType interactionType => _interactionType;


    public override void Interact()
    {
        base.Interact();

        // TO DO: Implement interaction effect

        Debug.Log("What happens if I slap this thing?");
    }
}
