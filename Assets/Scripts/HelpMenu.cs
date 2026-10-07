using UnityEngine;
using UnityEngine.SceneManagement;
public class HelpMenu : MonoBehaviour
{
    public void BackToMainMenu()
    {
        if (PausedMenu.instance != null)
        {
            PausedMenu.instance.BackFromHelp();
        }
        else
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("MainMenu");
        }
    }
}