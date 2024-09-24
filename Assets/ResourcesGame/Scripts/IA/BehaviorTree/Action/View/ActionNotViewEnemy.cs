using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime.Tasks;
[TaskCategory("IA SC/ Node View")]
public class ActionNotViewEnemy : ActionView
{
        public override void OnStart()
        {
            base.OnStart();
        }
        public override TaskStatus OnUpdate()
        {
            if (_AICharacterAction.AIEye.ViewEnemy == null)
            {
                return TaskStatus.Success;
            }

            return TaskStatus.Failure;
        }
        

    }
