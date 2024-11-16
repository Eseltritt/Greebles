using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HumanAI : NavAgent
{
    private List<Transform> _defaultMoveTargets = new List<Transform>();
    private Transform _previousTarget;
    private List<InteractableObject> _misplacedObjects = new List<InteractableObject>();

    public override void Start(){
        base.Start();

        

        AssigneDefaultTarget();
    }

    public override void Update(){
        base.Update();

        if (!hasTarget)
            AssigneDefaultTarget();
    }

    #region AI
        
    private void AssigneDefaultTarget()
    {

    }

    private void MisplacedObjectCheck()
    {

    }

    void OnTriggerEnter(Collider other){
        HitObject _interactable = other.gameObject?.GetComponent<HitObject>();

        if (_interactable == null)
            return;

        _misplacedObjects.Add(_interactable);
    }

    #endregion
}
