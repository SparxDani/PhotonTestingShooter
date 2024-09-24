using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletImpactEffect : MonoBehaviour
{
    public float time;
    public int Count_Emmiter;
    public TypePoolBulletImpactEffect type;
    public ParticleSystem[] particle;
    private void Start()
    {
        particle = GetComponentsInChildren<ParticleSystem>();
    }
    public void Emit()
    {
        foreach (var item in particle)
        {
            item.Emit(Count_Emmiter);
        }
    }
    public  void Active()
    {
        Invoke("Inactive", time);
       // base.Active();
    }

    void Inactive()
    {
        //base.Desactive();
        foreach (var item in particle)
        {
            item.Emit(0);
        }
    }




}
