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

    void Start()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            Instantiate(rat, gameObject.transform.position, quaternion.identity);
            RatAI ratAI = rat.GetComponent<RatAI>();
            ratAI.SetUp(gameObject, roomDestinations);

            ratsInPlay.Add(ratAI);
        }
    }

    public void UpdateHiddenRats(Component sender)
    {
        if (sender is RatAI)
        {
            hiddenRats.Add((RatAI)sender);

            bool allRatsHidden = true;

            foreach (var rat in ratsInPlay)
            {
                if (!hiddenRats.Contains(rat))
                    allRatsHidden = false;
            }

            if (allRatsHidden)
            {
                OnAllRatsHidden?.Raise_WithoutParam(this);

                foreach (var rat in ratsInPlay)
                {
                    rat.fridgeOpen = true;
                }
            }
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
