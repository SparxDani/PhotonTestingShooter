using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : WeaponsShoot
{
    public LayerMask mask;
    public float range;
   
    [Header("Force hit")]
    public float hitForce;
    public WeaponType typeWeapon;
    [Header("Radius Damage")]
    public float radius;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void UpdateFire()
    {
        if (IsEnabled)
        {
                if (IfCantShoot)
                {
                    HandWeaponsThunder.ActiveHandThunder();
                    EmmiteParticleFlash();
                    Raycast();
                }
        }
        else
            ReleaseFire();

    }
    void Raycast()
    {

        RaycastHit hit;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        ray.origin = PointShoot.position;
        PointShoot.rotation = Quaternion.LookRotation(ray.direction);
        
        if (Physics.SphereCast(ray.origin, radius, ray.direction, out hit, range, mask))
        {
            if (!base.IsNotIsThis(Onwer.gameObject, hit.collider.gameObject)) return;

            Health health = hit.collider.gameObject.GetComponent<Health>();

            if (health != null && !hit.collider.isTrigger && !health.IsDead)
            {
                if (!Onwer.health.IsAllies(health))
                {

                    health.Damage(typeWeapon, Onwer, damage);
                    _ParticleBulletImpactEffect.CreateImpactEffectBullet(hit.collider.gameObject, hit.point, hit.normal);
                }

            }

            

            Rigidbody rg = hit.collider.gameObject.GetComponent<Rigidbody>();
            if (rg != null)
            {
                Vector3 dirForce = (hit.point - PointShoot.position).normalized;
                rg.AddForceAtPosition(dirForce * hitForce, hit.point, ForceMode.Impulse);
            }
        }
        

    }
    public override void RayCastClone(Vector3 pos, Vector3 dir)
    {
        HandWeaponsThunder.ActiveHandThunder();
        EmmiteParticleFlash();

        RaycastHit hit;

        PointShoot.position = pos;
        PointShoot.rotation = Quaternion.LookRotation(dir);

        if (Physics.SphereCast(pos, radius, dir, out hit, range, mask))
        {
            if (!base.IsNotIsThis(Onwer.gameObject, hit.collider.gameObject)) return;
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


            

            Rigidbody rg = hit.collider.gameObject.GetComponent<Rigidbody>();
            if (rg != null)
            {
                Vector3 dirForce = (hit.point - PointShoot.position).normalized;
                rg.AddForceAtPosition(dirForce * hitForce, hit.point, ForceMode.Impulse);
            }
        }

    }
    public override void EmmiteParticleFlash()
    {
        foreach (var item in ParticleFlash)
        {
            item.Emite();
        }
    }
    
    public override void ReleaseFire()
    {
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(PointShoot.position, PointShoot.position + PointShoot.forward * range);
        Gizmos.DrawWireSphere(PointShoot.position, radius);
    }
}
