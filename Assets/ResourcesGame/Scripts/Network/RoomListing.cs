using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;
public class RoomListing : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _roomName;
    [SerializeField]
    private TextMeshProUGUI _roomCountPlayer;

    private RoomInfo _roomInfo;
    public RoomInfo RoomInfo { get { return _roomInfo; } }

    public void SetRoomInfo(RoomInfo info)
    {
        _roomName.text = info.Name;
        _roomCountPlayer.text = info.MaxPlayers.ToString();
        _roomInfo = info;
    }
    public void OnClick_Button()
    {
        
        if (_roomInfo != null)
        {
            PhotonNetwork.JoinRoom(_roomInfo.Name);
        }
            
    }
}
