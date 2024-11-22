using Unity.Multiplayer.Center.Common.Analytics;
using Unity.VisualScripting;
using UnityEngine;

public class GreeblesInteractable : InteractableObject
{
    [SerializeField] protected InteractableType _interactionType = InteractableType.Hit;

    public override InteractableType interactionType => _interactionType;

    public override void Catinteraction()
    {
        // TO DO: Implement interaction effect

        // Play Animation
        // Destroy Object
        
        Die();
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
