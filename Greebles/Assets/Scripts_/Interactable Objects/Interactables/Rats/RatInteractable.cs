using Unity.VisualScripting;
using UnityEngine;

public class RatInteractable : InteractableObject
{
    [SerializeField] private InteractableType _interactionType = InteractableType.Hit;
    public override InteractableType interactionType => _interactionType;

    private RatAI ratAI;

    public override void Start()
    {
        base.Start();
        ratAI = gameObject.GetComponent<RatAI>();
    }

    public override void Catinteraction()
    {
        ratAI.Escape();
    }
}
