using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using BehaviorDesigner.Runtime.Tasks;
[TaskCategory("IA SC/Node Base")]
public class ActionNodeAction : ActionNode
{
    protected AICharacterAction _AICharacterAction;
    public override void OnStart()
    {
        _AICharacterAction = GetComponent<AICharacterAction>();
        if (_AICharacterAction == null)
            Debug.Log("Not load component AICharacterVehicle");
        UnitSC = this._AICharacterAction.Health._Unity;

        base.OnStart();
    }
}
