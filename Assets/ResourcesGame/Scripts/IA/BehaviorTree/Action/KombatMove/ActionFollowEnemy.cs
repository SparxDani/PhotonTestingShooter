using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
[TaskCategory("IA SC/Node Move")]
public class ActionFollowEnemy : ActionNodeVehicle
{
    
    public override void OnStart()
    {
        base.OnStart();
    }
    public override TaskStatus OnUpdate()
    {
        if (((AICharacterVehicle)(_AICharacterVehicle)).Health.IsDead)
            return TaskStatus.Failure;

        SwitchMoveToEnemy();

        return TaskStatus.Success;
       
    }
    void SwitchMoveToEnemy()
    {
       
        switch (UnitSC)
        {
            
            case UnitSC.Zombie:
                if (_AICharacterVehicle is AICharacterVehicleIAZombie)
                {
                    ((AICharacterVehicleIAZombie)_AICharacterVehicle).RunAgent();
                    ((AICharacterVehicleIAZombie)(_AICharacterVehicle)).LookToEnemy();
                    ((AICharacterVehicleIAZombie)(_AICharacterVehicle)).MoveToEnemy();
                }
                break;
            
            default:
                
                break;
        }
    }
}
