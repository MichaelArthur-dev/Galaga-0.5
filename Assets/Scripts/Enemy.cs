using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 1; // Gloop health
    
    public void TakeDamage(int damage)
    {
        health = health - damage;
        if (health <= 0)
        {
            Destroy(gameObject); // Destroy the Gloop when health reaches zero
        }
    }
}
