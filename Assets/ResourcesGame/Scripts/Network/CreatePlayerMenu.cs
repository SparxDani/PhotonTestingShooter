using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;
public class CreatePlayerMenu : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI _InputplayerName;
    public void SetNickName()
    {
        PhotonNetwork.NickName = _InputplayerName.text;
        UIManager.instance.ActiveComponent(ComponentUI.ListRoom);
        this.gameObject.SetActive(false);
    }
}
