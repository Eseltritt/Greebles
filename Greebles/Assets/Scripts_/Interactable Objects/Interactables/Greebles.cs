using Unity.Multiplayer.Center.Common.Analytics;
using Unity.VisualScripting;
using UnityEngine;

public class Greebles : InteractableObject
{
    [SerializeField] protected InteractableType _interactionType = InteractableType.Hit;

    public override InteractableType interactionType => _interactionType;

    public float testFloat = 5.3f;

    public override void Interact()
    {
        base.Interact();

        // TO DO: Implement interaction effect
        
        Debug.Log("Die, filthy Greeble");
    }
}
