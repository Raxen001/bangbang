using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class InputManager : NetworkBehaviour
{
    private GameObject[] player;
    int localPlayerIndex;

    // Start is called before the first frame update
    public GameObject FindLocalPlayer()
    {
        player = GameObject.FindGameObjectsWithTag("Player");
        for(int i = 0; i < player.Length; i++)
        {
            if (player[i].GetComponent<PlayerMovement>().IsOwner)
            {
                localPlayerIndex = i;
                break;
            }
        }
        return player[localPlayerIndex];
    }

}
