using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3f;
    public int maxHealth = 50;
    public float fireRate = 2f;

    public GameObject enemyBulletPrefab;
    public Transform firePoint;

    public Transform leftPoint;
    public Transform rightPoint;

    private bool facingRight = true;

    private float nextFireTime;
    private int currentHealth;
    private bool movingRight = true;

    private void Start()
    {
        currentHealth = maxHealth;
        nextFireTime = Time.time + fireRate;
    }

    private void Update()
    {
        Patrol();
        Fire();
    }

    private void Patrol()
    {
        if (movingRight)
        {
            transform.position = Vector2.MoveTowards(transform.position, rightPoint.position, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, rightPoint.position) < 0.1f)
            {
                movingRight = false;
                Flip(true);
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, leftPoint.position, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, leftPoint.position) < 0.1f)
            {
                movingRight = true;
                Flip(false);
            }
        }
    }

    private void Flip(bool facingRight)
    {
        if (this.facingRight != facingRight)
        {
            this.facingRight = facingRight;
            Vector3 scaler = transform.localScale;
            scaler.x *= -1;
            transform.localScale = scaler;
        }
    }

    private void Fire()
    {
        if (Time.time > nextFireTime)
        {
            GameObject bullet = Instantiate(enemyBulletPrefab, firePoint.position, firePoint.rotation);
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript != null)
            {
                bulletScript.isEnemyBullet = true;
                bulletScript.SetDirection(facingRight ? Vector2.right : Vector2.left); // Fire based on facing direction
            }
            nextFireTime = Time.time + fireRate;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player1Controller player = collision.gameObject.GetComponent<Player1Controller>();
        if (player != null)
        {
            player.TakeDamage(10);
            TakeDamage(10);
        }

        Player2Controller player2 = collision.gameObject.GetComponent<Player2Controller>();
        if (player2 != null)
        {
            player2.TakeDamage(10);
            TakeDamage(10);
        }
    }
}
