using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

// I used some AI assistance to help manage the separate help screen flow for help opened
// from the pause menu without confusing it with help opened from the main menu and to help with the comments.
public class PausedMenu : MonoBehaviour
{
    public static PausedMenu instance; // Lets other scripts check or control the active pause menu
    public GameObject pausePanel;
    private bool paused; // Keeps gameplay paused while the pause menu or pause menu help is open
    private bool changingScene; // Stops repeated clicks from starting overlapping scene transitions
    private int resumeFrame = -1; // Tracks the resume frame so gameplay input stays blocked until that frame ends
    private List<GameObject> hiddenObjects = new List<GameObject>(); // Remembers which gameplay objects to show again after returning from help
    public static bool InputBlocked => instance != null && (instance.paused || Time.frameCount <= instance.resumeFrame); // Blocks input while paused and during the frame Resume is pressed

    void Awake()
    {
        AudioListener.pause = false;
        instance = this;
        pausePanel.SetActive(false);
    }
    public void PauseGame()
    {
        AudioListener.pause = true;
        if (changingScene) return;
        paused = true;
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
    }
    public void ResumeGame()
    {
        AudioListener.pause = false;
        if (changingScene) return;
        resumeFrame = Time.frameCount; // Avoids player input restarting partway through the current frame
        paused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }
    public void OpenHelp()
    {
        if (changingScene || !paused) return;
        StartCoroutine(ShowHelp()); // Hides the paused game and opens help without resuming gameplay
    }
    private IEnumerator ShowHelp()
    {
        changingScene = true;
        hiddenObjects.Clear();
        // Hide the paused games objects but keep the pause menu object alive so it can handle the return from help
        foreach (GameObject obj in gameObject.scene.GetRootGameObjects())
        {
            if (obj != gameObject && obj.activeSelf)
            {
                hiddenObjects.Add(obj);
                obj.SetActive(false);
            }
        }
        yield return SceneManager.LoadSceneAsync("HelpScene", LoadSceneMode.Additive); // Shows HelpScene while keeping the paused gameplay scene loaded underneath
        changingScene = false; // Allows pause menu actions again once the help scene has finished opening
    }
    public void BackFromHelp()
    {
        if (changingScene) return;
        StartCoroutine(CloseHelp()); // Unloads help and returns to the still paused game 
    }
    private IEnumerator CloseHelp()
    {
        changingScene = true;
        paused = true; // Keeps gameplay blocked while the help scene is being unloaded
        Time.timeScale = 0f;
        yield return SceneManager.UnloadSceneAsync("HelpScene");
        // Restore the objects hidden before opening help the pause menu itself was left active
        foreach (GameObject obj in hiddenObjects)
        {
            if (obj != null)
                obj.SetActive(true);
        }
        hiddenObjects.Clear();
        paused = true;
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
        yield return null; // Lets the restored pause menu become active for a frame before accepting more actions
        paused = true;
        Time.timeScale = 0f;
        changingScene = false; // Re enables pause menu actions after the return transition is fully settled
    }
    public void QuitToMenu()
    {
        AudioListener.pause = false;
        if (changingScene) return;
        changingScene = true;
        Time.timeScale = 1f; // Prevents the main menu from inheriting the paused time scale 
        SceneManager.LoadScene("MainMenu");
    }
    void OnDestroy()
    {
        if (instance == this)
        {
            instance = null; // Clears the static reference when this pause menu object is destroyed 
        }
    }
}