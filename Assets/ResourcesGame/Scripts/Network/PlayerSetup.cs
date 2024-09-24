using Photon.Pun;
using StarterAssets;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PlayerSetup : MonoBehaviour
{
    public TextMeshProUGUI Localnickname;
    public Transform _canvas;
    public GameObject heah;
    public Image HealthBarClone;
    public PhotonView PV { get; set; }
    public Transform PivotCamera;
    public Transform Aim;
    public bool IsMine;
    IKHuman _IKHuman;
    // Start is called before the first frame update
    private void Start()
    {

        PV = GetComponent<PhotonView>();
        IsMine = PV.IsMine;
        if (!PV.IsMine)
        {

            heah.layer = LayerMask.NameToLayer("Default");

            //Destroy(PivotCamera);

            this.gameObject.tag = "PlayerClone";

            SetLocalNickName(PV.Controller.NickName);

            CharacterController cc = GetComponent<CharacterController>();
            CapsuleCollider collider = this.gameObject.AddComponent<CapsuleCollider>();

            collider.height = cc.height;
            collider.center = cc.center;
            collider.radius = cc.radius;

            Destroy(GetComponent<ThirdPersonController>());
            Destroy(GetComponent<CharacterController>());
            GetComponent<HealthPlayer>().HealthBarClone = HealthBarClone;

        }
        else
        {
            // este es el master
            this.gameObject.tag = "Player";
            _IKHuman = GetComponent<IKHuman>();
            GameObject Camera = GameObject.FindGameObjectWithTag("MainCamera");
            if (Camera != null)
            {
                Camera.transform.parent = PivotCamera;
                Camera.transform.localPosition = Vector3.zero;
                Camera.transform.localRotation = Quaternion.identity;

                Aim.localPosition = Vector3.forward * 2f;
                Aim.parent = Camera.transform;

                _IKHuman.target = Camera.transform.GetChild(0);
            }
            _canvas.gameObject.SetActive(false);

            ChatManager.instance.PV = PV;
            SandController.instance.PlayerGame = this.gameObject;

        }
    }
    private void Update()
    {
        if (!PV.IsMine)
        {
            _canvas.LookAt(Camera.main.transform.position, Vector3.up);
        }
    }

    public void SetLocalNickName(string name)
    {
        if (Localnickname != null)
            Localnickname.text = name;

    }
    #region RPC
    [PunRPC]
    void RPC_AimPosition(Vector3 position)
    {
        Aim.position = position;
    }
    #endregion


}
