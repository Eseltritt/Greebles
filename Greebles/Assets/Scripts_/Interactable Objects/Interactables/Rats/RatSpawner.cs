using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RatSpawner : MonoBehaviour
{
    [SerializeField] private GameObject rat;
    [SerializeField] private int spawnAmount;

    [SerializeField] private Transform[] roomDestinations;
    [SerializeField] private GameEvent OnAllRatsHidden;
    private List<RatAI> ratsInPlay = new List<RatAI>();
    private List<RatAI> hiddenRats = new List<RatAI>();
    private int hiddenAmount;

    void Start()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            Instantiate(rat, gameObject.transform.position, quaternion.identity);
            RatAI ratAI = rat.GetComponent<RatAI>();
            ratAI.SetUp(gameObject, roomDestinations);

            rat.name = "rat_" + i;
            ratsInPlay.Add(ratAI);
        }
    }

    public void UpdateHiddenRats(Component sender, object isHidden)
    {
        if (sender is RatAI && isHidden is bool)
        {
            if ((bool)isHidden)
                hiddenAmount ++;
            else
                hiddenAmount --;

            if (hiddenAmount == ratsInPlay.Count)
                OnAllRatsHidden?.Raise_WithoutParam(this);
        }
    }

    public void SetStatesIdle()
    {
        foreach (var rat in ratsInPlay)
        {
            rat.fridgeOpen = false;
            rat.SetState(rat.IdleState);
        }
    }
}
