using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class PausedMenu : MonoBehaviour
{
    public static PausedMenu instance;
    public GameObject pausePanel;
    private bool paused;
    private bool changingScene;
    private int resumeFrame = -1;
    private List<GameObject> hiddenObjects = new List<GameObject>();
    public static bool InputBlocked => instance != null && (instance.paused || Time.frameCount <= instance.resumeFrame);

    void Awake()
    {
        instance = this;
        pausePanel.SetActive(false);
    }
    public void PauseGame()
    {
        if (changingScene) return;
        paused = true;
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
    }
    public void ResumeGame()
    {
        if (changingScene) return;
        resumeFrame = Time.frameCount;
        paused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }
    public void OpenHelp()
    {
        if (changingScene || !paused) return;
        StartCoroutine(ShowHelp());
    }
    private IEnumerator ShowHelp()
    {
        changingScene = true;
        hiddenObjects.Clear();
        foreach (GameObject obj in gameObject.scene.GetRootGameObjects())
        {
            if (obj != gameObject && obj.activeSelf)
            {
                hiddenObjects.Add(obj);
                obj.SetActive(false);
            }
        }
        yield return SceneManager.LoadSceneAsync("HelpScene", LoadSceneMode.Additive);
        changingScene = false;
    }
    public void BackFromHelp()
    {
        if (changingScene) return;
        StartCoroutine(CloseHelp());
    }
    private IEnumerator CloseHelp()
    {
        changingScene = true;
        paused = true;
        Time.timeScale = 0f;
        yield return SceneManager.UnloadSceneAsync("HelpScene");
        foreach (GameObject obj in hiddenObjects)
        {
            if (obj != null)
                obj.SetActive(true);
        }
        hiddenObjects.Clear();
        paused = true;
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
        yield return null;
        paused = true;
        Time.timeScale = 0f;
        changingScene = false;
    }
    public void QuitToMenu()
    {
        if (changingScene) return;
        changingScene = true;
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
    void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }
}