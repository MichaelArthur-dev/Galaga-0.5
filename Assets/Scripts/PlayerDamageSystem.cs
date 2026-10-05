using UnityEngine;

public class PlayerDamageSystem : MonoBehaviour
{
    private PlayerHealth health;

    void Awake()
    {
        health = GetComponent<PlayerHealth>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyBullet"))
        {
            health.TryTakeDamage(1);
            Destroy(other.gameObject); // Destroy the bullet after it hits the player
        }
        else if (other.CompareTag("Enemy"))
        {
            health.TryTakeDamage(1);
            {
                Destroy(other.gameObject); // Destroy the enemy after it hits the player
            }
        }
    }
}