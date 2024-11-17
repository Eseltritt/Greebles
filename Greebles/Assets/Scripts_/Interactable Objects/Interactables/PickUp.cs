using UnityEngine;

public class PickUp : InteractableObject
{
    [SerializeField] private InteractableType _interactionType;
    private GameObject _player;

    public override InteractableType interactionType => _interactionType;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Catinteraction()
    {
        base.Catinteraction();
        Debug.Log("Picked up");
        transform.parent = _player.transform;
        transform.position = _player.transform.position;
        //ToDo: Add a get Method to the PlayerBehaviour to get the PickUpTransform.
        //transform.position = _player.GetPickUpTransform().position;
        //ToDo: Add a CheckInPayload Method to the PlayerBehaviour to check in the GameObject to the player as a parameter.
        //_player.CheckInPayload(this);
    }
}
