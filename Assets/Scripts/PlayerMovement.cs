using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.InputSystem;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 7f; // Speed at which the player moves
    public float swipeThresold = 20f; // It is the mi
    private Vector2 touchStartPosition; // Position where the touch started
    private Rigidbody2D rb; // Reference to the Rigidbody2D component
    private float moveDirection; // Direction in which the player is moving

    void Awake()
    {
       rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        if (PausedMenu.InputBlocked)
        {
            moveDirection = 0f;
            return;
        }
        // Reset each frame so releasing the controls stops the ship.
        moveDirection = 0f;
        if (Keyboard.current != null)
        {
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
            {
                moveDirection = -1f;
            }
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
            {
                moveDirection = 1f;
            }
        }
        if (Touch.activeTouches.Count == 0)
        {
            return;
        }
        Touch finger = Touch.activeTouches[0];
        if (finger.phase == UnityEngine.InputSystem.TouchPhase.Began)
        {
            touchStartPosition = finger.screenPosition;
        }
        float swipeDistance = finger.screenPosition.x - touchStartPosition.x;
        if (Mathf.Abs(swipeDistance) > swipeThresold)
        {
            moveDirection = Mathf.Sign(swipeDistance);
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveDirection * moveSpeed, 0f);
    }
    void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }

    void OnDisable()
    {
        EnhancedTouchSupport.Disable();    
    }
}