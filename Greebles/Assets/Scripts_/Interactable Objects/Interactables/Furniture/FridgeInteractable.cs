using Unity.VisualScripting;
using UnityEngine;

public class FridgeInteractable : InteractableObject, IHumanInteractable
{
    [SerializeField] private InteractableType _interactableType;
    public override InteractableType interactionType => _interactableType;

    public bool ToBeCorrected { get; set; }
    public bool IsMisplaceable { get; set; } = false;
    public Vector3 InitialPosition { get; set; }
    public Quaternion InitialRotation { get; set; }

    [SerializeField] private GameEvent OnFridgeClosed;
    public bool isSlightlyOpened;

    public Animator animator;

    public override void Catinteraction()
    {
        if(isSlightlyOpened)
            animator.SetTrigger("Open");
    }

    public void OpenSlightly(Component sender)
    {
        animator.SetTrigger("OpenSlightly");
    }

    public void CorrectInteractable()
    {
        animator.SetTrigger("Close");
        OnFridgeClosed?.Raise_WithoutParam(this);
    }
}
