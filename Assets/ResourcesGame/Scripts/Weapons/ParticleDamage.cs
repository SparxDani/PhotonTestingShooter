using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDamage : MonoBehaviour
{
    [Header("Onwer")]
    public InfoPlayer Onwer;

    public int damage;

    ParticleSystem part;
    public WeaponType typeWeapon;

    List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

    private void Start()
    {
        part = GetComponent<ParticleSystem>();
    }

    void OnParticleCollision(GameObject other)
    {

        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);

        for (int i = 0; i < numCollisionEvents; i++)
        {
            HealthPlayer health = other.GetComponent<HealthPlayer>();
            if (health != null && !health.IsDead)
            {
                health.Damage(typeWeapon, Onwer, damage);
            }
        }

    }
}
