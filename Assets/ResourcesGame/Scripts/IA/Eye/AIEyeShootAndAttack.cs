using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AIEyeShootAndAttack : AIEyeShoot
{
       
        public DataViewAttack DataViewAttack = new DataViewAttack();

        // Start is called before the first frame update
        void Awake()
        {
                this.LoadComponent();
        }
        public override void LoadComponent()
        {
            base.LoadComponent();
            DataViewAttack.Owner = health;
        }

        //private void Update()
        //{
        //    this.UpdateScan();
        //}
        public override void UpdateScan()
        {
            base.UpdateScan();
            ActiveFire();
        }
        //public override void Scan()
        //{
        //    base.Scan();
        //    ActiveFire();
        //}
        public override void ActiveFire()
        {
            base.ActiveFire();
            if (ViewEnemy != null)
            {
                //Memory = ViewEnemy.position;

                if (DataViewAttack.IsInSight(ViewEnemy.transform))
                {
                    DataViewAttack.Attack = true;
                }
                else
                    DataViewAttack.Attack = false;
            }
            else
            {
                DataViewFire.Shoot = false;
                DataViewAttack.Attack = false;
                ViewEnemy = null;
                Memory = Vector3.zero;
            }

        }
        private void OnValidate()
        {
            mainDataView.CreateMesh();
            DataViewAttack.CreateMesh();
            DataViewFire.CreateMesh();
        }
        private void OnDrawGizmos()
        {
            mainDataView.OnDrawGizmos();
            DataViewAttack.OnDrawGizmos();
            DataViewFire.OnDrawGizmos();
            //UnityEditor.Handles.Label(transform.position,"distance: "+DistanceEnemy);
            //UnityEditor.Handles.DrawWireArc(transform.position,Vector3.up,transform.forward,360,8);

        }
}
