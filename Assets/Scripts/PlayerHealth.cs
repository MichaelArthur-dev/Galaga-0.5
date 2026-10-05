using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 4; // Maximum health of the player
    public int maxLives = 3; // Maximum lives of the player
    public int health;
    public int lives;
    public float respawnProtectionTime = 1.5f; // Time during which the player is invulnerable after respawning
    private Rigidbody2D rb;
    private Vector2 startingPosition;
    private float protectedUntil;
    private bool dead;
    public Sprite normalSprite;
    public Sprite slighltyDamagedSprite;
    public Sprite damagedSprite;
    public Sprite veryDamagedSprite;
    private SpriteRenderer sr;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        startingPosition = rb.position;
        health = maxHealth;
        lives = maxLives;
        sr = GetComponent<SpriteRenderer>();
        UpdateDamageSprite();
    }

    void Update()
    {
        if (dead)
        {
            return;
        }

        if (Time.time < protectedUntil)
        {
            float timeRemaining = protectedUntil - Time.time;
            sr.enabled = Mathf.Repeat(timeRemaining, 0.2f) < 0.1f; // Blink effect while protected
        }
        else
        {
            sr.enabled = true; // this ensures the sprite is visible when not protected
        }
    }

    public bool TryTakeDamage(int amount)
    {
        if (dead || Time.time < protectedUntil)
        {
            return false;
        }
        health = Mathf.Max(0, health - amount);
        UpdateDamageSprite();
        if (health == 0)
        {
            lives--;
            if (lives == 0)
            {
                dead = true;
                rb.linearVelocity = Vector2.zero;
                Debug.Log("Player has lost all lives.");
                gameObject.SetActive(false);
                return true;
            }
            health = maxHealth;
            UpdateDamageSprite();
            rb.position = startingPosition;
            rb.linearVelocity = Vector2.zero;
            protectedUntil = Time.time + respawnProtectionTime;
        }
        return true;
    }

    void UpdateDamageSprite()
    {
        if (health >= 4)
          sr.sprite = normalSprite;
        else if (health == 3)
          sr.sprite = slighltyDamagedSprite;
        else if (health == 2)
          sr.sprite = damagedSprite;
        else
          sr.sprite = veryDamagedSprite;
    }
}