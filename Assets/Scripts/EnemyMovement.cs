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
    
    void FixedUpdate() // Moves the enemy during the physics update
    {
        if (formationPoint == null) // Waits until a formation point has been assigned
        {
            return;
        }
        Vector2 nextPosition = Vector2.MoveTowards(rb.position, formationPoint.position, moveSpeed * Time.fixedDeltaTime); // Moves toward the spot 
        rb.position = nextPosition;
    }
}
