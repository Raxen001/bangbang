using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Cinemachine;

public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] private float speed = 3f;
    private bool isGrounded = true; 
    private VariableJoystick joyStick;
    private Vector3 moveDirection;
    private Rigidbody2D rb;
    private float jumpBoost = 10f;
    private Vector2 forceVector = Vector2.zero;
    private bool jumping = false;
    [SerializeField] private float verticalJumpBoost=1f;
    [SerializeField] private float horizontalJumpBoost=1f;
    private CinemachineVirtualCamera playerFollowCamera;
    [SerializeField] private float xVelocityLimit=100f;
    [SerializeField] private float yVelocityLimit=100f;
    private float jumpBoostConsumptionRate=1f;
    private float jumpBoostGrowthRate = 0.8f;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        //Getting the joystick component 
        if (!IsOwner) return;
        joyStick = GameObject.Find("Movement Joystick").GetComponent<VariableJoystick>();
        rb = GetComponent<Rigidbody2D>();
        playerFollowCamera = GameObject.FindGameObjectWithTag("Camera").GetComponent<CinemachineVirtualCamera>();
        SwitchCameraLookAt();
        SwitchCameraFollow();
    }
    void Update()
    {
        //checks whether its the player's object
        if (!IsOwner) return;
        //Movement
        //IsPlayerGrounded();
        MovePlayerHorizontally();
        ForceVectorAssigner();
        JumpBoostAssigner();
        JumpingAssigner();
        MovePlayerVertically();
        LimitVelocity();
    }
    private void MovePlayerHorizontally()
    {
        moveDirection = Vector3.zero;
        moveDirection.x = joyStick.Horizontal;
        //moveDirection.y = joyStick.Vertical;
        //transform.position += moveDirection * speed * Time.deltaTime;
        HorizontalMovement();
        SwitchCameraFollow();
    }

    private void HorizontalMovement()
    {
        if (isGrounded)
        {
            transform.position += moveDirection * speed * Time.deltaTime;
        }
        /*else
        {
            transform.position += moveDirection * speedInAir * Time.deltaTime;
        }*/
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        //Checks whether the player is in contact with the ground
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //checks whether the player is in contact with the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void MovePlayerVertically()
    {
        if (jumpBoost > 0 && jumping)
        {
            rb.AddForce(forceVector * Time.deltaTime,ForceMode2D.Impulse);
            jumpBoost -= jumpBoostConsumptionRate * Time.deltaTime;
        }
    }

    private void JumpBoostAssigner()
    {
        if (isGrounded && jumpBoost<10f)
        {
            jumpBoost += jumpBoostGrowthRate * Time.deltaTime;
        }
    }

    private void ForceVectorAssigner()
    {
        forceVector.x = joyStick.Horizontal * horizontalJumpBoost;
        forceVector.y = joyStick.Vertical * verticalJumpBoost;
    }

    private void JumpingAssigner()
    {
        if(joyStick.Vertical > 0f)
        {
            jumping = true;
        }
        else
        {
            jumping = false;
        }
        //Debug.Log(jumpBoost + OwnerClientId);
    }

    private void SwitchCameraLookAt()
    {
        playerFollowCamera.LookAt = transform;
    }

    private void SwitchCameraFollow()
    {
        playerFollowCamera.Follow = transform;
    }

    private void LimitVelocity()
    {
        if(rb.velocity.x > xVelocityLimit)
        {
            rb.velocity = new Vector2(xVelocityLimit,rb.velocity.y);
        }else if(rb.velocity.x < -xVelocityLimit)
        {
            rb.velocity = new Vector2(-xVelocityLimit, rb.velocity.y);
        }
        if (rb.velocity.y > yVelocityLimit)
        {
            rb.velocity = new Vector2(rb.velocity.x, yVelocityLimit);
        }
    }
}
