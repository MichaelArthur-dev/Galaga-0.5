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
    private float protectedUntil; // Stores when the players respawn protection ends
    private bool dead; // Prevents further damage and movement after game over
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

        if (Time.time < protectedUntil) // blinks the player during temporary invulnerability
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
        if (dead || Time.time < protectedUntil) // ignores damage while dead or protected
        {
            return false;
        }
        health = Mathf.Max(0, health - amount);
        UpdateDamageSprite();
        if (health == 0)
        {
            lives--; // Removes a life when all health is lost
            if (lives == 0)
            {
                dead = true;
                rb.linearVelocity = Vector2.zero;
                GameManager.instance.TriggerGameOver(); // ends the run when no lives remain
                gameObject.SetActive(false);
                return true;
            }
            health = maxHealth;
            UpdateDamageSprite();
            rb.position = startingPosition;
            rb.linearVelocity = Vector2.zero;
            GetComponent<PlayerMovement>().ResetMovement();
            protectedUntil = Time.time + respawnProtectionTime; // gives temporary protection after respawning
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