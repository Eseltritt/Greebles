using Unity.AI.Navigation;
using UnityEngine;

public interface IWalkable
{   
    public NavMeshModifier navMeshModifier { set; }

    public void BakeNavmeshArea();
}
