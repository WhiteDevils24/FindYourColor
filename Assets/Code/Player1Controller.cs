using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Controller : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;

    public float fireRate = 0.5f;
    private float nextFireTime = 0f;

    public GameObject bulletPrefab;
    public Transform firePoint;

    public int maxHealth = 100;
    private int currentHealth;

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool facingRight = true;


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

        if (Input.GetKey(KeyCode.RightArrow))
            moveInput = 1;
        else if (Input.GetKey(KeyCode.LeftArrow))
            moveInput = -1;

        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        if (moveInput > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveInput < 0 && facingRight)
        {
            Flip();
        }
    }

    private void Jump()
    {
        if (isGrounded && Input.GetMouseButtonDown(1))
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }

    private void Fire()
    {
        if (Input.GetMouseButtonDown(0) && Time.time > nextFireTime) // Left mouse button for fire
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript != null)
            {
                bulletScript.SetDirection(facingRight ? Vector2.right : Vector2.left);
            }

            Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            nextFireTime = Time.time + fireRate; // Update next fire time
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

        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(10);
            TakeDamage(10);
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
