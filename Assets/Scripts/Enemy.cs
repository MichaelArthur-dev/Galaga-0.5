using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 1; // Gloop health
    public int scoreValue = 10; // Score value for defeating the Gloop
    private bool defeated;

    public void TakeDamage(int damage)
    {
        if (defeated)
        {
            return;
        }
        health -= damage;
        if (health <= 0)
        {
            defeated = true;
            GameManager.instance.IncreaseScore(scoreValue);
            Destroy(gameObject);
        }
    }
}