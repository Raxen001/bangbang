using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class MyNetworkManager : NetworkManager
{
    private GameObject[] player;
    int localPlayerIndex;
    public GameObject FindLocalPlayer()
    {
        player = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < player.Length; i++)
        {
            if (player[i].GetComponent<PlayerMovement>().IsOwner)
            {
                localPlayerIndex = i;
                break;
            }
        }
        return player[localPlayerIndex];
    }
        public void Host()
    {
        StartHost();
    }

    public void Client()
    {
        StartClient();
    }
}
