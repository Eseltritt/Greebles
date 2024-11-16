using UnityEngine;

public class HitObject : InteractableObject
{
    #region Private Variables

    [SerializeField] private InteractableType _interactionType = InteractableType.Hit;

    public override InteractableType interactionType => _interactionType;

    private Rigidbody _rigidBody;

    private Vector3 _initialPosition;
    private Quaternion _intialRotation;

    private bool _isMisplaced = false;

    #endregion

    #region Public Variables

    public Vector3 InitialPosition { get {return _initialPosition;} }

    public Quaternion InitialRotation { get {return _intialRotation;} }

    public bool IsMisplaced { get {return _isMisplaced;} } // To use for Character to determine if an object must be replaced

    #endregion

    private Vector3 forceDirection = new Vector3(10,5,0); // TO DO: Replace with vector dependant on cat paw strike direction

    #region Init

    private void Start(){
        _initialPosition = gameObject.transform.position;
        _intialRotation = transform.rotation;

        if (GetComponent<Rigidbody>() == null)
        {
            // Add a Rigidbody component if it doesn't exist
            gameObject.AddComponent<Rigidbody>();
        }

        _rigidBody = gameObject.GetComponent<Rigidbody>();

        gameObject.tag = "HitObject";
    }

    #endregion

    #region Cat Interaction

    public override void Catinteraction()
    {
        base.Catinteraction();

        AddForce();
    }

    private void AddForce(){
        _rigidBody.AddForce(forceDirection, ForceMode.Impulse);
    }

    #endregion

    #region Human Interaction

    private void ResetPosition(){
        transform.position = _initialPosition;
        transform.rotation = _intialRotation;
    }
    #endregion
}
