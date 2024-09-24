using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime.Tasks;
[TaskCategory("IA SC/Node Attack")]
public class ActionHurtingMe: ActionNodeAction
{ 
        public override void OnStart()
        {
            base.OnStart();
            _AICharacterAction = GetComponent<AICharacterAction>();
        }
        public override TaskStatus OnUpdate()
        {
           
            if (((HealthIA)_AICharacterAction.Health).HurtingMe !=null)
            {
                return TaskStatus.Success;
            }
            return TaskStatus.Failure;
        }
        

    }
