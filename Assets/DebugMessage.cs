using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugMessage : MonoBehaviour
{
    #region Syngleton
    static DebugMessage _instance;

    static public bool isActive
    {
        get
        {
            return _instance != null;
        }
    }

    static public DebugMessage instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = UnityEngine.Object.FindObjectOfType(typeof(DebugMessage)) as DebugMessage;

                if (_instance == null)
                {
                    GameObject go = new GameObject("DebugMessage");
                    DontDestroyOnLoad(go);
                    _instance = go.AddComponent<DebugMessage>();
                }
            }
            return _instance;
        }
    }
    #endregion


    [SerializeField]
    private TextMeshProUGUI _message;


    public void AddMessage(string message)
    {
        _message.text += message + "\n";
    }
}