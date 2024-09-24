using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
[TaskCategory("IA SC/Node State")]
public class ActionIsIdle : ActionDistanceRange
{
    
    public override void OnStart()
    {
        base.OnStart();
        
        
    }
    public override TaskStatus OnUpdate()
    {
        
        if (_AICharacterControl is AICharacterVehicleIAZombie)
        {
            if(((AICharacterVehicleIAZombie)(_AICharacterControl)).IsIdle)
                return TaskStatus.Success;
        }
        return TaskStatus.Failure;

    }

   
}
