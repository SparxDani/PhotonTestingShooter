using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;
public class CreateRoomMenu : MonoBehaviour
{
    public TextMeshProUGUI _roomName;

    public TextMeshProUGUI _countPlayer;

    public GameObject ListRoom;

    int count=2;
    public int countMax;
    public void Increment()
    {
        count = Mathf.Clamp(count + 1, 2, countMax);
        _countPlayer.text = count.ToString();
    }
    public void Decremet()
    {
        count = Mathf.Clamp(count - 1, 2, countMax);
        _countPlayer.text = count.ToString();
    }

    //public void OnClickCreateRoom()
    //{
    //    if (!PhotonNetwork.IsConnected) return;
    //    RoomOptions options = new RoomOptions();
    //    options.MaxPlayers = (byte)int.Parse(_countPlayer.text);
    //    PhotonNetwork.JoinOrCreateRoom(_roomName.text, options, TypedLobby.Default);
    //}
    public void OnClickSalir()
    {
        if (!PhotonNetwork.IsConnected) return;
        _roomName.text = "";
        _countPlayer.text = "2";
        UIManager.instance.ActiveComponent(ComponentUI.ListRoom);
        this.gameObject.SetActive(false);
    }
    
    //public override void OnCreatedRoom()
    //{
       
    //    UIManager.instance.ActiveComponent(ComponentUI.CreateRoom);
    //    _roomName.text = "";
    //    _countPlayer.text = "2";
    //    this.gameObject.SetActive(false);


    //}
    //public override void OnCreateRoomFailed(short returnCode, string message)
    //{
    //    Debug.Log("OnCreateRoomFailed: " + message);
    //}
}
