using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
[TaskCategory("IA SC/Node Move")]
public class ActionStopAgent : ActionNodeVehicle
{
    public override void OnStart()
    {
        base.OnStart();
       
        _AICharacterVehicle = GetComponent<AICharacterVehicle>();
        if (_AICharacterVehicle == null)
            Debug.Log("Not load component AICharacterVehicle");
        UnitSC = this._AICharacterVehicle.Health._Unity;
    }
    public override TaskStatus OnUpdate()
    {
        if (((AICharacterVehicle)(_AICharacterVehicle)).Health.IsDead)
            return TaskStatus.Failure;
        
        SwitchStopAgent();

        return TaskStatus.Success;
       
    }
    void SwitchStopAgent()
    {

        switch (UnitSC)
        {
            
            case UnitSC.Zombie:
                if (_AICharacterVehicle is AICharacterVehicleIAZombie)
                {
                    ((AICharacterVehicleIAZombie)_AICharacterVehicle).StopAgent();
                }
                break;
            
            default:
                
                break;
        }
    }
}
