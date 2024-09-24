using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using BehaviorDesigner.Runtime.Tasks;
[TaskCategory("IA SC/Node Base")]
public class ActionNodeVehicle : ActionNode
    {
    protected AICharacterVehicle _AICharacterVehicle;
    public override void OnStart()
    {
        _AICharacterVehicle = GetComponent<AICharacterVehicle>();
        if (_AICharacterVehicle == null)
            Debug.Log("Not load component AICharacterVehicle");
        UnitSC = this._AICharacterVehicle.Health._Unity;

        base.OnStart();
    }
}
