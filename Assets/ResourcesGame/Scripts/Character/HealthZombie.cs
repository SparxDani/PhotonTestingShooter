using BehaviorDesigner.Runtime;
using Photon.Pun;
using UnityEngine;
using UnityEngine.AI;
public class HealthZombie : HealthIA
{
    Animator animator;
    NavMeshAgent agent;
    //Collider collider;
    AIEyeZombie _AIEyeZombie;
    AICharacterActionIAZombie _AICharacterActionIAZombie;
    AICharacterVehicleIAZombie _AICharacterVehicleIAZombie;
    ThirdPersonCharacterAnimatorZombie character;

    PhotonView _PhotonView;
    // Start is called before the first frame update
    void Start()
    {
        LoadComponent();
    }
    public override void LoadComponent()
    {
        base.LoadComponent();
        animator = GetComponent<Animator>();

        agent = GetComponent<NavMeshAgent>();

        _AIEyeZombie = GetComponent<AIEyeZombie>();

        character = GetComponent<ThirdPersonCharacterAnimatorZombie>();

        _AICharacterActionIAZombie = GetComponent<AICharacterActionIAZombie>();
        _AICharacterVehicleIAZombie = GetComponent<AICharacterVehicleIAZombie>();


        _PhotonView = GetComponent<PhotonView>();

        if (!_PhotonView.IsMine)
        {
            Destroy(agent);
            Destroy(_AIEyeZombie);
            Destroy(_AICharacterActionIAZombie);
            Destroy(_AICharacterVehicleIAZombie);
            Destroy(GetComponent<BehaviorTree>());
        }
    }

    void Die()
    {
        health = 0;

        if (_AICharacterActionIAZombie == null)
        {
            _AICharacterActionIAZombie = GetComponent<AICharacterActionIAZombie>();
        }

        if (_AICharacterActionIAZombie != null)
            _AICharacterActionIAZombie.enabled = false;

        if (_AICharacterVehicleIAZombie == null)
        {
            _AICharacterVehicleIAZombie = GetComponent<AICharacterVehicleIAZombie>();
        }

        if (_AICharacterVehicleIAZombie != null)
            _AICharacterVehicleIAZombie.enabled = false;


        if (_AIEyeZombie == null)
        {
            _AIEyeZombie = GetComponent<AIEyeZombie>();
        }

        if (_AIEyeZombie != null)
        {

            _AIEyeZombie.ViewEnemy = null;
            _AIEyeZombie.enabled = false;
        }



        //if (collider == null)
        //{
        //    collider = GetComponent<CapsuleCollider>();
        //}
        //if (collider != null)
        //    collider.enabled = false;




        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }

        if (agent != null)
            agent.isStopped = true;



        HealthBarClone.fillAmount = 0;

        animator.SetBool("Die", true);

        if (PV != null)
            PV.RPC("RPC_Die", RpcTarget.All);

        Invoke("Active", 10);
    }
    void Active()
    {


        if (_AICharacterActionIAZombie == null)
        {
            _AICharacterActionIAZombie = GetComponent<AICharacterActionIAZombie>();
        }

        if (_AICharacterActionIAZombie != null)
            _AICharacterActionIAZombie.enabled = true;

        if (_AICharacterVehicleIAZombie == null)
        {
            _AICharacterVehicleIAZombie = GetComponent<AICharacterVehicleIAZombie>();
        }

        if (_AICharacterVehicleIAZombie != null)
            _AICharacterVehicleIAZombie.enabled = true;


        if (_AIEyeZombie == null)
        {
            _AIEyeZombie = GetComponent<AIEyeZombie>();
        }

        if (_AIEyeZombie != null)
        {

            _AIEyeZombie.ViewEnemy = null;
            _AIEyeZombie.enabled = true;
        }



        //if (collider == null)
        //{
        //    collider = GetComponent<CapsuleCollider>();
        //}
        //if (collider != null)
        //    collider.enabled = true;



        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }

        if (agent != null)
            agent.isStopped = false;








        animator.SetBool("Die", false);

        health = healthMax;

        UpdateHealthBarLocal();

        Transform t = SandController.instance.SpawnerPlayer();

        this.transform.position = t.position;
        this.transform.rotation = t.rotation;

        if (PV != null)
            PV.RPC("RPC_Active", RpcTarget.All, t.position, t.rotation);

    }

    public override void Damage(WeaponType type, InfoPlayer enemy, int damage)
    {

        if (PV != null)
        {

            if (PV.IsMine)
            {
                character.Hit();
                base.Damage(type, enemy, damage);
                PV.RPC("RPC_ApplyDamage", RpcTarget.All, damage);
                if (IsDead)
                {
                    Die();
                    PV.RPC("RPC_Die", RpcTarget.All);
                }

                this.UpdateHealthBarLocal();
            }





        }
    }
    public override void UpdateHealthBarClone()
    {
        if (HealthBarClone != null)
        {
            float h = ((float)((float)health / (float)healthMax));
            HealthBarClone.fillAmount = h;
        }


    }
    #region RPC

    [PunRPC]
    void RPC_ApplyDamage(int damage)
    {

        character.Hit();
        if ((health - damage) > 0)
            health -= damage;
        else
            health = 0;
        UpdateHealthBarClone();

    }

    [PunRPC]
    void RPC_Die()
    {
        health = 0;

        if (_AICharacterActionIAZombie == null)
        {
            _AICharacterActionIAZombie = GetComponent<AICharacterActionIAZombie>();
        }

        if (_AICharacterActionIAZombie != null)
            _AICharacterActionIAZombie.enabled = false;

        if (_AICharacterVehicleIAZombie == null)
        {
            _AICharacterVehicleIAZombie = GetComponent<AICharacterVehicleIAZombie>();
        }

        if (_AICharacterVehicleIAZombie != null)
            _AICharacterVehicleIAZombie.enabled = false;


        if (_AIEyeZombie == null)
        {
            _AIEyeZombie = GetComponent<AIEyeZombie>();
        }

        if (_AIEyeZombie != null)
        {

            _AIEyeZombie.ViewEnemy = null;
            _AIEyeZombie.enabled = false;
        }



        //if (GetComponent<Collider>() == null)
        //{
        //    collider = GetComponent<CapsuleCollider>();
        //}




        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }

        if (agent != null)
            agent.isStopped = true;

        ////if (GetComponent<Collider>() != null)
        ////    GetComponent<Collider>().enabled = false;


        HealthBarClone.fillAmount = 0;

        animator.SetBool("Die", true);
    }

    [PunRPC]
    void RPC_Active(Vector3 position, Quaternion rotation)
    {


        if (_AICharacterActionIAZombie == null)
        {
            _AICharacterActionIAZombie = GetComponent<AICharacterActionIAZombie>();
        }

        if (_AICharacterActionIAZombie != null)
            _AICharacterActionIAZombie.enabled = true;

        if (_AICharacterVehicleIAZombie == null)
        {
            _AICharacterVehicleIAZombie = GetComponent<AICharacterVehicleIAZombie>();
        }

        if (_AICharacterVehicleIAZombie != null)
            _AICharacterVehicleIAZombie.enabled = true;


        if (_AIEyeZombie == null)
        {
            _AIEyeZombie = GetComponent<AIEyeZombie>();
        }

        if (_AIEyeZombie != null)
        {

            _AIEyeZombie.ViewEnemy = null;
            _AIEyeZombie.enabled = true;
        }



        //if (GetComponent<Collider>() == null)
        //{
        //    collider = GetComponent<CapsuleCollider>();
        //}




        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }

        if (agent != null)
            agent.isStopped = false;

        if (GetComponent<Collider>() != null)
            GetComponent<Collider>().enabled = true;


        animator.SetBool("Die", false);

        health = healthMax;

        UpdateHealthBarLocal();

        this.transform.position = position;

        this.transform.rotation = rotation;
    }

    #endregion

}
