using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime.Tasks;
[TaskCategory("IA SC/ Node Range / Eye")]
public class ActionDistanceNotColliderAttack : ActionNodeAction
{
        public override void OnStart()
        {
            base.OnStart();
            _AICharacterAction = GetComponent<AICharacterAction>();
            UnitSC = this._AICharacterAction.Health._Unity;
        }
        public override TaskStatus OnUpdate()
        {
            if(_AICharacterAction.Health.IsDead) return TaskStatus.Failure;

            

            return SwitchUnity();
        }
        TaskStatus SwitchUnity()
    {

        switch (UnitSC)
        {
            
            case UnitSC.Zombie:
                
                    AIEyeAttack _AIEyeAttackZombie = ((AICharacterActionIAZombie)_AICharacterAction).AIEye as AIEyeAttack;

                    if (_AIEyeAttackZombie != null && !_AIEyeAttackZombie.DataViewAttack.Attack)
                    {
                        return TaskStatus.Success;
                    }
                
                
                break;
            
            default:
                break;
        }

        return TaskStatus.Failure;
    }
}
