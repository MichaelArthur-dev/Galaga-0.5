using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab for the bullet
    public Transform firePoint; // Point from where the bullet will be fired
    public float shootInterval = 3f; // Time interval between shots
    private float timer;
    private EnemyMovement movement;

    void Awake()
    {
        movement = GetComponent<EnemyMovement>();
    }

    void Start()
    {
        timer = Random.Range(0f, shootInterval); // Start the timer at a random value to stagger shooting
    }

    void Update()
    {
        if (movement == null || movement.formationPoint == null)
        {
            return;
        }
        // wait until the enemy reaches its formation point before starting to shoot
        if (Vector2.Distance(transform.position, movement.formationPoint.position) > 0.2f)
        {
            return;
        }
        timer += Time.deltaTime;
        if (timer >= shootInterval)
        {
           Instantiate(bulletPrefab, firePoint.position, Quaternion.identity); // Instantiate the bullet at the fire point
            timer = 0f; // Reset the timer
        }
    }
}
