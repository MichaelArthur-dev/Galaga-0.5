using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float bulletSpeed = 4f; // Speed of the bullet
    public float lifetime = 5f; // Lifetime of the bullet before it gets destroyed

    void Start()
    {
        Destroy(gameObject, lifetime); // Destroy the bullet after its lifetime expires
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * bulletSpeed * Time.deltaTime, Space.World); // Move the bullet downwards
    }
}
