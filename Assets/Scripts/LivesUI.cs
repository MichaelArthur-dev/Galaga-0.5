using UnityEngine;
using UnityEngine.UI;
public class LivesUI : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public Image livesImage;
    public Sprite[] livesSprites = new Sprite[4];

    void LateUpdate()
    {
        if (playerHealth == null || livesImage == null)
            return;
        int lives = playerHealth.lives;
        if (lives < 0 || lives >= livesSprites.Length)
            return;
        if (livesSprites[lives] != null)
            livesImage.sprite = livesSprites[lives];
    }
}