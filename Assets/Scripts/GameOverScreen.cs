using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public TMP_Text finalScoreText;
    public TMP_Text finalWaveText;
    public TMP_Text newHighScoreText;
    public TMP_Text highScoreText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1f; // Makes sure this screen is not affected by a paused games time scale
        finalScoreText.text = "SCORE:\n" + RunResults.score; // Display the player socre for the game
        highScoreText.text = "HIGHEST SCORE:\n" + PlayerPrefs.GetInt("HIGH_SCORE", 0); // Shows the highest score achieved
        finalWaveText.text = "WAVE: " + RunResults.wave; // Shows the waved your survived to
        newHighScoreText.gameObject.SetActive(RunResults.newHighScore); // If your score is the new highest score display new high score text
    }

    // Update is called once per frame
    public void PlayAgain()
    {
        Time.timeScale = 1f; // Ensures gameplay resumes at normal speed in the new scene
        SceneManager.LoadScene("PlayScene");
    }

    public void MainMenu()
    {
        Time.timeScale = 1f; // Prevents the main menu from inheriting a paused time scale
        SceneManager.LoadScene("MainMenu");
    }
}
