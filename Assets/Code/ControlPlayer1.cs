using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPlayer1 : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    public GameObject bulletPrefab;
    public Transform firePoint;

    public Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleMovement();
        HandleJump();
        HandleFire();
    }

    void HandleMovement()
    {
        float moveX = 0f;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveX = moveSpeed;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveX = -moveSpeed;
        }

        rb.velocity = new Vector2(moveX, rb.velocity.y);
    }

    void HandleJump()
    {
        if (Input.GetMouseButtonDown(1)) // Right-click
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    void HandleFire()
    {
        if (Input.GetMouseButtonDown(0)) // Left-click
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
    }

    

}

