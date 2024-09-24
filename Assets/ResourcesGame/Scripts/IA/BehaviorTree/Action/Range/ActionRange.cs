using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime.Tasks;
[TaskCategory("IA SC/ Node Range")]
public class ActionRange : ActionNode
{
    public float distanceOneLand;
    protected AICharacterControl _AICharacterControl;
    public override void OnStart()
        {
            base.OnStart();
            _AICharacterControl = GetComponent<AICharacterControl>();
          
            if (_AICharacterControl == null)
                Debug.Log("Not load component AICharacterVehicle");
            UnitSC = this._AICharacterControl.Health._Unity;
        }
        
        

}
