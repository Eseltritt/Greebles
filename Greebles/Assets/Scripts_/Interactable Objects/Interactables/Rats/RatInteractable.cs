using UnityEngine;

public class RatInteractable : InteractableObject
{
    public override InteractableType interactionType => throw new System.NotImplementedException();

    public override void Catinteraction()
    {
        Escape();
    }

    void Escape()
    {
        
    }
}
