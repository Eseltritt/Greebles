using System;
using Unity.VisualScripting;
using UnityEngine;

public enum RatState
{
    Idle,
    Escape,
    Wait
}

public class RatAI : MonoBehaviour
{
    private RatState _state;
    private Transform _escapeDestination;

    [SerializeField] private float _waitTime;
    [SerializeField] private float _idleUpdateRate;

    public RatAI(Transform dest)
    {
        _escapeDestination = dest;

    }

    void Start()
    {
        _state = RatState.Idle;
    }

    void OnEnable()
    {
        //Event when cat interacts with this Rat
    }

    void OnDisable()
    {
        //Event when cat interacts with this Rat
    }

    void SetState(RatState state)
    {
        switch (state)
        {
            case RatState.Idle:

            break;
            case RatState.Escape:

            break;
            case RatState.Wait:
                Invoke("EndWait", _waitTime);
            break;
        }
    }

    void EndWait()
    {

    }
}
