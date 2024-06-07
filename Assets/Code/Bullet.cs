using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 20;
    public float lifetime = 2f; // Time after which the bullet is destroyed if it doesn't hit anything
    public bool isEnemyBullet = false; // Flag to check if the bullet is fired by an enemy

    private Rigidbody2D rb;
    private Vector2 direction;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component not found on bullet.");
        }
    }

    private void Start()
    {
        if (rb != null)
        {
            rb.velocity = direction * speed;
        }

        Destroy(gameObject, lifetime);
    }

    public void SetDirection(Vector2 direction)
    {
        this.direction = direction;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.CompareTag("Player1") || hitInfo.CompareTag("Player2"))
        {
            if (isEnemyBullet)
            {
                Player1Controller player = hitInfo.GetComponent<Player1Controller>();
                if (player != null)
                {
                    player.TakeDamage(damage);
                    Destroy(gameObject);
                }
                Player2Controller player2 = hitInfo.GetComponent<Player2Controller>();
                if (player2 != null)
                {
                    player2.TakeDamage(damage);
                    Destroy(gameObject);
                }
            }
            return; // Ignore collision with the player who fired the bullet
        }

        Enemy enemy = hitInfo.GetComponent<Enemy>();
        if (enemy != null && !isEnemyBullet)
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        Debug.Log("Bullet destroyed.");
    }
}
