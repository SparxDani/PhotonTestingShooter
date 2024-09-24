using Photon.Pun;
using UnityEngine;
public class InfoPlayer : MonoBehaviour
{
    public bool SinglePlayer = false;
    public Health health;
    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<Health>();
        if (SinglePlayer)
        {
            Destroy(GetComponent<PlayerSetup>());
            Destroy(GetComponent<PhotonView>());
            Destroy(GetComponent<PhotonAnimatorView>());
            Destroy(GetComponent<PhotonTransformView>());
        }
    }

    //[PunRPC]
    //void RPC_Chat(string message)
    //{
    //    Debug.Log("RPC_Chat> " + message);
    //    ChatManager.instance.AddTextChat(message);
    //}
}
