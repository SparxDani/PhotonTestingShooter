using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEyeShoot : AIEyeBase
{
        public Transform Aim;
        protected Vector3 StoreAimPosition;
        public DataViewFire DataViewFire = new DataViewFire();
        // Start is called before the first frame update
        
        public override void LoadComponent()
        {
            
            base.LoadComponent();
            DataViewFire.Owner = health;
            if (Aim != null)
                StoreAimPosition = Aim.localPosition;
        }
        //private void Update()
        //{
        //    this.UpdateScan();
        //}
        public override void UpdateScan()
        {
            base.UpdateScan();
            if(ViewEnemy!=null)
                DataViewFire.IsInSight(ViewEnemy.AimOffset);
            else
            if (ViewEnemy == null && DataViewFire.Shoot)
                {
                    DataViewFire.Shoot = false;
                }
            CalculateAim();
        }
        //public override void Scan()
        //{
        //    base.Scan();

        //    ActiveFire();

        //    CalculateAim();

        //}
        public virtual void ActiveFire()
        {
            if (ViewEnemy != null)
            {

                if (DataViewFire.IsInSight(ViewEnemy.transform))
                {
                    DataViewFire.Shoot = true;
                    
                }
                else
                    DataViewFire.Shoot = false;
            }
            else
            {
                DataViewFire.Shoot = false;
                ViewEnemy = null;
                Memory = Vector3.zero;
            }

        }
        public void CalculateAim()
        {
            if (ViewEnemy != null)
            {
                if (!ViewEnemy.IsCantView)
                {
                    ViewEnemy = null;
                    if (Aim != null)
                        Aim.localPosition = StoreAimPosition;
                }
                else
                if (ViewEnemy.IsDead)
                {
                    ViewEnemy = null;
                    if (Aim != null)
                        Aim.localPosition = StoreAimPosition;
            }
                else
                if (DataViewFire.IsInSight(ViewEnemy.transform))
                {
                    if (Aim != null)
                    {
                        Aim.position = Vector3.Lerp(Aim.position, ViewEnemy.AimOffset.position, Time.deltaTime * 120f);
                    }
                       
                }
            }
        }
        //private void ExtractViewEnemy(ref float min_dist, GameObject obj)
        //{
        //    Health _health = obj.GetComponent<Health>();

        //    if (_health != null && !_health.IsDead &&_health.IsCantView)
        //    {

        //        if(!IsAllies(_health))
        //        {
        //            float dist = (transform.position - obj.transform.position).magnitude;
        //            if (min_dist > dist)
        //            {
        //                ViewEnemy = _health;
        //                min_dist = dist;
        //                Memory = ViewEnemy.position;
        //            }
        //        }
        //        else
        //        {
        //            ViewAllie = _health;
        //            return;
        //        }
           

        //    }
        
        //}
        
}
