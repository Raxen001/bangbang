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
    public bool canShoot = true;
    private Reload reload;

    public override void OnNetworkSpawn()
    {
        FindObjectOfType<ShootButton>().AssignValues();
        reload = GetComponent<Reload>();
        GunSwap();
    }

    private void GunSwap()
    {
        reload.AssignMagSize(gunSO.magSize);
        reload.ReloadMag();
    }
    public void Shoot(Vector2 coordinates)
    {
        if (!IsOwner) return;
        if (reload.BulletCounter())
        {
            StartCoroutine(DelayShooting(gunSO.fireRate));
            ShootBulletServerRpc(coordinates);
        }
        else
        {
            StartCoroutine(DelayShooting(gunSO.reloadTime));
            reload.ReloadMag();
        }
        
        //bulletRigidbody.AddForce(Vector2.up * UpwardForce, ForceMode2D.Impulse);

    }

    IEnumerator DelayShooting(float fireRate)
    {
        canShoot = false;
        yield return new WaitForSecondsRealtime(fireRate);
        canShoot = true;
    }

    private void AssignBulletIDAndDamage(ulong value)
    {
        bullet = bulletInstance.GetComponent<Bullet>();
        bullet.Damage = gunSO.damage;
        bullet.ID = value;
    }

    private void AssignBulletForce(Vector2 coordinates , GameObject bulletInstace)
    {
        bulletRigidbody = bulletInstance.GetComponent<Rigidbody2D>();
        bulletRigidbody.AddForce(coordinates * gunSO.bulletSpeed, ForceMode2D.Impulse);
    }
    [ServerRpc]
    public void ShootBulletServerRpc(Vector2 coordinates,ServerRpcParams serverRpcParams = default)
    {
        var clientId = serverRpcParams.Receive.SenderClientId;
        ShootBulletClientRpc(coordinates,clientId);
    }

    [ClientRpc]
    private void ShootBulletClientRpc(Vector2 coordinates,ulong clientId)
    {
        float angle = coordinates.y / coordinates.x;
        float zangle = Mathf.Atan(angle);
        Quaternion quaternion;
        quaternion = Quaternion.Euler(0f, 0f, zangle);
        bulletInstance = Instantiate(gunSO.bulletPrefab, transform.position,quaternion);
        AssignBulletForce(coordinates,bulletInstance);
        AssignBulletIDAndDamage(clientId);
    }
}
