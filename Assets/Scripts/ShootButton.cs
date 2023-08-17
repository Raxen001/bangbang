using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class ShootButton : NetworkBehaviour
{
    private GameObject player;
    private BulletHandeler bulletHandler;

    public override void OnNetworkSpawn()
    {
        player = FindObjectOfType<InputManager>().FindLocalPlayer();
        bulletHandler = player.GetComponent<BulletHandeler>();
    }

    public void ShootCall()
    {
        bulletHandler.Shoot();
    }
}
