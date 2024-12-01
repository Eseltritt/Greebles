using UnityEngine;

public class PickUp : InteractableObject
{
    [SerializeField] private InteractableType _interactionType;
    private GameObject _player;
    private PlayerBehaviour _playerController;
    public GameEvent OnObjectPickUp;

    public override InteractableType interactionType => _interactionType;

    public override void Start()
    {
        base.Start();
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerController = _player.GetComponent<PlayerBehaviour>();
    }

    public override void Catinteraction()
    {
        if (_playerController._heldObject != null)
            return;

        transform.parent = _player.GetComponent<PlayerBehaviour>().holdObjectTransform.transform;
        transform.position = _player.GetComponent<PlayerBehaviour>().holdObjectTransform.transform.position;
        OnObjectPickUp?.Raise_SingleParam(this, true);
        //ToDo: Add a get Method to the PlayerBehaviour to get the PickUpTransform.
        //transform.position = _player.GetPickUpTransform().position;
        //ToDo: Add a CheckInPayload Method to the PlayerBehaviour to check in the GameObject to the player as a parameter.
        //_player.CheckInPayload(this);
    }
}
