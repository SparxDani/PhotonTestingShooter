using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime.Tasks;
[TaskCategory("IA SC/Inheritance")]
public class ActionWait : ActionNode
{

    protected AICharacterAction _AICharacterAction;
    protected AICharacterVehicle _AICharacterVehicle;
    protected int index = 0;
    public float[] arrayRate;
    public int bufferSize = 10;
    public float randomWaitMin = 1;
    public float randomWaitMax = 1;

    // The time that the task started to wait.
    // protected float rate;
    public float Framerate =0;
    // Remember the time that the task is paused so the time paused doesn't contribute to the wait time.
    protected float pauseTime;

    public override void OnStart()
    {
        _AICharacterVehicle = GetComponent<AICharacterVehicle>();
        _AICharacterAction = GetComponent<AICharacterAction>();
        if (_AICharacterVehicle == null)
            Debug.Log("Not load component AICharacterVehicle");
        if (_AICharacterAction == null)
            Debug.Log("Not load component AICharacterAction");
        UnitSC = this._AICharacterAction.Health._Unity;

        Framerate = 0;
        arrayRate = new float[bufferSize];
        for (int i = 0; i < arrayRate.Length; i++)
        {
            arrayRate[i] = (float)UnityEngine.Random.Range(randomWaitMin, randomWaitMax);
        }
        
    }

    

    public override void OnReset()
    {
        // Reset the public properties back to their original values
        randomWaitMin = 1;
        randomWaitMax = 1;
    }
}
