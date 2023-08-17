using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 10f);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Ground" || other.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
