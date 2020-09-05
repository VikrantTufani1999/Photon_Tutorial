using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PixelGunGameManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    GameObject playerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        if(playerPrefab != null)
        {
            if (PhotonNetwork.IsConnected)
            {
                int randomPoint = Random.Range(-20, 20);
                PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(randomPoint, 0, randomPoint), Quaternion.identity);
            }
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.NickName + " joined to " + PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName + " joined to " + PhotonNetwork.CurrentRoom.Name + " " + PhotonNetwork.CurrentRoom.PlayerCount);
    }
}
