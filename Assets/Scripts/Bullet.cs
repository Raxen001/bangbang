using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int damage;
    private ulong id;

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

    public int Damage
    {
        get
        {
            return damage;
        }

        set
        {
            damage = value;
        }
    }
    private void Start()
    {
        Destroy(gameObject, 10f);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        DestroyOnImpact(other.tag);
        DamagePlayer(other.gameObject);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        DestroyOnImpact(collision.gameObject.tag);
        DamagePlayer(collision.gameObject);
    }

    private void DestroyOnImpact(string tag)
    {
        if (tag == "Ground" || tag == "Wall")
        {
            Destroy(gameObject);
        }
    }

    private void DamagePlayer(GameObject player)
    {
        player.TryGetComponent<Health>(out Health health);
        if (health != null) health.UpdateHealth(damage,id);
    }

}
