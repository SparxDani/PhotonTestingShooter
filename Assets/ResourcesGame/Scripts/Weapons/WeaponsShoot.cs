using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



public class WeaponsShoot : Weapons
{


    [Header("Weapon Amount")]
    public int _cartridge = 0;
    public int _Maxcartridge = 0;
    public int _countbullet = 0;
    public int _MaxbulletTocartridge = 0;

    protected bool canShoot = true;
    protected bool isReloading = false;

    [Header("GUI Bullet")]
    public TextMeshProUGUI BulletText;

    [Header("GUI Cartridge")]
    public TextMeshProUGUI CartridgeText;

   
    [Header("Point Shoot")]
    public Transform PointShoot;

   
    [Header("Particle ")]
    public List<ParticleFlash> ParticleFlash = new List<ParticleFlash>();
    public ParticleBulletImpactEffect _ParticleBulletImpactEffect = new ParticleBulletImpactEffect();

    [Header("Hand Weapons Thunder")]
    public HandWeaponsThunder HandWeaponsThunder;
    
    public override void LoadComponent()
    {
        base.LoadComponent();
    }
    public virtual bool IfCantShoot { get => !(_countbullet <= 0); }
    public bool IfCanShoot()
    {
        return (_countbullet > 0);
    }
    public bool IfCanReload()
    {
        return (_cartridge > 0);
    }
    public virtual void EmmiteParticleFlash()
    {

    }

    public void UpdateAmount(int _cartridge, int _countbullet)
    {
        //this._cartridge= _cartridge;
        //this._countbullet = _countbullet;
        //this.cartridge = this._cartridge;
        //this.countbullet = this._countbullet;
        UpdateAmmoBulletText();
        UpdateAmmoCartridgeText();
    }

    public virtual void UpdateFire()
    {



    }
    public virtual void StopFire()
    {
        DisableFire();
        StopSoundShoot();
    }
    public virtual void RayCastClone(Vector3 pos, Vector3 dir)
    {

    }
    public virtual void ReleaseFire()
    {



    }
    public int GetWeaponDamage()
    {
        int extraDamage = 0;
        return extraDamage + damage;
    }
    public virtual void StartReload()
    {
        
    }
    public virtual void UpdateAmmoBulletText()
    {
        if (BulletText != null)
            BulletText.text = _countbullet.ToString();
    }
    public virtual void UpdateAmmoCartridgeText()
    {
        if (CartridgeText != null)
            CartridgeText.text = _cartridge.ToString();
    }
    public virtual void ActiveFire()
    {
        IsEnabled = true;
    }
    public virtual void Reload()
    {
        StartReload();
    }
    public virtual void DisableFire()
    {
        IsEnabled = false;
        //_MuzzleFlashWeamon.Stop();
    }
    
    
    public virtual void PlaySoundShoot()
    {


        //FMOD.Studio.PLAYBACK_STATE fmodstate;
        //_FireInstance.getPlaybackState(out fmodstate);
        //if (fmodstate == FMOD.Studio.PLAYBACK_STATE.STOPPED)
        //{
        //    _FireInstance.setVolume(VolumenSoundEffectFirel);
        //    _FireInstance.start();
        //}

    }
    public virtual void StopSoundShoot()
    {
        //FMOD.Studio.PLAYBACK_STATE fmodstate;
        //_FireInstance.getPlaybackState(out fmodstate);
        //if (fmodstate == FMOD.Studio.PLAYBACK_STATE.PLAYING)
        //_FireInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
    public virtual void UpdateEvent3D()
    {

        //FMODUnity.RuntimeManager.AttachInstanceToGameObject(_FireInstance, transform, rg);

    }
}
