using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;           // Access photon classes and connect to servers
using Photon.Realtime;      // To Provide RoomOption when creating a new room

public class LaunchManager : MonoBehaviourPunCallbacks
{
    public GameObject EnterGamePanel;
    public GameObject ConnectionStatusPanel;
    public GameObject LobbyPanel;


    #region Unity Methods

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        EnterGamePanel.SetActive(true);
        ConnectionStatusPanel.SetActive(false);
        LobbyPanel.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #endregion

    #region Public Methods

    public void ConnectToPhotonServer()             
    {
        if(!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();           // connect to servers
            ConnectionStatusPanel.SetActive(true);
            EnterGamePanel.SetActive(false);

        }
    }

    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();                    // Join a random room 
    }

    #endregion

    #region Photon Callbacks

    public override void OnConnectedToMaster()              // Pun Callback Method when we connect to the master server
    {
        Debug.Log(PhotonNetwork.NickName + " Connected to Photon Server.");             // Shows player name who joined the photon server
        LobbyPanel.SetActive(true);
        ConnectionStatusPanel.SetActive(false);
    }

    public override void OnConnected()                     // Called when we have an internet connection
    {
        Debug.Log("Connected to Internet.");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)           // When a player fails to join a room
    {
        base.OnJoinRandomFailed(returnCode, message);
        Debug.Log(message);
        CreateAndJoinRoom();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.NickName + " joined to " + PhotonNetwork.CurrentRoom.Name);
        PhotonNetwork.LoadLevel("GameScene");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName + " joined to " + PhotonNetwork.CurrentRoom.Name + " " + PhotonNetwork.CurrentRoom.PlayerCount);
    }
    #endregion

    #region Private Methods

    void CreateAndJoinRoom()                                                            // Create a new room when a player failed to join a room
    {
        string randomRoomName = "Room " + Random.Range(0, 10000);                       // Generate Random RoomName

        RoomOptions roomOptions = new RoomOptions();                                    // Get RoomOptions
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;
        roomOptions.MaxPlayers = 20;

        PhotonNetwork.CreateRoom(randomRoomName, roomOptions);                         // Create a new Room
    }

    #endregion

}
