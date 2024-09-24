using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
public class ChatManager : MonoBehaviour
{

	#region Syngleton
	static ChatManager _instance;

	static public bool isActive
	{
		get
		{
			return _instance != null;
		}
	}

	static public ChatManager instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = UnityEngine.Object.FindObjectOfType(typeof(ChatManager)) as ChatManager;

				if (_instance == null)
				{
					GameObject go = new GameObject("ChatManager");
					DontDestroyOnLoad(go);
					_instance = go.AddComponent<ChatManager>();
				}
			}
			return _instance;
		}
	}
	#endregion

	public TextMeshProUGUI UIChatText;
	public TextMeshProUGUI UIChatInput;
	public PhotonView PV { get; set; }
	// Start is called before the first frame update
	void Start()
    {
        
    }

	public void SendMessageChat()
	{
		ChatManager.instance.AddTextChat(UIChatInput.text);
		//Debug.Log("MessageSend> "+ UIChatInput.text);
		PV.RPC("RPC_Chat", RpcTarget.All, (PV.Controller.NickName+": "+ UIChatInput.text));
	}
	public void AddTextChat(string message)
    {
		UIChatText.text += message+"\n";
	}

}
