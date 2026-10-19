using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("PlayScene"); // Takes you to the play screen
    }
 
    public void QuitGame()
    {
        Application.Quit(); // Quits the game 
    }

    public void OpenHelp()
    {
        SceneManager.LoadScene("HelpScene"); // Loads the help page
    }
}
