using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Health : NetworkBehaviour
{
    [SerializeField] private NetworkVariable<float> health = new NetworkVariable<float>(default,
        NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    private const int maxHealth = 100;
    private PlayerID playerID;

    public override void OnNetworkSpawn()
    {
        health.Value = maxHealth;
        health.OnValueChanged += HealthChanged;
        playerID = GetComponent<PlayerID>();
    }

    public void UpdateHealth(float value,ulong id)
    {
        if(IsOwner && playerID.ID != id)
        health.Value = health.Value - value;
    }

    private void HealthChanged(float previousValue , float newValue)
    {
        if(IsOwner)
        Debug.Log(newValue);
    }
}

