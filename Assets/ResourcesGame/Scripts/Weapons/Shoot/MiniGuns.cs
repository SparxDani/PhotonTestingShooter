using UnityEngine;

public class MiniGuns : WeaponsShoot
{


    public float RateFire;
    float FlameRate = 0;
    bool stop = false;
    public LayerMask mask;
    public TrailRenderer trail;
    public float range;

    [Header("Force hit")]
    public float hitForce;
    public WeaponType typeWeapon;

    public Transform Cannon;
    // Start is called before the first frame update
    void Start()
    {

    }
    void RoolCannon()
    {

        Cannon.Rotate(Vector3.forward * Time.deltaTime * 500, Space.Self);

    }

    public override void UpdateFire()
    {
        if (IsEnabled)
        {
            if (FlameRate > RateFire)
            {
                if (IfCantShoot)
                {
                    HandWeaponsThunder.ActiveHandThunder();
                    EmmiteParticleFlash();
                    Raycast();
                    stop = true;
                }
                FlameRate = 0;
            }
            FlameRate += Time.deltaTime;
        }
        else
            ReleaseFire();

    }
    #region RayCast Master
    void Raycast()
    {
        RoolCannon();
        RaycastHit hit;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        ray.origin = PointShoot.position;
        PointShoot.rotation = Quaternion.LookRotation(ray.direction);

        var trace = Instantiate(trail, PointShoot.position, Quaternion.identity);
        Destroy(trace.gameObject, trail.time);
        trace.AddPosition(PointShoot.position);

        if (Physics.Raycast(ray, out hit, range, mask))
        {
            if (base.IsNotIsThis(Onwer.gameObject, hit.collider.gameObject))
            {
                Health health = hit.collider.gameObject.GetComponent<Health>();

                if (health != null &&
                    !hit.collider.isTrigger &&
                    !health.IsDead)
                {
                    if (!Onwer.health.IsAllies(health))
                    {
                        health.Damage(typeWeapon, Onwer, damage);
                        _ParticleBulletImpactEffect.CreateImpactEffectBullet(hit.collider.gameObject, hit.point, hit.normal);
                    }

                }

                trace.transform.position = hit.point;

                Rigidbody rg = hit.collider.gameObject.GetComponent<Rigidbody>();
                if (rg != null)
                {
                    Vector3 dirForce = (hit.point - PointShoot.position).normalized;
                    rg.AddForceAtPosition(dirForce * hitForce, hit.point, ForceMode.Impulse);
                }

            }

        }
        else
        {
            trace.AddPosition(PointShoot.position + PointShoot.forward * 50f);
        }

    }
    #endregion

    public override void EmmiteParticleFlash()
    {
        foreach (var item in ParticleFlash)
        {
            item.Emite();
        }
    }
    #region RayCast Clone
    public override void RayCastClone(Vector3 pos, Vector3 dir)
    {
        HandWeaponsThunder.ActiveHandThunder();
        EmmiteParticleFlash();
        RaycastHit hit;

        PointShoot.position = pos;
        PointShoot.rotation = Quaternion.LookRotation(dir);

        var trace = Instantiate(trail, PointShoot.position, Quaternion.identity);
        Destroy(trace.gameObject, trail.time);
        trace.AddPosition(PointShoot.position);

        if (Physics.Raycast(pos, dir, out hit, range, mask))
        {
            if (base.IsNotIsThis(Onwer.gameObject, hit.collider.gameObject))
            {
                Health health = hit.collider.gameObject.GetComponent<Health>();

                if (health != null && !hit.collider.isTrigger && !health.IsDead)
                {
                    if (!Onwer.health.IsAllies(health))
                    {
                        health.Damage(typeWeapon, Onwer, damage);
                        _ParticleBulletImpactEffect.CreateImpactEffectBullet(hit.collider.gameObject, hit.point, hit.normal);
                    }

                }

                trace.transform.position = hit.point;


            }

        }
        else
        {
            trace.AddPosition(PointShoot.position + PointShoot.forward * 50f);
        }
    }
    #endregion

    public override void ReleaseFire()
    {
        if (stop)
        {
            foreach (var item in ParticleFlash)
            {
                item.Stop();
            }
            stop = false;
        }



    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(PointShoot.position, PointShoot.position + PointShoot.forward * range);
    }
}
