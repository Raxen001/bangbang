using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class GunHandeler : NetworkBehaviour
{
    public GunSO gunSO;
    private Rigidbody2D bulletRigidbody;
    private GameObject bulletInstance;
    private Bullet bullet;

    public override void OnNetworkSpawn()
    {
        FindObjectOfType<ShootButton>().AssignValues();
    }
    public void Shoot()
    {
        if (!IsOwner) return;
        ShootBulletServerRpc();
        //bulletRigidbody.AddForce(Vector2.up * UpwardForce, ForceMode2D.Impulse);

    }

    private void AssignBulletIDAndDamage(ulong value)
    {
        bullet = bulletInstance.GetComponent<Bullet>();
        bullet.Damage = gunSO.damage;
        bullet.ID = value;
    }

    private void AssignBulletForce(GameObject bulletInstace)
    {
        bulletRigidbody = bulletInstance.GetComponent<Rigidbody2D>();
        bulletRigidbody.AddForce(Vector2.left * gunSO.bulletSpeed, ForceMode2D.Impulse);
    }
    [ServerRpc]
    public void ShootBulletServerRpc(ServerRpcParams serverRpcParams = default)
    {
        var clientId = serverRpcParams.Receive.SenderClientId;
        ShootBulletClientRpc(clientId);
    }

    [ClientRpc]
    private void ShootBulletClientRpc(ulong clientId)
    {
        bulletInstance = Instantiate(gunSO.bulletPrefab, transform.position, transform.rotation);
        AssignBulletForce(bulletInstance);
        AssignBulletIDAndDamage(clientId);
    }
}
