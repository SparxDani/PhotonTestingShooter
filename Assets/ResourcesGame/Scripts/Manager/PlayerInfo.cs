using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public enum SelectCharacter { Candy,CFB,Soldier}
public class PlayerInfo : MonoBehaviour
{
	static PlayerInfo _instance;

	static public bool isActive {
		get {
			return _instance != null;
		}
	}

	static public PlayerInfo instance {
		get {
			if (_instance == null)
			{
				_instance = Object.FindObjectOfType(typeof(PlayerInfo)) as PlayerInfo;

				if (_instance == null)
				{
					GameObject go = new GameObject("PlayerInfo");
					DontDestroyOnLoad(go);
					_instance = go.AddComponent<PlayerInfo>();
				}
			}
			return _instance;
		}
	}

	static string characterAvatarSelected = "PlayerCharacter";
	public string Selectedcharacter { get { return characterAvatarSelected; } set { characterAvatarSelected = value; } }
	
}
