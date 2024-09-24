using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEyeAttack : AIEyeBase
{
        public DataViewAttack DataViewAttack = new DataViewAttack();

        
        public override void LoadComponent()
        {
            base.LoadComponent();
        }
    

        public override void UpdateScan()
        {
            base.UpdateScan();
            if (ViewEnemy != null)
                DataViewAttack.IsInSight(ViewEnemy.transform);

        }
            
        

    

        

}
