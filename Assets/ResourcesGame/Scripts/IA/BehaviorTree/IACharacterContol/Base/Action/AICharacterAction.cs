using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICharacterAction : AICharacterControl
{
    #region FrameRate
    protected int index = 0;
    protected float[] arrayRate;
    public bool ActiveFire = true;
    protected float Framerate = 0;
    public int bufferSize = 10;
    public float randomWaitMin = 1;
    public float randomWaitMax = 1;
    #endregion
   
    public void LoadFrameRate()
    {
        Framerate = 0;
        arrayRate = new float[bufferSize];
        for (int i = 0; i < arrayRate.Length; i++)
        {
            arrayRate[i] = (float)UnityEngine.Random.Range(randomWaitMin, randomWaitMax);
        }
    }
    public virtual void FrameRateUpdateFire()
    {
        if (Framerate > arrayRate[index])
        {
            index++;
            index = index % arrayRate.Length;
            ActiveFire = false;
            Framerate = 0;

        }
        if (ActiveFire)
            Framerate += Time.deltaTime;
    }
    public override void LoadComponent()
    {
        LoadFrameRate();
        base.LoadComponent();
    }
    
}
