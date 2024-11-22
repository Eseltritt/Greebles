using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class StateMachine
{
    IState _currentState ;

    private Dictionary<Type, List<Transition>> _transitions = new Dictionary<Type, List<Transition>>();
    private List<Transition> _currentTransitions = new List<Transition>();
    /* private List<Transition> _anyTransitions = new List<Transition>(); */
    private static List<Transition> EmptyTransitions = new List<Transition>();

    //private Dictionary[]

    public void StateMachineUpdate()
    {
        var transition = GetTransition();

        if (transition != null)
            SetState(transition.To);
        
        _currentState?.StateUpdate();
    }

    public void SetState(IState state)
    {
        if (state == _currentState)
            return;

        _currentState.Exit();
        _currentState = state;

        _transitions.TryGetValue(_currentState.GetType(), out _currentTransitions);
        if (_currentTransitions != null)
        {
            _currentTransitions = EmptyTransitions;
        }

        _currentState.Enter();
    }

    public void AddTransition(IState from, IState to, Func<bool> predicate)
    {
        if (_transitions.TryGetValue(from.GetType(), out List<Transition> transitions) == false)  // Checks if transitions for a certain state exist in Dict _transitions
        {
            transitions = new List<Transition>();
            _transitions[from.GetType()] = transitions;
        }

        transitions.Add(new Transition(to, predicate));
    }

    private Transition GetTransition()
    {
        foreach (var transition in _currentTransitions)
        {
            if(transition.Condition())
                return transition;
        }

        return null;
    }

    class Transition
    {
        public IState To { get; }
        public Func<bool> Condition { get; }

        public Transition(IState to, Func<bool> predicate)
        {
            To = to;
            Condition = predicate;
        }
    }
}
