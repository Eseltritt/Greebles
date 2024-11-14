using UnityEngine;

public enum InteractableType
{
    Walk,
    Scratch,
    Hit
}

public interface IInteractable
{
    void Interact();
}

/* public interface IDamageable
{
    void damage();
} */

public abstract class InteractableObject : MonoBehaviour, IInteractable
{
    public abstract InteractableType interactionType {get;}

    public virtual void Interact()
    {
        // TO DO: Implement general interaction effect
    }
}