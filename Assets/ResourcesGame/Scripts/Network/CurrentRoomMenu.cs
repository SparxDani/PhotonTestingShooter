using UnityEngine;
using UnityEngine.UI ;
using Photon.Pun;
using TMPro;
public class CurrentRoomMenu : MonoBehaviourPunCallbacks
{
    
    public TextMeshProUGUI _roomName;

    public PlayerListingsMenu _PlayerListingsMenu;

    public GameObject buttonComenzar;
    public GameObject buttonListo;

    public Image _backgroundColor;
    public Color _Master;
    public Color _Local;
    // Start is called before the first frame update
    void Start()
    {
       
    }
    private void OnEnable()
    {
        
        if (PhotonNetwork.IsMasterClient)
        {
            buttonComenzar.SetActive(true);
            buttonListo.SetActive(false);
            _backgroundColor.color = _Master;
        }
        else
        {
            buttonComenzar.SetActive(false);
            buttonListo.SetActive(true);
            _backgroundColor.color = _Local;
        }
    }
    public void OnClick_Salir()
    {
        
        PhotonNetwork.LeaveRoom();

        UIManager.instance.ActiveComponent(ComponentUI.ListRoom);
        
    }
    

}
