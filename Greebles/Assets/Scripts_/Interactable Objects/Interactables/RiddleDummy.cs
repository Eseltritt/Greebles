using UnityEngine;

public class RiddleDummy : InteractableObject
{
    //This Dummy exists out of testing reasons for the riddle system.

    [SerializeField] private InteractableType _interactionType;
    public override InteractableType interactionType => _interactionType;

    public override void Catinteraction()
    {
        base.Catinteraction();
        gameObject.SetActive(false);
        
        Debug.Log("Riddle");
    }
}
