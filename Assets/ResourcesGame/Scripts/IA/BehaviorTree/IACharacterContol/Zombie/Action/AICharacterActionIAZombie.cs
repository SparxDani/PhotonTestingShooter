using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICharacterActionIAZombie : AICharacterActionLand
{
    public int damage = 10;
    public LayerMask MaskAttack;
    int indexAttack = 0;
    bool[] bufferAttackIndex = new bool[5];

    //AICharacterVehicleZergIAZerling _VehicleEnemyLandZerling;
    // Start is called before the first frame update
    void Start()
    {
        this.LoadComponent();
        ResetRangeIndexAttack();
        
        ActiveFire = true;
    }
    void ResetRangeIndexAttack()
    {
        indexAttack = 0;
        for (int i = 0; i < bufferAttackIndex.Length; i++)
        {
            int num = (int)UnityEngine.Random.Range(1, 100);
            bufferAttackIndex[i] = (num % 2 == 0);
        }
    }
    int SelectIndexAttack()
    {
        indexAttack++;
        indexAttack = indexAttack % bufferAttackIndex.Length;
        if (indexAttack== bufferAttackIndex.Length -1)
        {
            ResetRangeIndexAttack();
        }

        if (bufferAttackIndex[indexAttack])
            return 1;
        return 2;

    }
    public void ApplicateDamage()
    {
       // ActiveFire = false;
        if (AIEye.ViewEnemy == null) return;

        if (!IsInSightRay(AIEye.ViewEnemy)) return;

        //if (SoundCharacterIA is SoundCharacterIAZergling)
        //    ((SoundCharacterIAZergling)SoundCharacterIA).PlayAttack();

        if (!AIEye.ViewEnemy.IsDead)
            AIEye.ViewEnemy.Damage(WeaponType.SCRATCH,null,damage);


    }

    public override void LoadComponent()
    {
        character = GetComponent<ThirdPersonCharacterAnimatorZombie>();
        LoadFrameRate();
        base.LoadComponent();
    }
    public override void Attack()
    {
       // if (!ActiveFire) return;

        ((ThirdPersonCharacterAnimatorZombie)character).Attack();
        
    }
    bool IsInSightRay(Health enemy)
    {
        if (AIEye is AIEyeAttack)
            return ((AIEyeAttack)AIEye).DataViewAttack.IsInSight(enemy.transform);

        return false;
    }
    public void ActiveAttack()
    {
        ActiveFire = true;
    }
    
}
