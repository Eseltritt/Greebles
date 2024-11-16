using UnityEngine;

public enum InteractableType
{
    Walk,
    Scratch,
    Hit,
}

public abstract class InteractableObject : MonoBehaviour/* , IInteractable */
{
    public abstract InteractableType interactionType {get;}

    public virtual void Catinteraction()
    {
        // TO DO: Implement general interaction effect
    }
}