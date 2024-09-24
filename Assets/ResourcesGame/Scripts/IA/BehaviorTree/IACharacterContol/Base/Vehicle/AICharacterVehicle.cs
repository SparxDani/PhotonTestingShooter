using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AICharacterVehicle : AICharacterControl
{
    public Vector3 centerWader { set; get; }
    public float RangleWander=20;
    public override void LoadComponent()
    {
        base.LoadComponent();
        centerWader = transform.position;
    }
    #region CalculATE
    public virtual void CalculatePositionWander()
    {

    }
    public virtual void CalculatePositionWanderEnemy()
    {

    }
    public virtual Vector3 CalculatePositionEvade()
    {
        return Vector3.zero;
    }
    #endregion
    #region Move
    public virtual void KeepDiscanteAgent(float range)
    {
    }
    public virtual void MoveToPositionEvade()
    {
    }
    public virtual void MoveToPositionWanderEnemy()
    {
    }
    public virtual void MoveToPositionWander()
    {

    }
    public virtual void MoveToPosition(Vector3 position)
    {
    }
    public virtual void MoveToPositionStrafe()
    {

    }
    public virtual void MoveToAllied()
    {

    }
    public virtual void MoveToEnemy()
    {

    }
    #endregion
     
}
