using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Realtime;

public class RoomListItem : MonoBehaviour
{
    [SerializeField] private TMP_Text roomName;
    public RoomInfo info;


    public void SetUp(RoomInfo roomInfo)
    {
        info = roomInfo;
        roomName.text = info.Name;
    }

    public void OnClick()
    {
        Launcher.instance.JoinRoom(info);

    }
}
