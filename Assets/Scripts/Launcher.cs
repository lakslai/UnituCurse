    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using TMPro;
    using Photon.Pun;
    using System;
    using Photon.Realtime;

    public class Launcher : MonoBehaviourPunCallbacks
    {
        public static Launcher instance;

        [SerializeField] private TMP_InputField _roomInput;
        [SerializeField] private TMP_Text _errorText;
        [SerializeField] private TMP_Text _roomText;
        [SerializeField] private Transform _roomList;
        [SerializeField] private GameObject _roomButtonPrefab;
        [SerializeField] private Transform _playerList;
        [SerializeField] private GameObject _playerNamePrefab;
        [SerializeField] private GameObject _startGameButton;




    void Start()
        {
            instance = this;
            Debug.Log("Вы присоединяетесь к Мастер серверу!");
            PhotonNetwork.ConnectUsingSettings();
            MenuManager.instance.OpenMenu("loading");
        }

        public override void OnConnectedToMaster()
        {
            Debug.Log("Вы присоединились к Мастер серверу!");
            PhotonNetwork.JoinLobby();
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        public override void OnJoinedLobby()
        {
            Debug.Log("Вы присоединились к Лобби!");
            MenuManager.instance.OpenMenu("title");
            PhotonNetwork.NickName = "Player " + Mathf.FloorToInt(UnityEngine.Random.Range(0, 1000)).ToString("0000");
        }

        public void StartGame()
        {
            PhotonNetwork.LoadLevel(1);
        }

        public void CreateRoom()
        {
            if (string.IsNullOrEmpty(_roomInput.text))
            {
                return;
            }
            PhotonNetwork.CreateRoom(_roomInput.text);
            MenuManager.instance.OpenMenu("loading");
        }

        public override void OnJoinedRoom()
        {
            _roomText.text = PhotonNetwork.CurrentRoom.Name;
            MenuManager.instance.OpenMenu("room");

            Player[] players = PhotonNetwork.PlayerList;

            for (int i = 0; i < _playerList.childCount; i++)
            {
                Destroy(_playerList.GetChild(i).gameObject);
            }
            for (int i = 0; i < players.Length; i++)
            {
                Instantiate(_playerNamePrefab, _playerList).GetComponent<PlayerListItem>().SetUp(players[i]);
            }
            _startGameButton.SetActive(PhotonNetwork.IsMasterClient);
        }


        public override void OnMasterClientSwitched(Player newMasterClient)
        {
            _startGameButton.SetActive(PhotonNetwork.IsMasterClient);
        }


        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            _errorText.text = "Error " + message;
            MenuManager.instance.OpenMenu("error");
        }


        public void LeavRoom()
        {
            PhotonNetwork.LeaveRoom();
            MenuManager.instance.OpenMenu("loading");
        }

        public override void OnLeftRoom()
        {
            MenuManager.instance.OpenMenu("title");
        }


        public void JoinRoom(RoomInfo info)
        {
            PhotonNetwork.JoinRoom(info.Name);
            MenuManager.instance.OpenMenu("loading");
        }


        
        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {

            for (int i = 0; i < _roomList.childCount ; i++)
            {
                Destroy(_roomList.GetChild(i).gameObject);
            }

            for (int i = 0; i < roomList.Count; i++)
            {
                if (roomList[i].RemovedFromList)
                {
                    continue;
                }
                RoomListItem roomListItem = Instantiate(_roomButtonPrefab, _roomList).GetComponent<RoomListItem>();
                roomListItem.SetUp(roomList[i]);
            }
        }


    public override void OnPlayerEnteredRoom(Player player)
    {
        Instantiate(_playerNamePrefab, _playerList).GetComponent<PlayerListItem>().SetUp(player);
    }

}
