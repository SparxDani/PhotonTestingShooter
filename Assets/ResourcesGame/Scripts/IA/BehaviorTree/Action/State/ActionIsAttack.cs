using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
[TaskCategory("IA SC/Node State")]
public class ActionIsAttack : ActionDistanceRange
{
   
    public override void OnStart()
    {
        base.OnStart();
        
       
    }
    public override TaskStatus OnUpdate()
    {
        if(((AICharacterVehicleLand)(_AICharacterControl)).IsAttack)
                return TaskStatus.Success;
        return TaskStatus.Failure;

    }

   
}
