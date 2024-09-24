using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime.Tasks;
[TaskCategory("IA SC/ Node Range / Distance")]
public class ActionDistanceLessOneRangeEnemy : ActionDistanceRange
{
        public override void OnStart()
        {
            base.OnStart();
            UnitSC = this._AICharacterControl.Health._Unity;
        }
        public override TaskStatus OnUpdate()
        {
            float distance = _AICharacterControl.AIEye.DistanceEnemy;
            
            switch (UnitSC)
            {
                case UnitSC.Zombie:
                    if (distance < distanceOneLand)
                        return TaskStatus.Success;
                    break;
                
                default:
                    break;
            }

            return TaskStatus.Failure;
        }
        
    //    public override UnitSC GetTypeUnity()
    //    {
    //        if(_AICharacterControl.AIEye.ViewEnemy!=null)
    //            return _AICharacterControl.AIEye.ViewEnemy._Unity;
    //        return UnitSC.None;
    //}

}
