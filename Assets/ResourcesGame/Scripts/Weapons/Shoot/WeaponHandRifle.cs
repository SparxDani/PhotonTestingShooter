using UnityEngine;
public class WeaponHandRifle : WeaponHandShoot
{


    // Update is called once per frame
    void Update()
    {
        //RaycastHit hit;
        //if (state == StateHandWepons.Aim && Physics.Raycast(pivotColliderPlayer.position, pivotColliderPlayer.forward, out hit, distanceCollider, maskCollider))
        //{
        //    state = StateHandWepons.Collider;
        //}

        switch (state)
        {
            case StateHandWepons.Aim:
                WeaponStatePosition.localPosition = Vector3.Lerp(WeaponStatePosition.localPosition, Aimposition.localPosition, Time.deltaTime * speedAim);
                WeaponStatePosition.localRotation = Quaternion.Lerp(WeaponStatePosition.localRotation, Aimposition.localRotation, Time.deltaTime * speedAim);

                float angle = Vector3.Angle(WeaponStatePosition.forward, Aimposition.forward);

                float dist = (WeaponStatePosition.localPosition - Aimposition.localPosition).magnitude;

                if (dist > 0.0005f)
                {
                    DisableFire();
                }
                else
                    if (angle < 0.05f)
                    ActiveFire();
                break;
            case StateHandWepons.Idle:
                {
                    ThisWeapons.DisableFire();

                    WeaponStatePosition.localPosition = Vector3.Lerp(WeaponStatePosition.localPosition, IdlePosition.localPosition, Time.deltaTime * speedAim);
                    WeaponStatePosition.localRotation = Quaternion.Lerp(WeaponStatePosition.localRotation, IdlePosition.localRotation, Time.deltaTime * speedAim);

                    //float dist2 = (WeaponStatePosition.localPosition - IdlePosition.localPosition).magnitude;


                }
                break;
            case StateHandWepons.Collider:
                {
                    ThisWeapons.DisableFire();

                    WeaponStatePosition.localPosition = Vector3.Lerp(WeaponStatePosition.localPosition, ColliderPosition.localPosition, Time.deltaTime * speedAim);
                    WeaponStatePosition.localRotation = Quaternion.Lerp(WeaponStatePosition.localRotation, ColliderPosition.localRotation, Time.deltaTime * speedAim);
                }
                break;
            default:
                break;
        }
    }
    public override void DisableFire()
    {
        base.DisableFire();
    }
    public override void ActiveFire()
    {
        base.ActiveFire();
    }
    private void OnDrawGizmos()
    {
        if (!IsDrawGizmos) return;
        Gizmos.color = ColorGizmos;
        Gizmos.DrawLine(pivotColliderPlayer.position, pivotColliderPlayer.position + pivotColliderPlayer.forward * distanceCollider);
    }
}
