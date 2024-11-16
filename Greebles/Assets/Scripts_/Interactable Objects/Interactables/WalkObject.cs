using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;


[RequireComponent(typeof(NavMeshModifier))]
public class WalkObject : InteractableObject
{
    [SerializeField] private InteractableType _interactionType = InteractableType.Walk;

    public override InteractableType interactionType => _interactionType;

    public Transform overrideDestination;
}
