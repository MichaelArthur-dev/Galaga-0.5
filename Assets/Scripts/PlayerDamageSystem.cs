using UnityEngine;

public class PlayerDamageSystem : MonoBehaviour
{
    private PlayerHealth health; // connects this collision script to the players health system

    void Awake()
    {
        health = GetComponent<PlayerHealth>();
    }

    private void OnTriggerEnter2D(Collider2D other) // Handles bullets and enemies entering the players collider
    {
        if (other.CompareTag("EnemyBullet"))
        {
            health.TryTakeDamage(1); // Lets playerhealth handle invulnerability and life loss
            Destroy(other.gameObject); // Destroy the bullet after it hits the player
        }
        else if (other.CompareTag("Enemy"))
        {
            health.TryTakeDamage(1); // Applies damage when the player collides with an enemy
            {
                Destroy(other.gameObject); // Destroy the enemy after it hits the player
            }
        }
    }
}