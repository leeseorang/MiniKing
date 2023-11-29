using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickMove : MonoBehaviour
{
    // Joystick으로 Player 움직이기
    public Joystick moveJoystick;
    Rigidbody playerRb;

    Player player;

    Animator runAnim;

    public void Start()
    {
        player = gameObject.GetComponent<Player>();

        playerRb = GetComponent<Rigidbody>();

        runAnim = gameObject.GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (moveJoystick.Direction.y != 0)
        {
            player.Move(moveJoystick.Direction);
            runAnim.SetBool("isRun", true);
        }
        else
        {
            playerRb.velocity = Vector3.zero;
            runAnim.SetBool("isRun", false);
        }
    }
}