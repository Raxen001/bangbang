using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class HostButtton : NetworkManager
{
    private InputManager input;

    private void Start()
    {
        input = FindObjectOfType<InputManager>();
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
