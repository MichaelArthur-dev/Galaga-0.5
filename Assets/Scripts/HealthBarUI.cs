using UnityEngine;
using UnityEngine.UI;
public class HealthBarUI : MonoBehaviour
{
    public PlayerHealth PlayerHealth; // Player health script
    public Image healthImage; // Image of player health bar
    public Sprite[] healthSprites = new Sprite[5]; // Each different health bar states 

    void LateUpdate() // Refreshes the bar after health changes during the frame
    {
        if (PlayerHealth == null || healthImage == null)
            return;
        int health = PlayerHealth.health;
        if (health < 0 || health >= healthSprites.Length) // Prevents an invalid health value from indexing the sprite array
            return;
        if (healthSprites[health] != null)
            healthImage.sprite = healthSprites[health];
    }
}