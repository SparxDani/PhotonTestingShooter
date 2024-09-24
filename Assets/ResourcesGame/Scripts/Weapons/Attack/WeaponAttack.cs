using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttack : Weapons
{
   
    public Collider collider;
    public LayerMask mask;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void ActiveCollider()
    {
        collider.enabled = true;
    }
    public void DesactiveCollider()
    {
        collider.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!base.IsNotIsThis(Onwer.gameObject, other.gameObject)) return;

        if (other.gameObject.layer==(int)Mathf.Log(mask.value,2))
        {
            Health health = other.gameObject.GetComponent<Health>();

            if (health != null  && !health.IsDead)
            {
                if (!Onwer.health.IsAllies(health))
                    health.Damage(weaponType, Onwer, damage);

            }

        }
    }
}
