using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime.Tasks;
[TaskCategory("IA SC/ Node Range / Eye")]
public class ActionDistanceColliderAttack : ActionNodeAction
{
        public override void OnStart()
        {
            base.OnStart();
           
        }
    public override TaskStatus OnUpdate()
    {
        if (_AICharacterAction.Health.IsDead) return TaskStatus.Failure;



        return SwitchUnity();
    }
    TaskStatus SwitchUnity()
    {

        switch (UnitSC)
        {
            
            case UnitSC.Zombie:

                AIEyeAttack _AIEyeAttackZombie = ((AICharacterActionIAZombie)_AICharacterAction).AIEye as AIEyeAttack;

                if (_AIEyeAttackZombie != null && _AIEyeAttackZombie.DataViewAttack.Attack)
                {
                    return TaskStatus.Success;
                }


                break;
            
            case UnitSC.None:
                break;
            default:
                break;
        }

        return TaskStatus.Failure;
    }


}
