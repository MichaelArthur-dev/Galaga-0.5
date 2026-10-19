using UnityEngine;
using UnityEngine.SceneManagement;
public class HelpMenu : MonoBehaviour
{
    public void BackToMainMenu()
    {
        // If a pause menu exists help was opened during gameplay so return to the paused game
        if (PausedMenu.instance != null)
        {
            PausedMenu.instance.BackFromHelp();
        }
        else
        {
            Time.timeScale = 1f; // Ensures the main menu does not remain paused
            SceneManager.LoadScene("MainMenu");
        }
    }
}