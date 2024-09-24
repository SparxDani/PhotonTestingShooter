using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime.Tasks;
[TaskCategory("IA SC/Inheritance")]
public class ActionWaitInPosition : ActionWait
{
    
    public override void OnStart()
    {
        base.OnStart();
        Framerate = 0;
        _AICharacterVehicle = GetComponent<AICharacterVehicle>();
        
        if (_AICharacterVehicle == null)
            Debug.Log("Not load component AICharacterVehicle");
        UnitSC = this._AICharacterVehicle.Health._Unity;
    }

    public override TaskStatus OnUpdate()
    {
        if (_AICharacterVehicle.Health.IsDead)
            return TaskStatus.Failure;

        if (_AICharacterVehicle.AIEye.ViewEnemy!=null)
            return TaskStatus.Failure;

        if (Framerate > arrayRate[index])
        {
            index++;
            index = index % arrayRate.Length;
            Framerate = 0;
            return TaskStatus.Failure;
        }

        Framerate += Time.deltaTime;

        SwitchWaitInPosition();

        
        return TaskStatus.Running;
    }
    void SwitchWaitInPosition()
    {

        switch (UnitSC)
        {
            
            case UnitSC.Zombie:
                if (_AICharacterVehicle is AICharacterVehicleIAZombie)
                {
                    ((AICharacterVehicleIAZombie)_AICharacterVehicle).StopAgent();
                    ((AICharacterVehicleIAZombie)_AICharacterVehicle).LookToEnemy();
                }
                break;
            
            default:

                break;
        }
    }
    public override void OnPause(bool paused)
    {
        //if (paused)
        //{
        //    // Remember the time that the behavior was paused.
        //    pauseTime = Time.time;
        //}
        //else
        //{
        //    // Add the difference between Time.time and pauseTime to figure out a new start time.
        //    startTime += (Time.time - pauseTime);
        //}
    }

    public override void OnReset()
    {
        // Reset the public properties back to their original values
        randomWaitMin = 1;
        randomWaitMax = 1;
    }


}
