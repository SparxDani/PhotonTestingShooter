using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    public LayerMask mask;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer==(int)Mathf.Log(mask.value,2))
        {
            Health agent = other.gameObject.GetComponent<Health>();
            if (agent != null)
                agent.Damage(WeaponType.ASSAULT_RIFLE, null, 10000);
        }
    }
}
