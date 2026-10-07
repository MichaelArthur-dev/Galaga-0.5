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
        Time.timeScale = 1f;
        finalScoreText.text = "SCORE:\n" + RunResults.score;
        highScoreText.text = "HIGHEST SCORE:\n" + PlayerPrefs.GetInt("HIGH_SCORE", 0);
        finalWaveText.text = "WAVE: " + RunResults.wave;
        newHighScoreText.gameObject.SetActive(RunResults.newHighScore);
    }

    // Update is called once per frame
    public void PlayAgain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("PlayScene");
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
