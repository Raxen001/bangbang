using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class InputManager : NetworkBehaviour
{
    private GameObject[] player;
    int localPlayerIndex;
    private PlayerMovement playerScript;

    // Start is called before the first frame update
    public void FindLocalPlayer()
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
        playerScript = player[localPlayerIndex].GetComponent<PlayerMovement>();
    }

}
