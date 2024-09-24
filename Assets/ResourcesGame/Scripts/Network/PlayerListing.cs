using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;
public class PlayerListing : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _playerName;
    [SerializeField]
    private TextMeshProUGUI _Master;


    private Player _player;

    public bool IsReady = false;

    public Player Player { get { return _player; } }

   
    private void Start()
    {
        
        
    }
    public void SetPlayerInfo(Player info)
    {
        _playerName.text = info.NickName;
        _player = info;

        _Master.gameObject.SetActive(info.IsMasterClient);
        _Master.text = "Master";

    }
    public void SetReadyInfo()
    {
        if (IsReady)
        {
            _Master.gameObject.SetActive(IsReady);
            _Master.text = "Listo";
        }
        else
        {
            _Master.gameObject.SetActive(IsReady);
            _Master.text = "Esperando";
        }

        

    }

}
