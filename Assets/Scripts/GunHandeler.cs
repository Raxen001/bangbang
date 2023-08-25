using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class GunHandeler : NetworkBehaviour
{
    public GunSO gunSO;
    private Rigidbody2D bulletRigidbody;
    private GameObject bulletInstance;

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

    [ServerRpc]
    public void ShootBulletServerRpc(ServerRpcParams serverRpcParams = default)
    {
        ShootBulletClientRpc();
    }

    [ClientRpc]
    private void ShootBulletClientRpc()
    {
        bulletInstance = Instantiate(gunSO.bulletPrefab, transform.position, transform.rotation);
        bulletRigidbody = bulletInstance.GetComponent<Rigidbody2D>();
        bulletRigidbody.AddForce(Vector2.left * gunSO.bulletSpeed, ForceMode2D.Impulse);
    }
}
