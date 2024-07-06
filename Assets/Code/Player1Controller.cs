using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player1Controller : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;

    public float fireRate = 0.5f;
    private float nextFireTime = 0f;

    public GameObject bulletPrefab;
    public Transform firePoint;

    public int maxHealth = 100;
    public int currentHealth;

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool facingRight = true;

    private bool isGameOver = false;

    public Animator animator;

    public float fallLimit = -10f; // Define the fall limit


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogError("Animator component not found!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        Fire();
        CheckFall();
        UpdateAnimation();
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

        animator.SetBool("IsRunning", moveInput != 0);
    }

    private void Jump()
    {
        if (isGrounded && Input.GetMouseButtonDown(1))
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            animator.SetTrigger("Jump");
            isGrounded = false;

        }
    }
    private void CheckFall()
    {
        if (transform.position.y < fallLimit)
        {
            // Trigger game over
            LoadGameOverScene();
        }
    }


    private void UpdateAnimation()
    {
        animator.SetBool("IsGrounded", isGrounded); // Update grounded state
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

            animator.SetTrigger("Shoot");

            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision == null)
        {
            Debug.LogWarning("Collision is null!");
            return;
        }

        if (collision.gameObject == null)
        {
            Debug.LogWarning("Collision gameObject is null!");
            return;
        }

        if (collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = true;

            if (animator != null)
            {
                animator.SetBool("IsGrounded", true);
            }
            else
            {
                Debug.LogWarning("Animator is null when trying to set 'isGrounded' to true.");
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision == null)
        {
            Debug.LogWarning("Collision is null!");
            return;
        }

        if (collision.gameObject == null)
        {
            Debug.LogWarning("Collision gameObject is null!");
            return;
        }

        if (collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = false;

            if (animator != null)
            {
                animator.SetBool("IsGrounded", false);
            }
            else
            {
                Debug.LogWarning("Animator is null when trying to set 'isGrounded' to false.");
            }
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

    private void DestroyPlayer()
    {
        if (!isGameOver)
        {
            isGameOver = true;
            Debug.Log("Game Over!");
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;

            // Remove from CameraFollow before destroying
            CameraFollow cameraFollow = FindObjectOfType<CameraFollow>();
            if (cameraFollow != null)
            {
                cameraFollow.RemoveTarget(transform);
            }

            // Load the Game Over scene after a short delay
            LoadGameOverScene();
        }
    }

    private void LoadGameOverScene()
    {
        SceneManager.LoadScene("GameOverScene");
    }

    private void OnDestroy()
    {
        if (!isGameOver)
        {
            DestroyPlayer();
        }
    }
}
