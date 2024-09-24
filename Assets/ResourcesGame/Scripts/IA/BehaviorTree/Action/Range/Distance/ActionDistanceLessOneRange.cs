using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime.Tasks;
[TaskCategory("IA SC/ Node Range / Distance")]
public class ActionDistanceLessOneRange : ActionDistanceRange
{
        public override void OnStart()
        {
            base.OnStart();
            _AICharacterControl = GetComponent<AICharacterControl>();
        }
        public override TaskStatus OnUpdate()
        {
           
            float distance = _AICharacterControl.AIEye.DistanceEnemy;
            if (distance>-1 && distance < distanceOneLand)
                return TaskStatus.Success;

            return TaskStatus.Failure;
        }
        

}
