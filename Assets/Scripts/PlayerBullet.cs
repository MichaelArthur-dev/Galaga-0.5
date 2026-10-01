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
        transform.Translate(Vector3.up * bulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(1); // Deal damage to the enemy
            Destroy(gameObject); // Destroy the bullet
        }
    }
}
