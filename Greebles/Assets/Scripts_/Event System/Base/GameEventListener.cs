using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CustomgameEvent_NoParam : UnityEvent<Component> {}
[System.Serializable]
public class CustomgameEvent_OneParam : UnityEvent<Component, object> {}
[System.Serializable]
public class CustomgameEvent_TwoParam : UnityEvent<Component, object, object> {}

public class GameEventListener : MonoBehaviour
{   
    public GameEvent gameEvent;

    public CustomgameEvent_NoParam response_0;
    public CustomgameEvent_OneParam response_1;
    public CustomgameEvent_TwoParam response_2;

    private void OnEnable()
    {
        gameEvent.RegisterListener(this);
    }

    private void OnDisable()
    {
        gameEvent.UnregisterListener(this);
    }

    public void OnEventRaised_0(Component sender)
    {
        response_0?.Invoke(sender);
    }

    public void OnEventRaised_1(Component sender, object data)
    {
        response_1?.Invoke(sender, data);
    }

    public void OnEventRaised_2(Component sender, object data_1, object data_2)
    {
        response_2?.Invoke(sender, data_1, data_2);
    }
}
