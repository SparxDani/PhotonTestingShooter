using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Realtime;

public class UICreateRoom : MonoBehaviour
{
    public TextMeshProUGUI textRoomName;
    public TextMeshProUGUI textCountPlayer;
    int count;
    public int countMax;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Increment()
    {
        count = Mathf.Clamp(count + 1, 0, countMax);
        textCountPlayer.text = count.ToString();
    }
    public void Decremet()
    {
        count = Mathf.Clamp(count - 1, 0, countMax);
        textCountPlayer.text = count.ToString();
    }
   
}
