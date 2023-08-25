using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Health : NetworkBehaviour
{
    private NetworkVariable<float> health = new NetworkVariable<float>(default,
        NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    private const int maxHealth = 100;

    public override void OnNetworkSpawn()
    {
        health.Value = maxHealth;
        health.OnValueChanged += HealthChanged;
    }

    public void UpdateHealth(float value)
    {
        if(IsOwner)
        health.Value = health.Value - value;
    }

    private void HealthChanged(float previousValue , float newValue)
    {
        if(IsOwner)
        Debug.Log(newValue);
    }
}

