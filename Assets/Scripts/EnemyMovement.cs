using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 4f; // Speed at which the enemy moves
    public Transform formationPoint; // Point to which the enemy will move
    private Rigidbody2D rb; // Reference to the Rigidbody2D component

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
    }   
    
    void FixedUpdate()
    {
        if (formationPoint == null)
        {
            return;
        }
        Vector2 nextPosition = Vector2.MoveTowards(rb.position, formationPoint.position, moveSpeed * Time.fixedDeltaTime);
        rb.position = nextPosition;
    }
}
