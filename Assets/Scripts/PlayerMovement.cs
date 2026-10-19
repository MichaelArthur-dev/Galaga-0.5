using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 7f; // Controls the player's movement speed
    private Rigidbody2D rb;
    private Camera cam; // Used to convert touch positions and calculate screen limits
    private int movementTouchId = -1; // Identifies the touch currently controlling movement
    private float touchOffset; // Keeps the player from jumping when a touch begins
    private float targetX;
    private float keyboardDirection;
    private float halfWidth; // Keeps the whole player inside the camera view

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        halfWidth = sprite != null ? sprite.bounds.extents.x : 0.3f;
        targetX = rb.position.x;
    }
    void Update()
    {
        keyboardDirection = 0f; 
        if (PausedMenu.InputBlocked) // Prevents movement while the pause menu is open
        {
            movementTouchId = -1;
            targetX = rb.position.x;
            rb.linearVelocity = Vector2.zero;
            return;
        }
        if (Keyboard.current != null)
        {
            if (Keyboard.current.aKey.isPressed ||
                Keyboard.current.leftArrowKey.isPressed)
                keyboardDirection -= 1f;
            if (Keyboard.current.dKey.isPressed ||
                Keyboard.current.rightArrowKey.isPressed)
                keyboardDirection += 1f;
        }
        bool foundMovementTouch = false; // Tracks whether the controlling touch is still active
        foreach (Touch finger in Touch.activeTouches)
        {
            if (movementTouchId == -1 &&
                finger.phase == TouchPhase.Began &&
                !IsOverUI(finger.screenPosition))
            {
                movementTouchId = finger.touchId; // Locks movement to the first valid touch
                touchOffset = rb.position.x - WorldX(finger.screenPosition); // Prevents a jump when the touch begins
                targetX = rb.position.x;
            }
            if (finger.touchId != movementTouchId)
                continue;
            if (finger.phase == TouchPhase.Ended ||
                finger.phase == TouchPhase.Canceled)
                continue;
            foundMovementTouch = true;
            targetX = WorldX(finger.screenPosition) + touchOffset; // Follows the finger horizontally
        }
        if (!foundMovementTouch)
            movementTouchId = -1; // Allows a future touch to take control after this touch ends
    }
    void FixedUpdate()
    {
        if (PausedMenu.InputBlocked) // Stops physics movement while the pause menu is open
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }
        float nextX;
        if (movementTouchId != -1) // Touch movement takes priority over keyboard movement
            nextX = targetX;
        else
            nextX = rb.position.x +
                keyboardDirection * moveSpeed * Time.fixedDeltaTime;
        float screenHalfWidth = cam.orthographicSize * cam.aspect; // Calculates half of the camera's horizontal view
        float left = cam.transform.position.x - screenHalfWidth + halfWidth; // Sets the left movement limit
        float right = cam.transform.position.x + screenHalfWidth - halfWidth; // Sets the right movement limit

        nextX = Mathf.Clamp(nextX, left, right); // Keeps the player inside the camera view

        rb.linearVelocity = Vector2.zero;
        rb.MovePosition(new Vector2(nextX, rb.position.y)); // Moves through the Rigidbody2D during the physics update
    }
    private float WorldX(Vector2 screenPosition) // Converts a screen touch position into a world X position
    {
        float depth = transform.position.z - cam.transform.position.z; // Gives the camera the correct distance for conversion

        return cam.ScreenToWorldPoint(
            new Vector3(screenPosition.x, screenPosition.y, depth)).x;
    }
    public static bool IsOverUI(Vector2 screenPosition)
    {
        if (EventSystem.current == null)
            return false;
        PointerEventData pointer = new PointerEventData(EventSystem.current);
        pointer.position = screenPosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointer, results);

        return results.Count > 0; // Prevents gameplay movement when the touch is over UI
    }

    public void ResetMovement()
    {
        movementTouchId = -1;
        keyboardDirection = 0f;
        targetX = rb.position.x;
        rb.linearVelocity = Vector2.zero;
    }
    void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }
    void OnDisable()
    {
        movementTouchId = -1;
        if (rb != null)
            rb.linearVelocity = Vector2.zero;
        EnhancedTouchSupport.Disable();
    }
}