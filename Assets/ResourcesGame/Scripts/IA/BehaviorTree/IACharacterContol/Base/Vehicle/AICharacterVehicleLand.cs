using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AICharacterVehicleLand : AICharacterVehicle
{
    protected Vector3 PositionWander;
    protected NavMeshAgent agent { get ; set ; }
    
    protected int indexPath = 0;
    protected float elapsed = 0.0f;
    float currentSpeed;

    #region Rate
    protected int index = 0;
    protected float[] arrayRate;
    int bufferSize = 10;
    public float randomWaitMin = 1;
    public float randomWaitMax = 1;
    protected float Framerate = 0;
    #endregion
    public LogicDiffuseLand _LogicDiffuseLand { set; get; }
    public override void LoadComponent()
    {
        agent = GetComponent<NavMeshAgent>();
        currentSpeed = agent.speed;
        indexPath = 0;
        FrameRateInit();
        base.LoadComponent();
    }
    #region Calculate
    void FrameRateInit()
    {
        Framerate = 0;
        arrayRate = new float[bufferSize];
        for (int i = 0; i < arrayRate.Length; i++)
        {
            arrayRate[i] = (float)UnityEngine.Random.Range(randomWaitMin, randomWaitMax);
        }
    }
    public void UpdateSpeed(Vector3 target)
    {
        if (!this.agent.enabled) return;
        if (agent != null && !agent.isStopped)
        {
            float distance = (transform.position - target).magnitude;

            distance = (distance == 0) ? 1 : distance;

            float speed = _LogicDiffuseLand.SpeedDependDistancePosition.CalculateFuzzy(distance);

            agent.speed = currentSpeed * speed;

            //if (distance <= character._MoveSpeedMultiplierMax)
            //{
            //    speed = character._MoveSpeedMultiplierMax / distance;
            //    speed = Mathf.Clamp01(speed);

            //    agent.speed = currentSpeed * speed;
            //}
            //else
            //agent.speed = currentSpeed;

            //Debug.Log("agent.speed: "+ agent.speed + " currentSpeed: " + currentSpeed + " distance: "+ distance + " speed: " +speed);
        }




    }
    public void UpdatePath(Vector3 position)
    {
        if (health.IsDead)
        {
            StopAgent();
            return;
        };

        elapsed += Time.deltaTime;
        if (elapsed > 1f)
        {
            elapsed -= 1.0f;
            agent.ResetPath();
            NavMeshPath path2 = new NavMeshPath();
            NavMesh.CalculatePath(transform.position, position, NavMesh.AllAreas, path2);
            agent.SetPath(path2);
        }



        if (this.agent.remainingDistance > this.agent.stoppingDistance)
        {
            (this.character).Move(this.agent.desiredVelocity, false, transform.forward);
        }
        else
        {
            (this.character).Move(Vector3.zero, false, transform.forward);
        }
    }
    public bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        result = center + Random.insideUnitSphere * range;
        result.y = center.y;
        RaycastHit rayHit;
        if (Physics.Raycast(result, Vector3.down,out rayHit, 10000))
        {
            result.y = rayHit.point.y;

        }
        NavMeshHit hit;
        if (NavMesh.SamplePosition(result, out hit, 1.0f, NavMesh.AllAreas))
        {

            result = hit.position;
            Vector3 dir = (hit.position - center);

            Ray ray = new Ray(center, dir.normalized);
            RaycastHit rayHit2;
            if (Physics.Raycast(ray, out rayHit2, dir.magnitude))
            {
                result = rayHit2.point + rayHit2.normal * 2;

            }

            return true;
        }
        return false;
    }
    public override void CalculatePositionWanderEnemy()
    {

        if (_AIEye.ViewEnemy != null)
            RandomPoint(_AIEye.ViewEnemy.transform.position, RangleWander, out PositionWander);
        else
        if (_AIEye.Memory != null)
            RandomPoint(_AIEye.Memory, RangleWander, out PositionWander);
        else
            RandomPoint(centerWader, RangleWander, out PositionWander);

    }
    public override void CalculatePositionWander()
    {
        RandomPoint(transform.position, RangleWander, out PositionWander);
    }
    public override Vector3 CalculatePositionEvade()
    {
        Vector3 back = Vector3.zero;
        Ray ray = new Ray(transform.position, transform.forward);

        RaycastHit rayHit;

        if (Physics.Raycast(ray, out rayHit, 5))
        {
            Vector3 bounceDirection = Vector3.Reflect(transform.forward, rayHit.normal);
            back = rayHit.point + bounceDirection * 2;
        }

        return back;
    }
    #endregion
    #region Move
    public override void MoveToPosition(Vector3 position)
    {
        UpdatePath(position);
    }
    public override void MoveToPositionEvade()
    {
        Vector3 diff = this.transform.position - _AIEye.ViewEnemy.transform.position;
        float head_EvadeDependCountEnemy = _LogicDiffuseLand.EvadeDependCountEnemy.CalculateFuzzy(((AIEyeAttack)_AIEye).CountEnemyView);
        float head_EvadeDependDistanceEnemy = _LogicDiffuseLand.EvadeDependDistanceEnemy.CalculateFuzzy(((AIEyeAttack)_AIEye).DistanceEnemy);
        Vector3 futurePosition = this.transform.position + diff.normalized * (head_EvadeDependCountEnemy + head_EvadeDependDistanceEnemy);
        PositionWander = futurePosition;
        UpdatePath(PositionWander);
    }
    public override void MoveToPositionWanderEnemy()
    {
        MoveToPositionWander();
    }
    public override void MoveToPositionWander()
    {
        if (health.IsDead)
        {
            StopAgent();
            return;
        };

        GetCenterWander();

        float dist = (transform.position - PositionWander).magnitude;

        if (dist <= 2f)
        {
            this.CalculatePositionWander();
        }
        else
            if (Framerate > arrayRate[index])
        {
            index++;
            index = index % arrayRate.Length;
            this.CalculatePositionWander();
            Framerate = 0;
        }

        Framerate += Time.deltaTime;

        UpdatePath(PositionWander);
    }
    public override void MoveToPositionStrafe()
    {

    }
    public override void MoveToAllied()
    {

    }
    public override void MoveToEnemy()
    {

        if (_AIEye.ViewEnemy == null) return;
        if (health.IsDead)
        {
            StopAgent();
            return;
        };
        UpdatePath(_AIEye.ViewEnemy.transform.position);
    }

    public void GetCenterWander()
    {
        if (_AIEye.ViewEnemy == null)
        {
                centerWader = transform.position;
        }
        else
        {
            centerWader = _AIEye.ViewEnemy.transform.position;
        }

    }
    #endregion
    #region Agent
    public virtual void StopAgent()
    {
        if (this.agent.enabled && !agent.isStopped)
        {
            agent.isStopped = true;
            (this.character).Move(Vector3.zero, false, transform.forward);
        }


    }
    public virtual bool IsStopAgent()
    {
        return agent.isStopped;


    }
    public virtual void RunAgent()
    {

        if (this.agent.enabled && agent.isStopped)
            agent.isStopped = false;
    }
    #endregion

}
