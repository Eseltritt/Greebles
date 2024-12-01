using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;

public enum RitualObject
{
    Bone,
    Candle,
    ToiletPaper,
    Challice,
    Herbs,
    FinalRitualObject
}
//This class is used to place the ritual components on the ritual place.
public class RitualPlacement : InteractableObject
{
    [SerializeField] private InteractableType _interactionType;
    private GameObject _player;
    private PlayerBehaviour _playerBehaviour;
    public RitualObject ritualObjectType;
    public bool objectPlaced { get; private set; }
    public GameEvent OnHeldObjectChanged;
    private GameObject placedOject;
    //The list of the 5 ritual components.
    /* [SerializeField] private List<Transform> ritualComponents; */

    public override InteractableType interactionType => _interactionType;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {
        base.Start();
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerBehaviour = _player.GetComponent<PlayerBehaviour>();
    }

    public override void Catinteraction()
    {
        PlaceObject(CheckPayload(_playerBehaviour));
    }

    //This method places the object on the ritual place, if the player has a payload and if the payload is a ritual component.
    //The method checks the name of the ritual component and places it on the corresponding position.
    private void PlaceObject(RitualComponent ritualComponent)
    {
        if (ritualComponent == null)
        {
            Debug.Log("No ritual component");
            return;
        }

        if (ritualComponent.ritualObjectType == ritualObjectType)
        {
            ritualComponent.transform.parent = transform;
            ritualComponent.transform.localPosition = Vector3.zero;
            placedOject = ritualComponent.gameObject;
            objectPlaced = true;
            OnHeldObjectChanged?.Raise_SingleParam(this, false);
            /* ritualComponent.GetComponent<Ritu>().enabled = false; */
        }

        /* switch (ritualComponent.ritualObjectType)
        {
            case RitualObject.Bone:
                ritualComponent.transform.position = ritualComponents[0].position;
                ritualComponent.transform.rotation = ritualComponents[0].rotation;
                ritualComponent.transform.parent = transform;
                ritualComponent.GetComponent<PickUp>().enabled = false;
                break;
            case RitualObject.Candle:
                ritualComponent.transform.position = ritualComponents[1].position;
                ritualComponent.transform.rotation = ritualComponents[1].rotation;
                ritualComponent.transform.parent = transform;
                ritualComponent.GetComponent<PickUp>().enabled = false;
                break;
            case RitualObject.Challice:
                ritualComponent.transform.position = ritualComponents[2].position;
                ritualComponent.transform.rotation = ritualComponents[2].rotation;
                ritualComponent.transform.parent = transform;
                ritualComponent.GetComponent<PickUp>().enabled = false;
                break;
            case RitualObject.Herbs:
                ritualComponent.transform.position = ritualComponents[3].position;
                ritualComponent.transform.rotation = ritualComponents[3].rotation;
                ritualComponent.transform.parent = transform;
                ritualComponent.GetComponent<PickUp>().enabled = false;
                break;
            case RitualObject.ToiletPaper:
                ritualComponent.transform.position = ritualComponents[4].position;
                ritualComponent.transform.rotation = ritualComponents[4].rotation;
                ritualComponent.transform.parent = transform;
                ritualComponent.GetComponent<PickUp>().enabled = false;
                break;
            default:
                Debug.Log("Not a ritual component");
                ritualComponent.gameObject.SetActive(false);
                break;
        } */
    }

    void OnTriggerExit (Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Interactable") && other.gameObject == placedOject)
        {
            objectPlaced = false;
        }
    }

    private RitualComponent CheckPayload(PlayerBehaviour player)
    {
        GameObject payload = player.CheckoutPayload();
        if (payload == null)
        {
            Debug.Log("No payload");
            return null;
        }
        
        //return payload;
        return payload.GetComponent<RitualComponent>();
    }
}
