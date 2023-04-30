using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class InputManager : NetworkBehaviour
{
    private GameObject[] player;
    int localPlayer;
    private PlayerMovement playerScript;

    // Start is called before the first frame update
    public void FindLocalPlayer()
    {
        player = GameObject.FindGameObjectsWithTag("Player");
        for(int i = 0; i < player.Length; i++)
        {
            if (player[i].GetComponent<PlayerMovement>().IsOwner)
            {
                localPlayer = i;
                break;
            }
        }
        playerScript = player[localPlayer].GetComponent<PlayerMovement>();
    }

    public void MoveLeft()
    {
        playerScript.MoveLeft();
    }

    public void MoveUp()
    {
        playerScript.MoveUp();
    }
    public void MoveRight()
    {
        playerScript.MoveRight();
    }
    public void MoveDown()
    {
        playerScript.MoveDown();
    }

    // Update is called once per frame

}
