
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AICharacterVehicleIAZombie : AICharacterVehicleLand
{

   
    // Start is called before the first frame update
    void Start()
    {
        this.LoadComponent();
    }
    public override void LoadComponent()
    {
        character = GetComponent<ThirdPersonCharacterAnimatorZombie>();
        base.LoadComponent();
    }

    public override void MoveToEnemy()
    {
        //((SoundCharacterIAUnityCombatTerrain)SoundCharacterIA).PlayInKombat();
        base.MoveToEnemy();
    }
    public override void MoveToPositionEvade()
    {
        //((SoundCharacterIAUnityCombatTerrain)SoundCharacterIA).PlayHelp();
        base.MoveToPositionEvade();
    }
    public override void MoveToPositionWanderEnemy()
    {
        //((SoundCharacterIAUnityCombatTerrain)SoundCharacterIA).PlayInKombat();
        base.MoveToPositionWanderEnemy();
    }
    public override void MoveToPositionWander()
    {
        base.MoveToPositionWander();
    }
    public override void MoveToPosition(Vector3 position)
    {
        base.MoveToPosition(position);
    }
    public override void MoveToPositionStrafe()
    {
        //((SoundCharacterIAUnityCombatTerrain)SoundCharacterIA).PlayInKombat();
        base.MoveToPositionStrafe();
    }
    public override void MoveToAllied()
    {
        base.MoveToAllied();
    }
    public override void StopAgent()
    {
        base.StopAgent();
        character.Move(Vector3.zero, false, _AIEye.DirectionEnemy);
    }
    void OnDrawGizmos()
    {
        if (!IsDrawGizmos) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(PositionWander, 1);

        if (_AIEye != null)
        {
            if (_AIEye.ViewEnemy != null)
            {
               // UnityEditor.Handles.Label(transform.position + Vector3.left , "DE:" + _AIEye.DistanceEnemy.ToString("0.0"));
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(_AIEye.ViewEnemy.transform.position, 1);
                Gizmos.DrawLine(_AIEye.ViewEnemy.transform.position, transform.position);
            }
            
        }
    }
}
