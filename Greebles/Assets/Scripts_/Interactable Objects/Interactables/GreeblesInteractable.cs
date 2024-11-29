using Unity.Mathematics;
using Unity.Multiplayer.Center.Common.Analytics;
using Unity.VisualScripting;
using UnityEngine;

public class GreeblesInteractable : InteractableObject
{
    [SerializeField] protected InteractableType _interactionType = InteractableType.Hit;

    public override InteractableType interactionType => _interactionType;

    [SerializeField]private bool isDead = false;
    [SerializeField] private GameObject deathAnim;
    private GreeblesAI greeblesAI;
    [SerializeField]private float sizePercentage = 1;
    [SerializeField] private float shrinkTime = 5;

    Vector3 initialSize;

    public override void Start()
    {
        base.Start();

        greeblesAI = gameObject.GetComponent<GreeblesAI>();
        initialSize = gameObject.transform.localScale;
    }

    public override void Catinteraction()
    {
        Die();
    }

    void Update()
    {
        if (isDead)
            ShrinkAndDestroy();
    }

    public void Die()
    {
        isDead = true;

        GameObject deathAnimObj = Instantiate(deathAnim, transform.position, quaternion.identity);
        deathAnimObj.transform.parent = gameObject.transform;

        greeblesAI.SetNavSpeed(0);
    }

    public void ShrinkAndDestroy()
    {
        if (sizePercentage > 0)
            sizePercentage -= Time.deltaTime / shrinkTime;
        /* else
            Destroy(gameObject); */

        gameObject.transform.localScale = initialSize * sizePercentage;
    }
}
