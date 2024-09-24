using Photon.Pun;
using Photon.Realtime;
using StarterAssets;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SandController : MonoBehaviourPunCallbacks, IInRoomCallbacks
{
    #region Syngleton
    static SandController _instance;

    static public bool isActive
    {
        get
        {
            return _instance != null;
        }
    }

    static public SandController instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = UnityEngine.Object.FindObjectOfType(typeof(SandController)) as SandController;

                if (_instance == null)
                {
                    GameObject go = new GameObject("SandController");
                    DontDestroyOnLoad(go);
                    _instance = go.AddComponent<SandController>();
                }
            }
            return _instance;
        }
    }
    #endregion
    #region UI
    public TextMeshProUGUI UINickName;

    public GameObject CanvasOption;
    public GameObject CanvasChat;
    public Image healthUI;
    public GameObject PlayerGame;
    public IndicatorScreen IndicatorDamageScreen;
    #endregion
    #region Room Info
    public List<Transform> spawnPointRoom = new List<Transform>();
    public List<Transform> spawnPointZombie = new List<Transform>();
    #endregion
    #region Player Info
    int myNumberInRoom;

    #endregion
    #region Gizmos
    [Range(0.2f, 1)]
    public float radiusSphereGizmos;
    #endregion
    #region Input
    #endregion
    public int CountZombie;
    void Awake()
    {

    }
    void Start()
    {
        CanvasChat.SetActive(false);
        CanvasOption.SetActive(false);
    }
    public override void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
        SceneManager.sceneLoaded += OnSceneFinishedLoading;
    }

    private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        UINickName.text = "NickName: " + PhotonNetwork.NickName;
        PlayerGame = FactoryBuilder.instance.BuilderPlayer(PlayerInfo.instance.Selectedcharacter, PhotonNetwork.NickName, SpawnerPlayer());



        if (PlayerGame.GetComponent<PhotonView>().IsMine)
        {

            HealthPlayer _HealthPlayerSetup = PlayerGame.GetComponent<HealthPlayer>();

            if (_HealthPlayerSetup != null)
            {
                _HealthPlayerSetup.HealthBarLocal = healthUI;
                _HealthPlayerSetup.IndicatorDamageScreen = IndicatorDamageScreen;
            }

        }

        print("OnSceneFinishedLoading...");

        for (int i = 0; i < CountZombie; i++)
        {
            FactoryBuilder.instance.BuilderZombie("Zombie1", SpawnerZombie());

        }

    }

    public Transform SpawnerPlayer()
    {
        int indexspawnPoint = UnityEngine.Random.Range(0, spawnPointRoom.Count - 1);
        indexspawnPoint = indexspawnPoint % spawnPointRoom.Count;
        Transform spawnTransform = spawnPointRoom[indexspawnPoint];
        return spawnTransform;
    }
    public Transform SpawnerZombie()
    {
        int indexspawnPoint = UnityEngine.Random.Range(0, spawnPointZombie.Count - 1);
        indexspawnPoint = indexspawnPoint % spawnPointZombie.Count;
        Transform spawnTransform = spawnPointZombie[indexspawnPoint];
        return spawnTransform;
    }
    public override void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
        SceneManager.sceneLoaded -= OnSceneFinishedLoading;
    }
    public void OnSalirButtonClicked()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel(0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            if (!CanvasOption.activeSelf)
            {
                CanvasOption.SetActive(true);
                Time.timeScale = 0;
                PausePlayerGame();
            }
            else
            {
                ResumePlayerGame();
                CanvasOption.SetActive(false);
                Time.timeScale = 1;
            }

        }
        else
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (!CanvasChat.activeSelf)
            {
                CanvasChat.SetActive(true);
                PausePlayerGame();
            }
            else
            {
                CanvasChat.SetActive(false);
                ResumePlayerGame();

            }

        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            LogMessege.instance.OpenWindow();
            PausePlayerGame();
        }

    }

    public void PausePlayerGame()
    {
        InputSystemManager.instance._StarterAssetsInputs.cursorInputForLook = false;
        InputSystemManager.instance._StarterAssetsInputs.cursorLocked = false;
        PlayerGame.GetComponent<ThirdPersonController>().enabled = false;
        PlayerGame.GetComponent<WeaponManager>().enabled = false;
        //PlayerGame.GetComponent<Animation>().Stop();

    }
    public void ResumePlayerGame()
    {
        InputSystemManager.instance._StarterAssetsInputs.cursorInputForLook = true;
        InputSystemManager.instance._StarterAssetsInputs.cursorLocked = true;
        PlayerGame.GetComponent<ThirdPersonController>().enabled = true;
        PlayerGame.GetComponent<WeaponManager>().enabled = true;
        //PlayerGame.GetComponent<Animation>().Play();
    }

    private void OnDrawGizmos()
    {
        foreach (var item in spawnPointRoom)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(item.position, radiusSphereGizmos);
        }

        foreach (var item in spawnPointZombie)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(item.position, radiusSphereGizmos);
        }
    }
}
