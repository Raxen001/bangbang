using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] private float speed = 3f;
    void Update()
    {
        //checks whether its the player's object
        if (!IsOwner) return;
        //Movement
        if (Input.GetKey(KeyCode.W))
        {
            gameObject.transform.Translate(Vector2.up * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            gameObject.transform.Translate(Vector2.down * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            gameObject.transform.Translate(Vector2.right* Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            gameObject.transform.Translate(Vector2.left * Time.deltaTime * speed);
        }
    }
}
