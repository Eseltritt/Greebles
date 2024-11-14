using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Game Event", menuName = "Game Event")]
public class GameEvent : ScriptableObject
{
    public List<GameEventListener> listeners = new List<GameEventListener>();
    
    public void Raise_WithoutParam(Component sender)
    {
        for (int i = 0; i < listeners.Count; i++)
        {
            listeners[i].OnEventRaised_0(sender);
        }
    }

    public void Raise_SingleParam(Component sender, object data)
    {
        for (int i = 0; i < listeners.Count; i++)
        {
            listeners[i].OnEventRaised_1(sender, data);
        }
    }

    public void Raise_DoubleParam(Component sender, object data_1, object data_2)
    {
        for (int i = 0; i < listeners.Count; i++)
        {
            listeners[i].OnEventRaised_2(sender, data_1, data_2);
        }
    }

    public void RegisterListener(GameEventListener listener)
    {
        if (!listeners.Contains(listener))
        {
            listeners.Add(listener);
        }
    }
    
    public void UnregisterListener(GameEventListener listener)
    {
        if (listeners.Contains(listener))
        {
            listeners.Remove(listener);
        }
    }
}
