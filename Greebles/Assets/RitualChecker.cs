using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class RitualChecker : MonoBehaviour
{
    [SerializeField] private RitualPlacement[] ritualSpots;
    
    [SerializeField] private bool allRitualObjectsPlaced;
    [SerializeField] private GameObject itemToSpawn;

    public void Update()
    {
        bool allPlaced = true;

        foreach (var ritualSpot in ritualSpots)
        {
            if(!ritualSpot.objectPlaced)
                allPlaced = false;
        }

        if(allPlaced && !allRitualObjectsPlaced)
        {
            allRitualObjectsPlaced = true;
            SpawnItem();
        }
    }

    private void SpawnItem()
    {
        if(itemToSpawn == null)
            return;

        GameObject item = Instantiate(itemToSpawn);
        item.transform.parent = transform;
        item.transform.localPosition = new Vector3(0,1,0);
        
    }
}
