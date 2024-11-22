using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;

//This class is used to place the ritual components on the ritual place.
public class RitualPlacement : InteractableObject
{
    [SerializeField] private InteractableType _interactionType;
    private GameObject _player;
    //The list of the 5 ritual components.
    [SerializeField] private List<Transform> ritualComponents;

    public override InteractableType interactionType => _interactionType;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    public override void Catinteraction()
    {
        base.Catinteraction();
        Debug.Log("Placed");
        PlaceObject(CheckPayload(_player));
    }

    //This method places the object on the ritual place, if the player has a payload and if the payload is a ritual component.
    //The method checks the name of the ritual component and places it on the corresponding position.
    private void PlaceObject(GameObject ritualComponent)
    {
        if (ritualComponent == null)
        {
            Debug.Log("No ritual component");
            return;
        }
        switch (ritualComponent.name)
        {
            case "SecretRiddleOne":
                ritualComponent.transform.position = ritualComponents[0].position;
                ritualComponent.transform.rotation = ritualComponents[0].rotation;
                ritualComponent.transform.parent = transform;
                ritualComponent.GetComponent<PickUp>().enabled = false;
                break;
            case "SecretRiddleTwo":
                ritualComponent.transform.position = ritualComponents[1].position;
                ritualComponent.transform.rotation = ritualComponents[1].rotation;
                ritualComponent.transform.parent = transform;
                ritualComponent.GetComponent<PickUp>().enabled = false;
                break;
            case "SecretRiddleThree":
                ritualComponent.transform.position = ritualComponents[2].position;
                ritualComponent.transform.rotation = ritualComponents[2].rotation;
                ritualComponent.transform.parent = transform;
                ritualComponent.GetComponent<PickUp>().enabled = false;
                break;
            case "SecretRiddleFour":
                ritualComponent.transform.position = ritualComponents[3].position;
                ritualComponent.transform.rotation = ritualComponents[3].rotation;
                ritualComponent.transform.parent = transform;
                ritualComponent.GetComponent<PickUp>().enabled = false;
                break;
            //case "SecretRiddleFive":
            //    ritualComponent.transform.position = ritualComponents[4].position;
            //    ritualComponent.transform.rotation = ritualComponents[4].rotation;
            //    ritualComponent.transform.parent = transform;
            //    ritualComponent.GetComponent<PickUp>().enabled = false;
            //    break;
            default:
                Debug.Log("Not a ritual component");
                ritualComponent.SetActive(false);
                break;
        }
    }

    private GameObject CheckPayload(GameObject player)
    {
        //GameObject payload = player.CheckoutPayload();
        //if (payload == null)
        //{
        //    Debug.Log("No payload");
        //    return null;
        //}
        //return payload;
        return null;
    }
}
