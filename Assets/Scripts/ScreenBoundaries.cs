using UnityEngine;
using UnityEngine.Rendering;

public class ScreenBoundaries : MonoBehaviour
{
public BoxCollider2D leftBoundary;
public BoxCollider2D rightBoundary;
        
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Camera cam = Camera.main;
        float halfWidth = cam.orthographicSize * cam.aspect;
        float leftEdge = cam.transform.position.x - halfWidth;
        float rightEdge = cam.transform.position.x + halfWidth;
        float leftShift = leftEdge - leftBoundary.bounds.max.x;
        leftBoundary.transform.position += Vector3.right * leftShift;
        float rightShift = rightEdge - rightBoundary.bounds.min.x;
        rightBoundary.transform.position += Vector3.right * rightShift;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
