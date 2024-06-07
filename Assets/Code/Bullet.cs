using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 20;
    public float lifetime = 2f;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    private void Start()
    {
        rb.velocity = transform.right * speed;
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.CompareTag("Player"))
        {
            return; // Ignore collision with the player who fired the bullet
        }

        Player1Controller player = hitInfo.GetComponent<Player1Controller>();
        if (player != null)
        {
            player.TakeDamage(damage);
        }

        Player2Controller player2 = hitInfo.GetComponent<Player2Controller>();
        if (player2 != null)
        {
            player2.TakeDamage(damage);
        }

        // Add logic for enemy damage if necessary

        Destroy(gameObject);

        Debug.Log("Bullet destroyed.");
    }
}
