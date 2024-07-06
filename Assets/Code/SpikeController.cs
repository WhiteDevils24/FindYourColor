using UnityEngine;
using System.Collections;

public class SpikeController : MonoBehaviour
{
    private Coroutine damageCoroutine; // To keep track of the damage coroutine

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            if (damageCoroutine == null) // Start the coroutine if it's not already running
            {
                if (other.CompareTag("Player1"))
                {
                    Player1Controller player1 = other.GetComponent<Player1Controller>();
                    damageCoroutine = StartCoroutine(DealDamageOverTime(player1));
                }
                else if (other.CompareTag("Player2"))
                {
                    Player2Controller player2 = other.GetComponent<Player2Controller>();
                    damageCoroutine = StartCoroutine(DealDamageOverTime(player2));
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            if (damageCoroutine != null) // Stop the coroutine when the player leaves the spike
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
        }
    }

    private IEnumerator DealDamageOverTime(Player1Controller player)
    {
        while (true)
        {
            player.TakeDamage(10); // Adjust the damage value as needed
            yield return new WaitForSeconds(1.0f); // Adjust the time interval as needed
        }
    }

    private IEnumerator DealDamageOverTime(Player2Controller player)
    {
        while (true)
        {
            player.TakeDamage(10); // Adjust the damage value as needed
            yield return new WaitForSeconds(1.0f); // Adjust the time interval as needed
        }
    }
}
