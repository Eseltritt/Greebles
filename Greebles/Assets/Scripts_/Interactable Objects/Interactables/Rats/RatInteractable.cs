using Unity.VisualScripting;
using UnityEngine;

public class RatInteractable : InteractableObject
{
    public override InteractableType interactionType => throw new System.NotImplementedException();
    private RatAI ratAI;

    private void Start()
    {
        ratAI = gameObject.GetComponent<RatAI>();
    }

    public override void Catinteraction()
    {
        Escape();
    }

    void Escape()
    {
        
    }
}
