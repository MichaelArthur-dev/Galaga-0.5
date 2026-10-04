using UnityEngine;
public class FormationController : MonoBehaviour
{
    public float moveAmount = 0.2f; // Amount to move the formation each time
    public float moveSpeed = 1f; // Speed at which the formation moves
    private Vector3 startPosition; // Starting position of the formation
    private float timer; // Timer to track movement intervals
    private float leftEdge; // Left edge of the screen
    private float rightEdge; // Right edge of the screen
    public Transform leftmostPoint;
    public Transform rightmostPoint;
    public float edgePadding = 0.4f;
    private float maximumX;
    private float minimumX; 

    void Start()
    {
        startPosition = transform.position; // Store the starting position
        Camera cam = Camera.main;
        float halfWidth = cam.orthographicSize * cam.aspect;
        leftEdge = cam.transform.position.x - halfWidth;
        rightEdge = cam.transform.position.x + halfWidth;
        float leftOffset = leftmostPoint.position.x - startPosition.x;
        float rightOffset = rightmostPoint.position.x - startPosition.x;
        minimumX = leftEdge + edgePadding - leftOffset;
        maximumX = rightEdge - edgePadding - rightOffset;
        if (minimumX > maximumX)
        {
            Debug.LogWarning("FormationController: The formation is too wide for the screen. Adjust the edgePadding or formation size.");
        }
    }
    void Update()
    {
        timer += Time.deltaTime; // Increment the timer by the time since the last frame
        float offset = Mathf.Sin(timer * moveSpeed) * moveAmount;
        float desiredX = startPosition.x + offset;
        float limitedX = Mathf.Clamp(desiredX, minimumX, maximumX);
        transform.position = new Vector3(limitedX, startPosition.y, startPosition.z); // Move the formation to the new position
    }
}
