﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerNameInputManager : MonoBehaviour
{
    public void SetPlayerName(string playername)
    {
        if(string.IsNullOrEmpty(playername))
        {
            Debug.Log("Player name is Empty.");
            return;
        }

        PhotonNetwork.NickName = playername;
    }

}
