using UnityEngine;

public class RiddleDummy : InteractableObject
{
    //This Dummy exists out of testing reasons for the riddle system.

    [SerializeField] private InteractableType _interactionType;
    [SerializeField] public GameEvent riddleOneCount;
    public override InteractableType interactionType => _interactionType;

    // This method is called when the player interacts with the object. It calls the RiddleOneCount event and deactivates the object.
    public override void Catinteraction()
    {
        riddleOneCount?.Raise_WithoutParam(this);
        Debug.Log("Riddle");
        gameObject.SetActive(false);
    }
}
