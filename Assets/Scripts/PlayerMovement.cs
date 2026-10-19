using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.EventSystems;
using System.Collections.Generic;

using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 7f;

    private Rigidbody2D rb;
    private Camera cam;

    private int movementTouchId = -1;
    private float touchOffset;
    private float targetX;
    private float keyboardDirection;
    private float halfWidth;

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

        if (PausedMenu.InputBlocked)
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

        bool foundMovementTouch = false;

        foreach (Touch finger in Touch.activeTouches)
        {
            // Only a newly placed finger can take control.
            if (movementTouchId == -1 &&
                finger.phase == TouchPhase.Began &&
                !IsOverUI(finger.screenPosition))
            {
                movementTouchId = finger.touchId;
                touchOffset = rb.position.x - WorldX(finger.screenPosition);
                targetX = rb.position.x;
            }

            if (finger.touchId != movementTouchId)
                continue;

            if (finger.phase == TouchPhase.Ended ||
                finger.phase == TouchPhase.Canceled)
                continue;

            foundMovementTouch = true;
            targetX = WorldX(finger.screenPosition) + touchOffset;
        }

        if (!foundMovementTouch)
            movementTouchId = -1;
    }

    void FixedUpdate()
    {
        if (PausedMenu.InputBlocked)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        float nextX;

        if (movementTouchId != -1)
            nextX = targetX;
        else
            nextX = rb.position.x +
                keyboardDirection * moveSpeed * Time.fixedDeltaTime;

        float screenHalfWidth = cam.orthographicSize * cam.aspect;
        float left = cam.transform.position.x - screenHalfWidth + halfWidth;
        float right = cam.transform.position.x + screenHalfWidth - halfWidth;

        nextX = Mathf.Clamp(nextX, left, right);

        rb.linearVelocity = Vector2.zero;
        rb.MovePosition(new Vector2(nextX, rb.position.y));
    }

    private float WorldX(Vector2 screenPosition)
    {
        float depth = transform.position.z - cam.transform.position.z;

        return cam.ScreenToWorldPoint(
            new Vector3(screenPosition.x, screenPosition.y, depth)
        ).x;
    }

    public static bool IsOverUI(Vector2 screenPosition)
    {
        if (EventSystem.current == null)
            return false;

        PointerEventData pointer = new PointerEventData(EventSystem.current);
        pointer.position = screenPosition;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointer, results);

        return results.Count > 0;
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