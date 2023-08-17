using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class BulletHandeler : NetworkBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    private GameObject bulletInstance;
    private Rigidbody2D bulletRigidbody;
    [SerializeField] private float bulletSpeed = 30f;
    [SerializeField] private float UpwardForce = 30f;
    public void Shoot()
    {
        if (!IsOwner) return;
        ShootBulletServerRpc();
        //bulletRigidbody.AddForce(Vector2.up * UpwardForce, ForceMode2D.Impulse);

    }

    [ServerRpc]
    public void ShootBulletServerRpc(ServerRpcParams serverRpcParams = default)
    {
        ShootBulletClientRpc();
    }

    [ClientRpc]
    private void ShootBulletClientRpc()
    {
        bulletInstance = Instantiate(bulletPrefab, transform.position, transform.rotation);
        bulletRigidbody = bulletInstance.GetComponent<Rigidbody2D>();
        bulletRigidbody.AddForce(Vector2.left * bulletSpeed, ForceMode2D.Impulse);
    }
}
