using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WeaponHandShoot : WeaponHand
{
    [Header("Position Hand")]
    public Transform Aimposition;
    public Transform IdlePosition;
    public Transform ColliderPosition;

    [Header("IK Hand")]
    public Transform HandRight;
    public Transform HandLeft;
    public Transform RightElbow;
    public Transform LeftElbow;

    [Header("Weapons")]
    public Transform WeaponStatePosition;

    [Header("Speed Animation")]
    public float speedAim;

    [Header("State Hand Wepons")]
    public StateHandWepons state;

    [Header("ThisWeapons")]
    public WeaponsShoot ThisWeapons;

    [Header("Collider Weapon")]
    public Transform pivotColliderPlayer;
    public LayerMask maskCollider;
    public float distanceCollider;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public virtual void Fire()
    {
        if (ThisWeapons.IsEnabled)
        {
            ThisWeapons.UpdateFire();
        }
    }
    // Update is called once per frame
    //void Update()
    //{
    //    RaycastHit hit;
    //    if(state== StateHandWepons.Aim && Physics.Raycast(pivotColliderPlayer.position, pivotColliderPlayer.forward,out hit, distanceCollider,maskCollider))
    //    {
    //        state = StateHandWepons.Collider;
    //    }


    //    switch (state)
    //    {
    //        case StateHandWepons.Aim:
    //            WeaponStatePosition.localPosition = Vector3.Lerp(WeaponStatePosition.localPosition, Aimposition.localPosition, Time.deltaTime * speedAim);
    //            WeaponStatePosition.localRotation = Quaternion.Lerp(WeaponStatePosition.localRotation, Aimposition.localRotation, Time.deltaTime * speedAim);

    //            float dist = (WeaponStatePosition.localPosition - Aimposition.localPosition).magnitude;

    //            if (dist > 0.0005f)
    //            {
    //                DisableFire();
    //            }
    //            else
    //                ActiveFire();
    //            break;
    //        case StateHandWepons.Idle:
    //            {
    //                ThisWeapons.DisableFire();

    //                WeaponStatePosition.localPosition = Vector3.Lerp(WeaponStatePosition.localPosition, IdlePosition.localPosition, Time.deltaTime * speedAim);
    //                WeaponStatePosition.localRotation = Quaternion.Lerp(WeaponStatePosition.localRotation, IdlePosition.localRotation, Time.deltaTime * speedAim);

    //                //float dist2 = (WeaponStatePosition.localPosition - IdlePosition.localPosition).magnitude;


    //            }
    //            break;
    //        case StateHandWepons.Collider:
    //            {
    //                ThisWeapons.DisableFire();

    //                WeaponStatePosition.localPosition = Vector3.Lerp(WeaponStatePosition.localPosition, ColliderPosition.localPosition, Time.deltaTime * speedAim);
    //                WeaponStatePosition.localRotation = Quaternion.Lerp(WeaponStatePosition.localRotation, ColliderPosition.localRotation, Time.deltaTime * speedAim);
    //            }
    //            break;
    //        default:
    //            break;
    //    }
    //}

    public virtual void DisableFire()
    {
        ThisWeapons.DisableFire();
        
    }
    public virtual void ActiveFire()
    {
        ThisWeapons.ActiveFire();
        
    }

    private void OnDrawGizmos()
    {
        if (!IsDrawGizmos) return;
        Gizmos.color = ColorGizmos;
        Gizmos.DrawLine(pivotColliderPlayer.position, pivotColliderPlayer.position + pivotColliderPlayer.forward * distanceCollider);
    }
}
