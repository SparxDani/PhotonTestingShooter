using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime.Tasks;
[TaskCategory("IA SC/ Node Range / Distance")]
public class ActionDistanceGreaterOneRange : ActionDistanceRange
{
        
        public override void OnStart()
        {
            base.OnStart();
            _AICharacterControl = GetComponent<AICharacterControl>();
        }
        public override TaskStatus OnUpdate()
        {
            float distance = _AICharacterControl.AIEye.DistanceEnemy;
            if (distance > distanceOneLand)
                return TaskStatus.Success;
        
            return TaskStatus.Failure;
        }
        
}
