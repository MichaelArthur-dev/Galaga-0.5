using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab; // Reference to the bullet prefab
    public Transform firePoint; // Reference to the point from where the bullet will be fired
    public float shootCooldown = 0.25f; // Cooldown time between shots
    private float nextShootTime = 0f;

    void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }

    void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Shoot();
        }
        
        foreach (Touch finger in Touch.activeTouches)
        {
            if (finger.phase == UnityEngine.InputSystem.TouchPhase.Ended && finger.isTap)
            {
                Shoot();
            }
        }
    }

    void Shoot()
    {
        if (Time.time < nextShootTime)
        {
            return;
        }
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        nextShootTime = Time.time + shootCooldown;
    }
}
