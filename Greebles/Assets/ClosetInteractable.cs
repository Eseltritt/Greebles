using System.Collections.Generic;
using UnityEngine;

public class ClosetInteractable : InteractableObject, IHumanInteractable
{
    [SerializeField] private InteractableType _interactionType;
    public override InteractableType interactionType => _interactionType;

    public bool ToBeCorrected { get; set; } = false;
    public bool IsMisplaceable { get; set; } = false;
    public Vector3 InitialPosition { get; set; }
    public Quaternion InitialRotation { get; set; }

    [Header("Spawn Settings")]
    public Transform spawnTransform;
    public float spawnForce;
    public GameObject boneToSpawn;
    public GameObject GreebleToSpawn;
    [Tooltip("Bone and greeble Probability are the probability at which these two game objects are spawned when the door is opened. The two values should not add up to more than 1.")]
    public float boneProbability = 0.5f;
    public float greebleProbability = 0.25f;

    private Collider collider;
    private HashSet<GameObject> spawnedObjects = new HashSet<GameObject>();

    [SerializeField] private Animator animatorController;

    public override void Start()
    {
        base.Start();

        collider = gameObject.GetComponent<BoxCollider>();
        collider.isTrigger = false;
    }

    public override void Catinteraction()
    {
        animatorController.SetTrigger("Open");

        ToBeCorrected = true;
        Invoke("SpawnObject", 0.75f);
    }

    public void CorrectInteractable()
    {
        animatorController.SetTrigger("Close");

        ToBeCorrected = false;
    }

    private void SpawnObject()
    {
        float probability = Random.value;

        if( probability <= boneProbability )
        {
            SpawnBone();

            return;
        }

        if( probability <= boneProbability + greebleProbability )
        {
            SpawnGreeble();

            return;
        }
    }

    private void SpawnGreeble()
    {
        collider.isTrigger = true;
        if(!ToBeCorrected)
            return;

        GameObject item = Instantiate(GreebleToSpawn, spawnTransform.position, spawnTransform.rotation);
        AddForceToItem(item);
        spawnedObjects.Add(item);
    }

    private void SpawnBone()
    {
        collider.isTrigger = true;
        if(!ToBeCorrected)
            return;

        GameObject item = Instantiate(boneToSpawn, spawnTransform.position, spawnTransform.rotation);
        
        AddForceToItem(item);
        spawnedObjects.Add(item);
    }

    private void AddForceToItem(GameObject item)
    {
        Rigidbody rb = item.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(item.transform.right * spawnForce);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (spawnedObjects.Contains(other.gameObject))
        {
            spawnedObjects.Remove(other.gameObject);

            if (spawnedObjects.Count == 0)
            {
                collider.isTrigger = false;
            }
        }
    }
}
