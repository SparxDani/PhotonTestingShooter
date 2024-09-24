using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandWeaponsThunder : MonoBehaviour
{
    public Vector3 Offset;
    public float speed;
    public WeaponsShoot _weapons;
    Coroutine active;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Update()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, Time.deltaTime * speed);
    }
    public virtual void ActiveHandThunder()
    {
        
        Vector3 value = new Vector3(Mathf.Abs(Random.insideUnitSphere.x )* Offset.x,
                                    Mathf.Abs(Random.insideUnitSphere.y) * Offset.y,
                                    Mathf.Abs(Random.insideUnitSphere.z) * Offset.z);

        transform.localPosition = value;
       
    }
    
}
