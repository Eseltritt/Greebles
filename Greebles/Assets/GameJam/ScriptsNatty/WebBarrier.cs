using UnityEngine;
using UnityEngine.Events;

public class WebBarrier : MonoBehaviour
{
    public GameObject intactWeb;  // Reference to the doorCobweb
    public GameObject damagedWeb; // Reference to the damagedCobweb prefab
    public int hitsToDestroy = 2;
    public UnityEvent onWebDestroyed;

    private int currentHits = 0;
    private BoxCollider intactCollider;
    private BoxCollider damagedCollider;

    private void Awake()
    {
        gameObject.tag = "WebBarrier";
    }


    private void OnEnable()
    {
        if (CatInteraction.Instance != null)
        {
            CatInteraction.Instance.RegisterWebBarrier(this);
        }
        else
        {
            Debug.LogError("CatInteraction instance not found!");
        }
        ResetWeb();
    }

    private void OnDisable()
    {
        if (CatInteraction.Instance != null)
        {
            CatInteraction.Instance.UnregisterWebBarrier(this);
        }
    }

    private void Start()
    {
        // Get the BoxColliders from the intact and damaged web prefabs
        intactCollider = intactWeb.GetComponent<BoxCollider>();
        damagedCollider = damagedWeb.GetComponent<BoxCollider>();

        if (intactCollider == null)
        {
            Debug.LogError("BoxCollider not found on intactWeb!");
        }
        if (damagedCollider == null)
        {
            Debug.LogError("BoxCollider not found on damagedWeb!");
        }

        ResetWeb();
    }

    public void TakeHit()
    {
        currentHits++;
        UpdateWebState();

        if (currentHits >= hitsToDestroy)
        {
            DestroyWeb();
        }
    }

    private void UpdateWebState()
    {
        Debug.Log($"UpdateWebState for {gameObject.name}: Hits = {currentHits}");
        switch (currentHits)
        {
            case 0:
                Debug.Log($"{gameObject.name}: Intact");
                intactWeb.SetActive(true);
                damagedWeb.SetActive(false);
                if (intactCollider != null) intactCollider.enabled = true;
                if (damagedCollider != null) damagedCollider.enabled = false;
                break;
            case 1:
                Debug.Log($"{gameObject.name}: Damaged");
                intactWeb.SetActive(false);
                damagedWeb.SetActive(true);
                if (intactCollider != null) intactCollider.enabled = false;
                if (damagedCollider != null) damagedCollider.enabled = true;
                break;
        }
    }

    private void DestroyWeb()
    {
        Debug.Log($"DestroyWeb for {gameObject.name}!");
        intactWeb.SetActive(false);
        damagedWeb.SetActive(false);
        if (intactCollider != null) intactCollider.enabled = false;
        if (damagedCollider != null) damagedCollider.enabled = false;
        onWebDestroyed.Invoke();
    }

    public void ResetWeb()
    {
        currentHits = 0;
        UpdateWebState();
    }

    public bool IsDestroyed()
    {
        return currentHits >= hitsToDestroy;
    }

    public BoxCollider GetActiveCollider()
    {
        return currentHits == 0 ? intactCollider : damagedCollider;
    }

    public Vector3 GetClosestPoint(Vector3 position)
    {
        BoxCollider activeCollider = GetActiveCollider();
        if (activeCollider != null)
        {
            return activeCollider.ClosestPoint(position);
        }
        return transform.position;
    }

    public bool IsPointInRange(Vector3 position, float range)
    {
        Vector3 closestPoint = GetClosestPoint(position);
        return Vector3.Distance(position, closestPoint) <= range;
    }
}