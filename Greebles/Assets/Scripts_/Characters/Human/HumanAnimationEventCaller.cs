using UnityEngine;

public class HumanAnimationEventCaller : MonoBehaviour
{
    public HumanAI human;
    
    public void CallStateUpdate()
    {
        human.StateUpdate();
    }
}
