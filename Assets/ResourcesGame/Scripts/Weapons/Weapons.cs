using UnityEngine;


public enum WeaponType { PISTOL, SHOTGUN, ASSAULT_RIFLE, MINIGUN, SNIPER_RIFLE, LAUNCH, SCRATCH, AXE1, AXE2, NONE };
[System.Serializable]
public class ParticleFlash
{

    public ParticleSystem particle;
    public int CountEmitter = 1;
    public ParticleFlash()
    { }
    public void Emite()
    {
        particle.Emit(CountEmitter);
    }
    public void Stop()
    {
        particle.Emit(0);
    }
}
public class Weapons : MonoBehaviour
{
    [Header("Gun Attributes")]
    public string weaponName;
    public WeaponType weaponType;

    [Header("Weapon Damage")]
    public int damage = 17;

    [Header("Onwer")]
    public InfoPlayer Onwer;


    protected bool _IsEnabled = false;



    [Header("Draw Gizmo")]
    public bool ShowGizmo = false;


    public bool IsEnabled;
    //{
    //    get
    //    {
    //        return _IsEnabled;
    //    }
    //    set
    //    {
    //        _IsEnabled = value;
    //    }
    //}
    public virtual void LoadComponent()
    {

    }
    public virtual bool IsNotIsThis(GameObject obj1, GameObject obj2)
    {
        return (obj1.GetInstanceID() != obj2.GetInstanceID());
    }

}
