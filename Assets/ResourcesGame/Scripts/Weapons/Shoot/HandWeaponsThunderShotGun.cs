using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandWeaponsThunderShotGun : HandWeaponsThunder
{
    Coroutine active;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Update()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, Time.deltaTime * speed);
    }
    public override void ActiveHandThunder()
    {
        base.ActiveHandThunder();
        _weapons.DisableFire();
        active=StartCoroutine(ActiveWeapons());
    }
    IEnumerator ActiveWeapons()
    {

        yield return new WaitForSeconds(2);
        _weapons.ActiveFire();
        StopCoroutine(active);
    }
}
