using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
public class PlayerListingsMenu : MonoBehaviourPunCallbacks
{

    [SerializeField]
    private PlayerListing _playerListing;

    public List<PlayerListing> _listings = new List<PlayerListing>();
    public bool IsReady = false;

    [SerializeField]
    private TextMeshProUGUI _buttonReadyText;

    [SerializeField]
    private GameObject _buttonReady;

    private void Start()
    {
       
        _buttonReady.SetActive(!PhotonNetwork.IsMasterClient);
    }
    public override void OnEnable()
    {
        base.OnEnable();
        SetReadyUp(false);
    }
    public void GetCurrentRoomPlayers(Dictionary<int, Player> listPlayer)
    {
        if (!PhotonNetwork.IsConnected)
            return;
        if (PhotonNetwork.CurrentRoom == null || PhotonNetwork.CurrentRoom.Players == null)
            return;

        foreach (KeyValuePair<int, Player> playerinfo in PhotonNetwork.CurrentRoom.Players)
        {
            AddPlayerListing(playerinfo.Value);
        }
    }
    public void OnClick_ReadyUp()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            SetReadyUp(!IsReady);
            base.photonView.RPC("RPC_ChangeReadyState",RpcTarget.MasterClient,PhotonNetwork.LocalPlayer, IsReady);
        }
        
    }
   
    [PunRPC]
    private void RPC_ChangeReadyState(Player player, bool ready)
    {
        int index = _listings.FindIndex(x=>x.Player == player);
        if (index != -1)
        {
            _listings[index].IsReady = ready;
            _listings[index].SetReadyInfo();
        }
            
    }
    private void SetReadyUp(bool state)
    {
        IsReady = state;

        if (IsReady)
        {
            _buttonReadyText.text = "Listo";
        }
        else
        {
            _buttonReadyText.text = "Presione aquí";
        }
    }
    public void AddPlayerListing(Player newPlayer)
    {
        PlayerListing listing = Instantiate(_playerListing, transform);
        if (_listings != null)
        {

            listing.SetPlayerInfo(newPlayer);
            _listings.Add(listing);
        }

    }
    public void DestroyPlayer(Player otherPlayer)
    {
        int index = _listings.FindIndex(x => x.Player == otherPlayer);
        if (index != -1)
        {
            Destroy(_listings[index].gameObject);
            _listings.RemoveAt(index);
        }
    }
    public void DestroyPlayerListing()
    {
        foreach (var item in _listings)
        {
            Destroy(item.gameObject);
        }
        _listings.Clear();
    }
   
}
