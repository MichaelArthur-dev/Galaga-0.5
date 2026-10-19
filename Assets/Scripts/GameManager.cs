using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Lets other scripts use the game manager
    public TMP_Text scoreText;
    public TMP_Text waveText;
    private int score;
    private int waveNumber;
    private bool gameOver;

    void Awake()
    {
        if (instance != null && instance != this) // Stops a second game manager from being used
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        Time.timeScale = 1f;
        UpdateText();
    }
    public void IncreaseScore(int amount)
    {
        if (gameOver) return; // Stops the score changing after game over
        score += amount;
        UpdateText();
    }
    public void SetWave(int number)
    {
        if (gameOver) return; // Stops the wave changing after game over
        waveNumber = number;
        UpdateText();
    }
    void UpdateText()
    {
        if (scoreText != null) // Checks if the score text is set in the Inspector
            scoreText.text = "Score: " + score;
        if (waveText != null) // Checks if the wave text is set in the Inspector
            waveText.text = "Wave: " + waveNumber;
    }
    public void TriggerGameOver()
    {
        if (gameOver) return; // Makes sure game over only runs once
        gameOver = true;
        RunResults.score = score;
        RunResults.wave = waveNumber;
        int previousBest = PlayerPrefs.GetInt("HIGH_SCORE", 0); // Uses 0 if there is no saved high score yet
        RunResults.newHighScore = score > previousBest;
        if (RunResults.newHighScore)
        {
            PlayerPrefs.SetInt("HIGH_SCORE", score);
            PlayerPrefs.Save(); // Saves the new high score
        }
        Time.timeScale = 1f; // Makes sure the game is not still paused
        SceneManager.LoadScene("GameOver");
    }
    void OnDestroy()
    {
        if (instance == this) // Clears the reference when this manager is removed
            instance = null;
    }
}
