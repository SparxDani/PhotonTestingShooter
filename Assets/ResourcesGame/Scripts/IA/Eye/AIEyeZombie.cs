using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEyeZombie : AIEyeAttack
{
        // Start is called before the first frame update
        void Start()
        {
            this.LoadComponent();
        }
        public override void LoadComponent()
        {
            base.LoadComponent();
            
        }
        

        private void Update()
        {
            base.UpdateScan();
        }

        private void OnValidate()
        {
            mainDataView.CreateMesh();
            DataViewAttack.CreateMesh();
        }
        private void OnDrawGizmos()
        {
            mainDataView.OnDrawGizmos();
            DataViewAttack.OnDrawGizmos();
        }

}
