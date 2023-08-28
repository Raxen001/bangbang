using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerID : NetworkBehaviour
{
    [SerializeField] private ulong id;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if (IsOwner)
        {
            SendClientIDServerRPC();
        }

    }
    [ServerRpc]
    private void SendClientIDServerRPC(ServerRpcParams serverRpcParams = default)
    {
        AssignIDClientRPC(serverRpcParams.Receive.SenderClientId);
    }

    [ClientRpc]
    private void AssignIDClientRPC(ulong value)
    {
        id = value;
    }
        public ulong ID
    {
        get
        {
            return id;
        }
        set
        {
            id = value;
        }
    }
}
