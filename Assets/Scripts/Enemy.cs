using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 1; // Gloop health
    public int scoreValue = 10; // Score value for defeating the Gloop
    public AudioClip explosionSound;
    [Range(0f, 1f)] public float explosionVolume = 0.4f;
    private bool defeated;

    public void TakeDamage(int damage)
    {
        if (defeated)
            return;
        health -= damage;
        {
            if (health <= 0)
            {
                defeated = true;
                GameManager.instance.IncreaseScore(scoreValue);

                if (explosionSound != null)
                {
                    GameObject soundObject = new GameObject("ExplosionSound");
                    AudioSource audio = soundObject.AddComponent<AudioSource>();
                    audio.clip = explosionSound;
                    audio.volume = explosionVolume;
                    audio.spatialBlend = 0f;
                    audio.Play();
                    Destroy(soundObject, explosionSound.length);
                }
                Destroy(gameObject);
            }
        }
    }
}