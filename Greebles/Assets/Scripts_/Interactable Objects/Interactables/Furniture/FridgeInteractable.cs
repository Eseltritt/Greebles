using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class FridgeInteractable : InteractableObject, IHumanInteractable
{
    [SerializeField] private InteractableType _interactableType;
    public override InteractableType interactionType => _interactableType;

    public bool ToBeCorrected { get; set; }
    public bool IsMisplaceable { get; set; } = false;
    public Vector3 InitialPosition { get; set; }
    public Quaternion InitialRotation { get; set; }

    [SerializeField] private GameEvent OnFridgeClosed;
    public bool isSlightlyOpened { get; private set; } = false;
    private bool isFullyOpen = false;
    [SerializeField] public GameObject spawnItem;
    [SerializeField] private Transform spawnTransform;
    [SerializeField] private float spawnForce;
    public float itemSpawnDelay = 1f;

    public Animator animator;

    public override void Catinteraction()
    {
        Debug.Log("cat interaction registered");
        if(isSlightlyOpened && !isFullyOpen)
        {
            Debug.Log("fridge is open");
            animator.SetTrigger("Open");
            Invoke("SpawnItem", itemSpawnDelay);
        }
    }

    private void SpawnItem()
    {
        if(!ToBeCorrected)
            return;

        GameObject item = Instantiate(spawnItem, spawnTransform.position, spawnTransform.rotation);
        Rigidbody rb = item.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.AddForce(item.transform.right *  spawnForce);
        }

    }

    public void OpenSlightly(Component sender)
    {
        animator.SetTrigger("OpenSlightly");
        ToBeCorrected = true;
        isSlightlyOpened = true;
    }

    public void CorrectInteractable()
    {
        animator.SetTrigger("Close");
        OnFridgeClosed?.Raise_WithoutParam(this);
        ToBeCorrected = false;
        isSlightlyOpened = false;
    }
}
