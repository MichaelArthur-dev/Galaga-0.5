using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("PlayScene");
    }
 
    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenHelp()
    {
        SceneManager.LoadScene("HelpScene");
    }
}
