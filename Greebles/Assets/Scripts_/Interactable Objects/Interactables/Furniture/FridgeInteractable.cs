using Unity.VisualScripting;
using UnityEngine;

public class FridgeInteractable : InteractableObject
{
    [SerializeField] private InteractableType _interactableType;
    public override InteractableType interactionType => _interactableType;

    public Animator animator;

    public override void Catinteraction()
    {
        animator.SetTrigger("Open");
    }

    public void Close()
    {
        
    }
}
