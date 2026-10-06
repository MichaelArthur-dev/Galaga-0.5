using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public TMP_Text scoreText;
    public TMP_Text waveText;
    private int score;
    private int waveNumber;
    private bool gameOver;

    void Awake()
    {
        if (instance != null && instance != this)
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
        if (gameOver) return;
        score += amount;
        UpdateText();
    }

    public void SetWave(int number)
    {
        if (gameOver) return;
        waveNumber = number;
        UpdateText();
    }

    void UpdateText()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
        if (waveText != null)
            waveText.text = "Wave: " + waveNumber;
    }

    public void TriggerGameOver()
    {
        if (gameOver) return;
        gameOver = true;
        RunResults.score = score;
        RunResults.wave = waveNumber;
        int previousBest = PlayerPrefs.GetInt("HIGH_SCORE", 0);
        RunResults.newHighScore = score > previousBest;
        if (RunResults.newHighScore)
        {
            PlayerPrefs.SetInt("HIGH_SCORE", score);
            PlayerPrefs.Save();
        }
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameOver");
    }


    void OnDestroy()
    {
        if (instance == this)
            instance = null;
    }
}
