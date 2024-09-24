using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    static UIManager _instance;
    static public bool isActive {
        get {
            return _instance != null;
        }
    }
    static public UIManager instance {
        get {
            if (_instance == null)
            {
                _instance = Object.FindObjectOfType(typeof(UIManager)) as UIManager;

                if (_instance == null)
                {
                    GameObject go = new GameObject("UIManager");
                    DontDestroyOnLoad(go);
                    _instance = go.AddComponent<UIManager>();
                }
            }
            return _instance;
        }
    }
    public List<UIComponent> listComponent = new List<UIComponent>();
    public UIComponent initComp;
    //----------------------------------------------------------------------------------
    private void Start()
    {
        ActiveComponent(initComp.gameObject);
    }
    //----------------------------------------------------------------------------------
    public void ActiveComponent(GameObject component)
    {
        UIComponent Uicomp = component.GetComponent<UIComponent>();
        foreach (var item in listComponent)
        {
            if (item.type == Uicomp.type && item.gameObject.GetInstanceID()== component.GetInstanceID())
            {
                component.SetActive(true);
            }
            else
                item.gameObject.SetActive(false);
        }
    }
    //----------------------------------------------------------------------------------
    public void DesactiveComponent(GameObject component)
    {
        UIComponent Uicomp = component.GetComponent<UIComponent>();
        foreach (var item in listComponent)
        {
            if (item.type == Uicomp.type && item.gameObject.GetInstanceID() == component.GetInstanceID())
            {
                component.SetActive(false);
                return;
            }
            
        }
    }
    //----------------------------------------------------------------------------------
    public void ActiveComponent(ComponentUI component)
    {
        foreach (var item in listComponent)
        {
            if (item.type == component)
            {
                item.gameObject.SetActive(true);
            }
            else
                item.gameObject.SetActive(false);
        }
    }
    //----------------------------------------------------------------------------------
    public void DesactiveComponent(ComponentUI component)
    {
        foreach (var item in listComponent)
        {
            if (item.type == component)
            {
                item.gameObject.SetActive(true);
            }
        }
    }
    //----------------------------------------------------------------------------------
    //----------------------------------------------------------------------------------
    //----------------------------------------------------------------------------------
}
