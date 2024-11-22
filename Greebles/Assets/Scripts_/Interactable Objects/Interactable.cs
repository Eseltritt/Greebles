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

    public virtual void Start(){
        gameObject.layer = LayerMask.NameToLayer("Interactable");
    }

    public abstract void Catinteraction();
}