using UnityEngine;
using UnityEngine.Rendering;
// This script will made help with AI due to i was haviing issues getting it to fit on the screens 
// It positions the boundary colliders at the camera left and right edges so the player stays on screen across different phone shapes

public class ScreenBoundaries : MonoBehaviour
{
public BoxCollider2D leftBoundary; // Reference to the left boundary collider
    public BoxCollider2D rightBoundary; // Reference to the right boundary collider


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Camera cam = Camera.main; // Get the main camera
        float halfWidth = cam.orthographicSize * cam.aspect; // Calculate half the width of the camera's view in world units
        float leftEdge = cam.transform.position.x - halfWidth; // Calculate the left edge of the camera's view in world units
        float rightEdge = cam.transform.position.x + halfWidth; // Calculate the right edge of the camera's view in world units
        float leftShift = leftEdge - leftBoundary.bounds.max.x; // Calculate how much to shift the left boundary to align with the left edge of the camera's view
        leftBoundary.transform.position += Vector3.right * leftShift; // Move the left boundary so its right edge meets the screens left edge
        float rightShift = rightEdge - rightBoundary.bounds.min.x; // Calculate the distance needed to align the right boundarys left edge
        rightBoundary.transform.position += Vector3.right * rightShift; // Move the right boundary into position
    }
}
