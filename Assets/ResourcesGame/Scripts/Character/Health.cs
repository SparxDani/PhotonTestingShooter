using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum UnitSC
{
    Zombie,
    Marine,
    None
}
public enum StateKombat
{
    Idle,
    Attack,
    Fire,
    Hit,
    Dead,
    Stand,
    None
}
public enum TypeAgent { A, B, C, D, E }
public class Health : MonoBehaviour
{
    public int health;
    public int healthMax;
    public bool IsDead { get => (health <= 0); }
    public Transform AimOffset;
    public Health HurtingMe;

    public StateKombat StateKombat;
    public bool CantView;

    public UnitSC _Unity;
    public bool IsCantView { get => CantView; }
    [Header("Type Agent")]
    public TypeAgent typeAgent;
    [Header("Type List Agent Allies")]
    public List<TypeAgent> typeAgentAllies = new List<TypeAgent>();
    public PhotonView PV;
    public Image HealthBarLocal;
    public Image HealthBarClone;
    public bool Importal = false;
    Coroutine HurtingMeroutine;
    public virtual void LoadComponent()
    {
        health = healthMax;
        PV = GetComponent<PhotonView>();

    }
    // Start is called before the first frame update
    public virtual void ResetPlayer()
    {

    }
    public virtual void UpdateHealthBarClone()
    {
        if (HealthBarClone != null)
        {
            float h = ((float)((float)health / (float)healthMax));
            HealthBarClone.fillAmount = h;
        }
    }
    public void UpdateHealthBarLocal()
    {

        if (HealthBarLocal != null)
        {
            float h = ((float)((float)health / (float)healthMax));
            HealthBarLocal.fillAmount = h;

        }
    }
    public virtual void Damage(WeaponType type, InfoPlayer enemy, int damage)
    {
        if (Importal) return;

        if (!IsDead)
        {
            if ((health - damage) > 0)
                health -= damage;
            else
                health = 0;

            if (enemy != null)
                HurtingMeroutine = StartCoroutine(HurtingMeActive(enemy.health));
        }

    }

    IEnumerator HurtingMeActive(Health enemy)
    {
        HurtingMe = enemy;
        yield return new WaitForSeconds(3);
        HurtingMe = null;
        StopCoroutine(HurtingMeroutine);
    }
    public bool IsAllies(Health heatlhScan)
    {
        for (int j = 0; (heatlhScan != null && j < heatlhScan.typeAgentAllies.Count); j++)
        {
            if (typeAgentAllies[j] == heatlhScan.typeAgent)
            {
                return true;
            }
        }

        return false;
    }
}
