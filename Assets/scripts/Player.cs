using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    public float playerSpeed = 3f;
    Rigidbody playerRb;

    private void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    public void Move(Vector2 dir)
    {
        playerRb.velocity = new Vector3 (-dir.x * playerSpeed, 0, -dir.y * playerSpeed);

        transform.forward = playerRb.velocity;
    }
}
