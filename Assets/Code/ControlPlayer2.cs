using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPlayer2 : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    public GameObject bulletPrefab;
    public Transform firePoint;

    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleJump();
        HandleFire();
    }

    void HandleMovement()
    {
        float moveX = 0f;
        if (Input.GetKey(KeyCode.D))
        {
            moveX = moveSpeed;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            moveX = -moveSpeed;
        }

        rb.velocity = new Vector2(moveX, rb.velocity.y);
    }

    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    void HandleFire()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
    }
}
