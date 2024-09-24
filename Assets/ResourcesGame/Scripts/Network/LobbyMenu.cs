using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
public class LobbyMenu : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _playerName;
    public RoomListingsMenu _RoomListingsMenu;

    private void OnEnable()
    {
        _playerName.text = "Nick Name:"+PhotonNetwork.NickName;
    }
}
