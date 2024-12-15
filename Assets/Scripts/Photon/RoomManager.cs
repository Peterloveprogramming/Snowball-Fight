using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
public class RoomManager : MonoBehaviourPunCallbacks
{
    private string mapType;

    public TextMeshProUGUI OccupancyRateText_ForRoom1;
    public TextMeshProUGUI OccupancyRateText_ForRoom2;
    public TextMeshProUGUI OccupancyRateText_ForRoom3;



    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;

        // this will connect the player automatically 
        // if (!PhotonNetwork.IsConnectedAndReady)
        // {
        //     PhotonNetwork.ConnectUsingSettings();
        // }
        // else
        // {
        //     PhotonNetwork.JoinLobby();
        // }
    }

    // Update is called once per frame
    void Update()
    {

    }

    #region UI Callback Methods
    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public void OnEnterButtonClicked_ROOM1()
    {
        mapType = MultiplayerVRConstants.ROOM_NUMBER_VALUE_ROOM1;
        ExitGames.Client.Photon.Hashtable expectedCustomRoomProperties = new ExitGames.Client.Photon.Hashtable() { { MultiplayerVRConstants.ROOM_TYPE_KEY, mapType } };
        PhotonNetwork.JoinRandomRoom(expectedCustomRoomProperties, 0);
    }

    public void OnEnterButtonClicked_ROOM2()
    {
        mapType = MultiplayerVRConstants.ROOM_NUMBER_VALUE_ROOM2;
        ExitGames.Client.Photon.Hashtable expectedCustomRoomProperties = new ExitGames.Client.Photon.Hashtable() { {MultiplayerVRConstants.ROOM_TYPE_KEY, mapType } };
        PhotonNetwork.JoinRandomRoom(expectedCustomRoomProperties,0);
    }

        public void OnEnterButtonClicked_ROOM3()
    {
        mapType = MultiplayerVRConstants.ROOM_NUMBER_VALUE_ROOM3;
        ExitGames.Client.Photon.Hashtable expectedCustomRoomProperties = new ExitGames.Client.Photon.Hashtable() { { MultiplayerVRConstants.ROOM_TYPE_KEY, mapType } };
        PhotonNetwork.JoinRandomRoom(expectedCustomRoomProperties, 0);
    }
    #endregion

    #region Photon Callback Methods
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log(message);
        CreateAndJoinRoom();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to servers again.");
        PhotonNetwork.JoinLobby();
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("A room is created with the name: " + PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("The Local player: " + PhotonNetwork.NickName + " joined to " + PhotonNetwork.CurrentRoom.Name + " Player count " + PhotonNetwork.CurrentRoom.PlayerCount);

        if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey(MultiplayerVRConstants.ROOM_TYPE_KEY))
        {
            object mapType;
            if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue(MultiplayerVRConstants.ROOM_TYPE_KEY,out mapType))
            {
                Debug.Log("Joined room with the map: " + (string)mapType);
                if ((string)mapType == MultiplayerVRConstants.ROOM_NUMBER_VALUE_ROOM1)
                {
                    //Load the school scene
                    PhotonNetwork.LoadLevel("Room1");

                }else if ((string)mapType == MultiplayerVRConstants.ROOM_NUMBER_VALUE_ROOM2)
                {
                    //Load the outdoor scene
                    PhotonNetwork.LoadLevel("Room2");
                }else if ((string)mapType == MultiplayerVRConstants.ROOM_NUMBER_VALUE_ROOM3)
                {
                    //Load the outdoor scene
                    PhotonNetwork.LoadLevel("Room3");
                }
            }
        }


    }


    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName + " joined to: " + "Player count: " + PhotonNetwork.CurrentRoom.PlayerCount);
    }


    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        if (roomList.Count == 0)
        {
            //There is no room at all
            OccupancyRateText_ForRoom1.text = 0 + " / " + 6;
            OccupancyRateText_ForRoom2.text = 0 + " / " + 6;
            OccupancyRateText_ForRoom3.text = 0 + " / " + 6;


        }

        foreach (RoomInfo room in roomList)
        {
            Debug.Log(room.Name);
            if (room.Name.Contains(MultiplayerVRConstants.ROOM_NUMBER_VALUE_ROOM1))
            {
                //Update the Outdoor room occupancy field
                Debug.Log("Room is a room1. Player count is: " + room.PlayerCount);

                OccupancyRateText_ForRoom1.text = room.PlayerCount + " / " + 6;

            }else if (room.Name.Contains(MultiplayerVRConstants.ROOM_NUMBER_VALUE_ROOM2))
            {
                Debug.Log("Room is a room2 map. Player count is: " +room.PlayerCount);
                OccupancyRateText_ForRoom2.text = room.PlayerCount + " / " + 6;
            }else if (room.Name.Contains(MultiplayerVRConstants.ROOM_NUMBER_VALUE_ROOM3))
            {
                Debug.Log("Room is a room3. Player count is: " +room.PlayerCount);
                OccupancyRateText_ForRoom3.text = room.PlayerCount + " / " + 6;
            }
        }


    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined the Lobby.");
    }
    #endregion

    #region Private Methods
    private void CreateAndJoinRoom()
    {
        string randomRoomName = "Room_" +mapType + Random.Range(1, 3);
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 6;


        string[] roomPropsInLobby = { MultiplayerVRConstants.ROOM_TYPE_KEY };
        //We have 3 different rooms
        //1. room1
        //2. room2
        //2. room3

        ExitGames.Client.Photon.Hashtable customRoomProperties = new ExitGames.Client.Photon.Hashtable() { {MultiplayerVRConstants.ROOM_TYPE_KEY, mapType } };

        roomOptions.CustomRoomPropertiesForLobby = roomPropsInLobby;
        roomOptions.CustomRoomProperties = customRoomProperties;

        PhotonNetwork.CreateRoom(randomRoomName, roomOptions);

    }


    #endregion
}
