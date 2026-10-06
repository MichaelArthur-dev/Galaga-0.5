using UnityEngine;
using UnityEngine.UI;
public class HealthBarUI : MonoBehaviour
{
    public PlayerHealth PlayerHealth;
    public Image healthImage;
    public Sprite[] healthSprites = new Sprite[5];

    void LateUpdate()
    {
        if (PlayerHealth == null || healthImage == null)
            return;
        int health = PlayerHealth.health;
        if (health < 0 || health >= healthSprites.Length)
            return;
        if (healthSprites[health] != null)
            healthImage.sprite = healthSprites[health];
    }
}