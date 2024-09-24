using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using StarterAssets;
public class InputSystemManager : MonoBehaviour
{
    #region Sigleton
    static InputSystemManager _instance;

    static public bool isActive
    {
        get
        {
            return _instance != null;
        }
    }
    static public InputSystemManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = UnityEngine.Object.FindObjectOfType(typeof(InputSystemManager)) as InputSystemManager;

                if (_instance == null)
                {
                    GameObject go = new GameObject("InputSystemManager");
                    DontDestroyOnLoad(go);
                    _instance = go.AddComponent<InputSystemManager>();
                }
            }
            return _instance;
        }
    }
    #endregion

    [SerializeField] private PlayerInput playerInput = null;
    [SerializeField] private StarterAssetsInputs _starterAssetsInputs = null;
    public PlayerInput PlayerInput => playerInput;
    public StarterAssetsInputs _StarterAssetsInputs => _starterAssetsInputs;
    void OnEnable()
    {
        playerInput = GetComponent<PlayerInput>();
        _starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        PlayerInput.camera = Camera.main;
    }

}
