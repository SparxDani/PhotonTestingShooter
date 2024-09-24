using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICharacterActionLand : AICharacterAction
{
    public override void LoadComponent()
    {
        base.LoadComponent();
    }
   
    #region Attack
    public virtual void Attack()
    {
        
    }
    public virtual void FirePlay()
    {

    }
    public virtual void StopPlay()
    { }
    //public virtual void Reload()
    //{ }
    //public bool IsCanAttackEnemy()
    //{
    //    if (!Passive || 
    //        _AIEye.ViewEnemy != null|| 
    //        ActiveFire)
    //        return true;
    //    return false;
    //}
    #endregion
}
