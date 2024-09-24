using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
[TaskCategory("IA SC/Node Move")]
public class ActionFollowAttackEnemy : ActionNodeVehicle
{
    
    public override void OnStart()
    {
        base.OnStart();
    }
    public override TaskStatus OnUpdate()
    {
        if (((AICharacterVehicle)_AICharacterVehicle).Health.IsDead)
            return TaskStatus.Failure;

        SwitchMoveToAttackEnemy();

        return TaskStatus.Success;
       
    }
    void SwitchMoveToAttackEnemy()
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
