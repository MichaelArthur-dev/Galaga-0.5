using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 1; // Gloop health
    public int scoreValue = 10; // Score value for defeating the Gloop
    public AudioClip explosionSound; // The sound effect of the explosion
    [Range(0f, 1f)] public float explosionVolume = 0.4f; // Bar to change the audio in the inspector for the explosion sound effect
    private bool defeated;

    public void TakeDamage(int damage)
    {
        if (defeated) // Stops the enemy from giving score more than once
            return;
        health -= damage;
        {
            if (health <= 0)
            {
                defeated = true; // Marks it defeated before destroying it
                GameManager.instance.IncreaseScore(scoreValue); // Adds this enemys score to the players total

                if (explosionSound != null)
                {
                    GameObject soundObject = new GameObject("ExplosionSound"); // Plays the sound from a temporary object so it can finish after the enemy is removed
                    AudioSource audio = soundObject.AddComponent<AudioSource>();
                    audio.clip = explosionSound;
                    audio.volume = explosionVolume;
                    audio.spatialBlend = 0f; 
                    audio.Play(); // Makes the explosion sound play 
                    Destroy(soundObject, explosionSound.length);
                }
                Destroy(gameObject);
            }
        }
    }
}