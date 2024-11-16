using Unity.Multiplayer.Center.Common.Analytics;
using Unity.VisualScripting;
using UnityEngine;

public class Greebles : InteractableObject
{
    [SerializeField] protected InteractableType _interactionType = InteractableType.Hit;

    public override InteractableType interactionType => _interactionType;

    public override void Catinteraction()
    {
        base.Catinteraction();

        // TO DO: Implement interaction effect

        // Play Animation
        // Destroy Object
        
        Debug.Log("Die, filthy Greeble");
    }


}
