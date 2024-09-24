using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.IO;

public enum TypePlayer {Avatar,Arena}
public enum TypeParticle { Ricochet, BulletHole }
public class FactoryBuilder : MonoBehaviour
{
	#region Syngleton
	static FactoryBuilder _instance;

	static public bool isActive {
		get {
			return _instance != null;
		}
	}

	static public FactoryBuilder instance {
		get {
			if (_instance == null)
			{
				_instance = UnityEngine.Object.FindObjectOfType(typeof(FactoryBuilder)) as FactoryBuilder;

				if (_instance == null)
				{
					GameObject go = new GameObject("FactoryBuilder");
					DontDestroyOnLoad(go);
					_instance = go.AddComponent<FactoryBuilder>();
				}
			}
			return _instance;
		}
	}
	#endregion
	
	void Start()
	{
        
    }

	public GameObject BuilderPlayer(string prefabnameSand, string nickname,Transform spawnTransform)
	{
		
		GameObject player=null;
		player = PhotonNetwork.Instantiate(Path.Combine("Prefabs/Characters", prefabnameSand), spawnTransform.position, spawnTransform.rotation, 0);
		if (player != null)
		{
			PlayerSetup _PlayerSetup = player.GetComponent<PlayerSetup>();
			player.GetComponent<PhotonView>().Controller.NickName= nickname;
		}
		return player;
	}
	public GameObject BuilderZombie(string prefabnameSand, Transform spawnTransform)
	{

		GameObject zombie = null;
		zombie = PhotonNetwork.Instantiate(Path.Combine("Prefabs/Zombie", prefabnameSand), spawnTransform.position, spawnTransform.rotation, 0);
		
		return zombie;
	}
	public GameObject BuilderItem(TypeItem type, Transform spawnTransform)
	{
        switch (type)
        {
            case TypeItem.Box:
				return PhotonNetwork.Instantiate(Path.Combine("Prefabs/Item", "Box"), spawnTransform.position, spawnTransform.rotation, 0);
            case TypeItem.Ball:
				return PhotonNetwork.Instantiate(Path.Combine("Prefabs/Item", "Ball"), spawnTransform.position, spawnTransform.rotation, 0);
            default:
                break;
        }
		return null;
	}
	public GameObject BuilderParticle(string  type, Vector3 pos, Vector3 nr)
	{
		
		return Instantiate(Resources.Load(Path.Combine("Prefabs/ParticleWeapons",type)) as GameObject,  pos, Quaternion.LookRotation(nr));

	}

}
