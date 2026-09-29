using System.Runtime.CompilerServices;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public float scrollSpeed = 2f; // Speed at which the background scrolls
    public float overlap = 0.2f; // Overlap between the two background images
    private float backgroundHeight; // Height of the background imag
    private float backgroundSpacing; // Spacing between the two background images

    void Start()
    {
        backgroundHeight = GetComponent<SpriteRenderer>().bounds.size.y; // Get the height of the background image

        backgroundSpacing = backgroundHeight - overlap; // Calculate the spacing between the two background images
    } // End of start

    void Update()
    {
        transform.Translate(Vector2.down * scrollSpeed * Time.deltaTime); // Move the background downwards

        if (transform.position.y <= -backgroundSpacing) // If the background has moved completely off screen
        {
            transform.position += Vector3.up * backgroundSpacing * 2f; // Move it back to the top
        }
    } // End of Update
} // End of class
