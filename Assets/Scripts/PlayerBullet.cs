using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float bulletSpeed = 10f; // Speed of the bullet
    public float lifetime = 3f; // Lifetime of the bullet in seconds

    void Start()
    {
        Destroy(gameObject, lifetime); // Destroy the bullet after its lifetime expires) 
    }
    void Update()
    {
        transform.Translate(Vector3.up * bulletSpeed * Time.deltaTime); // Moves upward consistently regardless of frame rate
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemy = other.GetComponent<Enemy>(); // Checks whether the object hit is an enemy
        if (enemy != null)
        {
            enemy.TakeDamage(1); // Deal damage to the enemy
            Destroy(gameObject); // Destroy the bullet
        }
    }
}
