using UnityEngine;
using UnityEngine.UI;
public class LivesUI : MonoBehaviour
{
    public PlayerHealth playerHealth; // Playerhealth script 
    public Image livesImage; // Lives sprite image
    public Sprite[] livesSprites = new Sprite[4]; // Sprite at each index should show that many remaining lives

    void LateUpdate() // Refreshes the display after gameplay scripts update the players lives
    {
        if (playerHealth == null || livesImage == null)
            return;
        int lives = playerHealth.lives;
        if (lives < 0 || lives >= livesSprites.Length) // Avoids indexing the sprite array with an invalid lives value
            return;
        if (livesSprites[lives] != null)
            livesImage.sprite = livesSprites[lives];
    }
}