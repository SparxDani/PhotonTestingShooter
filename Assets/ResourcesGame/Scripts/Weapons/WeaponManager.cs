using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
public enum TypePoolBulletImpactEffect
{
    BulletImpactFleshBigEffect,
    BulletImpactFleshSmallEffect,
    BulletImpactMetalEffect,
    BulletImpactSandEffect,
    BulletImpactStoneEffect,
    BulletImpactWoodEffect,
    BulletImpactAcidEffect,
    BulletImpactExplotionEffect,
    None
}
public class WeaponManager : MonoBehaviour
{
    public List<WeaponHand> listWeapons = new List<WeaponHand>();
    public WeaponHand currentWeapont;
    public PhotonView PV { get; set; }
    public float speedAim;
    IKHuman _IKHuman;
    public WeaponType type;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        _IKHuman = GetComponent<IKHuman>();
        animator = GetComponent<Animator>();

        foreach (var item in listWeapons)
        {
            item.gameObject.SetActive(false);
        }

        ActiveWeaponsClone(type);

    }
    public void ResetWeaponManagerClone()
    {
        ActiveWeaponsClone(WeaponType.ASSAULT_RIFLE);
    }
    public void ResetWeaponManagerMaster()
    {
        ActiveWeaponsMaster(WeaponType.ASSAULT_RIFLE);
    }

    void Update()
    {

        if (PV != null && !PV.IsMine) return;

        #region SelectWeapons
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ActiveWeaponsMaster(WeaponType.ASSAULT_RIFLE);
        }
        else
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ActiveWeaponsMaster(WeaponType.SHOTGUN);
        }
        else
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ActiveWeaponsMaster(WeaponType.MINIGUN);
        }
        else
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ActiveWeaponsMaster(WeaponType.AXE1);
        }
        else
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            ActiveWeaponsMaster(WeaponType.AXE2);
        }
        #endregion

        #region StateHandWeapons

        if (currentWeapont is WeaponHandShoot)
        {

            if (Input.GetMouseButton(1) || !InputSystemManager.instance._StarterAssetsInputs.sprint)
            {
                if (((WeaponHandShoot)currentWeapont).state != StateHandWepons.Aim)
                {
                    ((WeaponHandShoot)currentWeapont).ActiveFire();
                    ((WeaponHandShoot)currentWeapont).state = StateHandWepons.Aim;
                    if (PV != null && PV.IsMine)
                        PV.RPC("RPC_ActiveFire", RpcTarget.All);
                }

            }
            else
            {
                if (((WeaponHandShoot)currentWeapont).state == StateHandWepons.Aim)
                {
                    ((WeaponHandShoot)currentWeapont).DisableFire();
                    if (((WeaponHandShoot)currentWeapont).state != StateHandWepons.Collider)
                        ((WeaponHandShoot)currentWeapont).state = StateHandWepons.Idle;
                    if (PV != null && PV.IsMine)
                        PV.RPC("RPC_DesactiveFire", RpcTarget.All);
                }


            }

            switch (((WeaponHandShoot)currentWeapont).ThisWeapons.weaponType)
            {
                case WeaponType.PISTOL:

                    break;
                case WeaponType.SHOTGUN:
                    if (Input.GetMouseButtonDown(0))
                    {
                        Fire();
                    }

                    break;
                case WeaponType.ASSAULT_RIFLE:
                    if (Input.GetMouseButton(0))
                    {
                        Fire();
                    }
                    else
                    {
                        DisableFire();
                    }
                    break;
                case WeaponType.MINIGUN:
                    if (Input.GetMouseButton(0))
                    {
                        Fire();
                    }
                    else
                    {
                        DisableFire();
                    }
                    break;
                case WeaponType.SNIPER_RIFLE:
                    break;
                case WeaponType.LAUNCH:
                    break;
                case WeaponType.SCRATCH:
                    break;
                case WeaponType.NONE:
                    break;
                default:
                    break;
            }

        }

        #endregion
        else
        if (currentWeapont is WeaponHandWhite)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Attack();
            }

        }

    }
    void Fire()
    {
        ((WeaponHandShoot)currentWeapont).Fire();

        if (PV != null && PV.IsMine)
            PV.RPC("RPC_Fire", RpcTarget.All, ((WeaponHandShoot)currentWeapont).ThisWeapons.PointShoot.position, ((WeaponHandShoot)currentWeapont).ThisWeapons.PointShoot.forward);
    }
    void Attack()
    {
        if (!animator.GetBool("Attack"))
        {
            animator.SetBool("Attack", true);

            if (PV != null && PV.IsMine)
                PV.RPC("RPC_Attack", RpcTarget.All);

        }

    }
    void ActiveCollider()
    {
        ((WeaponHandWhite)currentWeapont).weaponAttack.ActiveCollider();
    }
    void DesactiveCollider()
    {
        ((WeaponHandWhite)currentWeapont).weaponAttack.DesactiveCollider();
    }
    void DisableFire()
    {
        ((WeaponHandShoot)currentWeapont).DisableFire();

        if (PV != null && PV.IsMine)
            PV.RPC("RPC_Release", RpcTarget.All);

    }
    void ActiveWeaponsMaster(WeaponType ty)
    {
        type = ty;



        if (type == WeaponType.AXE1)
        {
            animator.SetInteger("Axe", 1);

            ChangeHandWeaponMaster(type);
        }
        else
        if (type == WeaponType.AXE2)
        {
            animator.SetInteger("Axe", 2);

            ChangeHandWeaponMaster(type);
        }
        else
        {
            animator.SetInteger("Axe", 0);

            foreach (var item in listWeapons)
            {
                if (item is WeaponHandShoot)
                {
                    if (type == ((WeaponHandShoot)item).ThisWeapons.weaponType)
                    {
                        ((WeaponHandShoot)item).gameObject.SetActive(true);

                        currentWeapont = ((WeaponHandShoot)item);

                        _IKHuman.IKActive = true;

                        _IKHuman.LeftHand = ((WeaponHandShoot)item).HandLeft;
                        _IKHuman.RightHand = ((WeaponHandShoot)item).HandRight;

                        _IKHuman.LeftElbow = ((WeaponHandShoot)item).LeftElbow;
                        _IKHuman.RightElbow = ((WeaponHandShoot)item).RightElbow;
                    }
                    else
                    {
                        ((WeaponHandShoot)item).ThisWeapons.DisableFire();
                        ((WeaponHandShoot)item).gameObject.SetActive(false);
                    }
                }
                else
                if (item is WeaponHandWhite)
                {
                    ((WeaponHandWhite)item).gameObject.SetActive(false);
                }

            }

            if (PV != null && PV.IsMine)
            {
                PV.RPC("RPC_ActiveWeapons", RpcTarget.All, type);
            }
        }
    }
    void ActiveWeaponsClone(WeaponType ty)
    {
        type = ty;

        if (type == WeaponType.AXE1)
        {
            animator.SetInteger("Axe", 1);

            ChangeHandWeaponClone(type);
        }
        else
        if (type == WeaponType.AXE2)
        {
            animator.SetInteger("Axe", 2);

            ChangeHandWeaponClone(type);
        }
        else
        {
            animator.SetInteger("Axe", 0);

            foreach (var item in listWeapons)
            {
                if (item is WeaponHandShoot)
                {
                    Debug.Log("type : " + type + " weaponType: " + ((WeaponHandShoot)item).ThisWeapons.weaponType);

                    if (type == ((WeaponHandShoot)item).ThisWeapons.weaponType)
                    {
                        ((WeaponHandShoot)item).gameObject.SetActive(true);

                        currentWeapont = ((WeaponHandShoot)item);

                        _IKHuman.IKActive = true;

                        _IKHuman.LeftHand = ((WeaponHandShoot)item).HandLeft;
                        _IKHuman.RightHand = ((WeaponHandShoot)item).HandRight;
                        //Debug.Log("type : " + type);
                    }
                    else
                    {
                        ((WeaponHandShoot)item).ThisWeapons.DisableFire();
                        ((WeaponHandShoot)item).gameObject.SetActive(false);
                    }
                }
                else
                if (item is WeaponHandWhite)
                {

                    ((WeaponHandWhite)item).gameObject.SetActive(false);
                }

            }


        }
    }
    private void ChangeHandWeaponMaster(WeaponType type)
    {

        foreach (var item in listWeapons)
        {
            if (item is WeaponHandShoot)
            {
                if (type == ((WeaponHandShoot)item).ThisWeapons.weaponType)
                {
                    ((WeaponHandShoot)item).gameObject.SetActive(true);

                    currentWeapont = ((WeaponHandShoot)item);
                    _IKHuman.IKActive = true;

                    _IKHuman.LeftHand = ((WeaponHandShoot)item).HandLeft;
                    _IKHuman.RightHand = ((WeaponHandShoot)item).HandRight;

                }
                else
                {
                    ((WeaponHandShoot)item).ThisWeapons.DisableFire();
                    ((WeaponHandShoot)item).gameObject.SetActive(false);
                }
            }
            else
            if (item is WeaponHandWhite)
            {
                _IKHuman.IKActive = false;
                _IKHuman.LeftHand = null;
                _IKHuman.RightHand = null;

                if (type == ((WeaponHandWhite)item).weaponAttack.weaponType)
                {
                    ((WeaponHandWhite)item).gameObject.SetActive(true);
                    currentWeapont = ((WeaponHandWhite)item);

                }
                else
                {
                    ((WeaponHandWhite)item).gameObject.SetActive(false);
                }

            }

        }

        if (PV != null && PV.IsMine)
        {
            PV.RPC("RPC_ChangeWeapon", RpcTarget.All, type);
        }
    }
    private void ChangeHandWeaponClone(WeaponType type)
    {

        foreach (var item in listWeapons)
        {
            if (item is WeaponHandShoot)
            {
                if (type == ((WeaponHandShoot)item).ThisWeapons.weaponType)
                {
                    ((WeaponHandShoot)item).gameObject.SetActive(true);
                    currentWeapont = ((WeaponHandShoot)item);
                    _IKHuman.IKActive = true;
                    _IKHuman.LeftHand = ((WeaponHandShoot)item).HandLeft;
                    _IKHuman.RightHand = ((WeaponHandShoot)item).HandRight;
                }
                else
                {
                    ((WeaponHandShoot)item).ThisWeapons.DisableFire();
                    ((WeaponHandShoot)item).gameObject.SetActive(false);
                }
            }
            else
            if (item is WeaponHandWhite)
            {
                _IKHuman.IKActive = false;
                _IKHuman.LeftHand = null;
                _IKHuman.RightHand = null;
                if (type == ((WeaponHandWhite)item).weaponAttack.weaponType)
                {
                    ((WeaponHandWhite)item).gameObject.SetActive(true);
                    currentWeapont = ((WeaponHandWhite)item);
                }
                else
                {
                    ((WeaponHandWhite)item).gameObject.SetActive(false);
                }

            }

        }


    }
    #region RPC
    [PunRPC]
    void RPC_Attack()
    {
        animator.SetBool("Attack", true);
    }
    [PunRPC]
    void RPC_Fire(Vector3 origin, Vector3 direction)
    {
        if (((WeaponHandShoot)currentWeapont).ThisWeapons is Rifle)
        {
            Rifle rifle = ((Rifle)((WeaponHandShoot)currentWeapont).ThisWeapons);
            rifle.RayCastClone(origin, direction);
        }
        else
        if (((WeaponHandShoot)currentWeapont).ThisWeapons is ShotGun)
        {
            ShotGun shotGun = ((ShotGun)((WeaponHandShoot)currentWeapont).ThisWeapons);
            shotGun.RayCastClone(origin, direction);
        }
        else
        if (((WeaponHandShoot)currentWeapont).ThisWeapons is MiniGuns)
        {
            MiniGuns miniGuns = ((MiniGuns)((WeaponHandShoot)currentWeapont).ThisWeapons);
            miniGuns.RayCastClone(origin, direction);
        }
    }
    [PunRPC]
    void RPC_Release()
    {
        ((WeaponHandShoot)currentWeapont).ThisWeapons.DisableFire();
    }

    [PunRPC]
    void RPC_ChangeWeapon(WeaponType ty)
    {
        ChangeHandWeaponClone(ty);
    }
    [PunRPC]
    void RPC_ActiveWeapons(WeaponType ty)
    {
        ActiveWeaponsClone(ty);
    }

    [PunRPC]
    void RPC_ActiveFire()
    {
        ((WeaponHandShoot)currentWeapont).ActiveFire();
        ((WeaponHandShoot)currentWeapont).state = StateHandWepons.Aim;
    }
    [PunRPC]
    void RPC_DesactiveFire()
    {
        ((WeaponHandShoot)currentWeapont).DisableFire();
        if (((WeaponHandShoot)currentWeapont).state != StateHandWepons.Collider)
            ((WeaponHandShoot)currentWeapont).state = StateHandWepons.Idle;
    }
    #endregion


}
