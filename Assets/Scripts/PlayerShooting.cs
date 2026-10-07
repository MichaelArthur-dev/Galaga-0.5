using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab; // Reference to the bullet prefab
    public Transform firePoint; // Reference to the point from where the bullet will be fired
    public float shootCooldown = 0.25f; // Cooldown time between shots
    private float nextShootTime = 0f; // Time when the player can shoot again

    void OnEnable()
    {
        EnhancedTouchSupport.Enable(); // Enable enhanced touch support
    }

    void OnDisable()
    {
        EnhancedTouchSupport.Disable(); // Disable enhanced touch support
    }

    void Update()
    {
        if (PausedMenu.InputBlocked) return; // Check if input is blocked by the pause menu
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame) // Check for space key press
        {
            Shoot(); // Call the Shoot method when space key is pressed
        }
        
        foreach (Touch finger in Touch.activeTouches) // Iterate through all active touches
        {
            if (finger.phase == UnityEngine.InputSystem.TouchPhase.Ended && finger.isTap) // Check if the touch has ended and is a tap
            {
                Shoot(); // Call the Shoot method when a tap is detected
            }
        }
    }

    void Shoot()
    {
        if (Time.time < nextShootTime) // Check if the cooldown period has passed
        {
            return; // If not exit the method without shooting
        }
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation); // Instantiate the bullet prefab at the fire points position and rotation
        nextShootTime = Time.time + shootCooldown; // Update the next shoot time based on the cooldown
    }
}
