using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
public class Trampolin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ThirdPersonController ch = other.gameObject.GetComponent<ThirdPersonController>();
            if(ch!=null)
            {
                ch.PushJump(10);
            }
        }
    }
    
}
