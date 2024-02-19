using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Photon.Pun;

public class PlayerManager : MonoBehaviour
{
    private PhotonView _photonView;   
    void Start()
    {
        _photonView = GetComponent<PhotonView>();
        if (_photonView.IsMine)
        {
            CreatController();
        }
    }
    
    private void CreatController()
    {
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"), Vector3.zero, Quaternion.identity);
    }

    void Update()
    {
        
    }
}
