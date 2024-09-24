using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime.Tasks;
[TaskCategory("IA SC/Node Attack")]
public class ActionNotHurtingMe: ActionNodeAction
{

    public override void OnStart()
    {
        base.OnStart();
        _AICharacterAction = GetComponent<AICharacterAction>();
    }
    public override TaskStatus OnUpdate()
    {

        if (((Health)_AICharacterAction.Health).HurtingMe == null)
        {
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }



}
