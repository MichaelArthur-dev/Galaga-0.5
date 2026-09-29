using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 7f; // Speed at which the player moves
    private Vector2 touchStartPosition; // Position where the touch started


    // Update is called once per frame
    void Update()
    {
        if (Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            touchStartPosition = Touchscreen.current.primaryTouch.position.ReadValue();
        }

        if (Touchscreen.current.primaryTouch.press.isPressed)
        {
           
        }
    }
}
