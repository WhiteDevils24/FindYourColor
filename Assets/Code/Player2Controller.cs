using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Controller : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;

    public GameObject bulletPrefab;
    public Transform firePoint;

    public int maxHealth = 100;
    private int currentHealth;

    private Rigidbody2D rb;
    private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        Fire();
    }

    private void Move()
    {
        float moveInput = 0;

        if (Input.GetKey(KeyCode.D))
            moveInput = 1;
        else if (Input.GetKey(KeyCode.A))
            moveInput = -1;

        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
    }

    private void Jump()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.W))
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }

    private void Fire()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            bullet.layer = LayerMask.NameToLayer("Bullet");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            // Handle player death
        }
    }
}
