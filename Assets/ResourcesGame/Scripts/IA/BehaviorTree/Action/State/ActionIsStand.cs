using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
[TaskCategory("IA SC/Node State")]
public class ActionIsStand : ActionNodeVehicle
{
    public override void OnStart()
    {
        base.OnStart();
       
    }
    public override TaskStatus OnUpdate()
    {
        
        //if (_AICharacterVehicle is AICharacterVehicleLandBurrow)
        //{
        //    if (((AICharacterVehicleLandBurrow)(_AICharacterVehicle)).IsStand)
        //    {
        //       // Debug.Log("IsStand");
        //        return TaskStatus.Success;
        //    }
                
        //}
        return SwitchUnity();

    }
    TaskStatus SwitchUnity()
    {
        switch (UnitSC)
        {
            case UnitSC.Zombie:
                if (_AICharacterVehicle is AICharacterVehicleIAZombie)
                {
                    if (((AICharacterVehicleIAZombie)(_AICharacterVehicle)).IsStand)
                    {
                        return TaskStatus.Success;
                    }

                }
                
                break;
            
            default:
                break;
        }
        return TaskStatus.Failure;

    }

}
