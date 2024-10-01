using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class PlayerPhotonController : MonoBehaviour
{
    PhotonView photonView;
    public GameObject _camera;


    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();

        if (photonView.IsMine == false)
        {
            CharacterController characterController = GetComponent<CharacterController>();
            CapsuleCollider capsuleCollider = this.gameObject.AddComponent<CapsuleCollider>();
            capsuleCollider.radius = 0.5f;
            capsuleCollider.height = 2;

 
            Destroy(characterController);
            Destroy(GetComponent<PlayerMovement>());

            Destroy(this);
        }
    }

    
}