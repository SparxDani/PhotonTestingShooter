using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
[TaskCategory("IA SC/Node Move")]
public class ActionWanderWait : ActionWait
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
        if (_AICharacterVehicle.Health.IsDead)
            return TaskStatus.Failure;

        //if (_AICharacterVehicle.AIEye != null && _AICharacterVehicle.AIEye.ViewEnemy != null)
        //    return TaskStatus.Failure;

        if (Framerate > arrayRate[index])
        {
            index++;
            index = index % arrayRate.Length;
            Framerate = 0;
            return TaskStatus.Failure;
        }

        Framerate += Time.deltaTime;
        SwitchMoveToPositionWander();
        return TaskStatus.Running;
        
    }

    void SwitchMoveToPositionWander()
    {

        switch (UnitSC)
        {
            case UnitSC.Zombie:

                if (_AICharacterVehicle is AICharacterVehicleIAZombie)
                {
                    ((AICharacterVehicleIAZombie)_AICharacterVehicle).RunAgent();
                    ((AICharacterVehicleIAZombie)_AICharacterVehicle).MoveToPositionWander();
                }
                break;
            
            default:

                break;
        }
    }
}
