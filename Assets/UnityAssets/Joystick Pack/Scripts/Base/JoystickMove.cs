using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickMove : MonoBehaviour
{
    // Joystick으로 Player 움직이기
    public Joystick moveJoystick;
    Rigidbody playerRb;

    Player player; 

    public void Start()
    {
        player = gameObject.GetComponent<Player>();

        playerRb = GetComponent<Rigidbody>();

    }

    private  void FixedUpdate()
    {
        if (moveJoystick.Direction.y != 0)
        {
            //velocity - 속도
            // playerRb.velocity = new Vector3
            //     (-moveJoystick.Direction.x * playerSpeed, 0, -moveJoystick.Direction.y * playerSpeed);

            player.Move( moveJoystick.Direction);
        }
        else
        {
            playerRb.velocity = Vector3.zero;
        }
    }
}