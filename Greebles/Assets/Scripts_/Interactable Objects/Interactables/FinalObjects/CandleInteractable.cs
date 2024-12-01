using UnityEditor.Rendering;
using UnityEngine;

public class CandleInteractable : InteractableObject, IHumanInteractable
{
    [SerializeField] private InteractableType _interactionType = InteractableType.Hit;
    public override InteractableType interactionType => _interactionType;

    private Rigidbody _rigidBody;

    #region Events
        [SerializeField] private GameEvent OnHitObjectMisplaced;
    #endregion

    public bool ToBeCorrected { get; set; } = false;
    public bool IsMisplaceable { get; set; } = true;
    public Vector3 InitialPosition { get; set; }
    public Quaternion InitialRotation { get; set; }
    private GameObject player;

    public float force = 5; // TO DO: Replace with vector dependant on cat paw strike direction

    #region Init

    public override void Start(){
        base.Start();

        InitialPosition = gameObject.transform.position;
        InitialRotation = transform.rotation;

        if (GetComponent<Rigidbody>() == null)
        {
            gameObject.AddComponent<Rigidbody>();
        }

        _rigidBody = gameObject.GetComponent<Rigidbody>();

        gameObject.tag = "HitObject";

        ToBeCorrected = false;

        player = GameObject.FindGameObjectWithTag("Player");
    }

    #endregion

    #region Cat Interaction

    public override void Catinteraction()
    {
        transform.LookAt(player.transform);
        AddForce();
        
        ToBeCorrected = true;
    }

    private void AddForce(){
        _rigidBody.AddForce(transform.right * force, ForceMode.VelocityChange);
        OnHitObjectMisplaced?.Raise_WithoutParam(this);
    }

    #endregion

    #region Human Interaction

    public void CorrectInteractable()
    {
        transform.position = InitialPosition;
        transform.rotation = InitialRotation;

        ToBeCorrected = false;
    }
    #endregion
}