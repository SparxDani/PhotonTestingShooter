using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum TypeItem { Box,Ball}
public class FactoryItem : MonoBehaviour
{
    public TypeItem type;
    float Rate = 5;
    float FrameRate=0;
    bool Active = false;
    public Transform spawnPoint;
    private void Update()
    {
        if(FrameRate>Rate)
        {
            FrameRate = 0;
            Active = true;
        }
        FrameRate += Time.deltaTime;
    }
    private void OnTriggerStay(Collider other)
    {
        //Debug.Log("OnTriggerStay 1");
        if (other.CompareTag("Player"))
        {
            if(Input.GetKeyDown(KeyCode.E) &&Active)
            {
                FactoryBuilder.instance.BuilderItem(type, spawnPoint);
                
            }

        }
    }
}
