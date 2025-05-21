using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private string mainMenuSceneName = "MainMenu";
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Slider loadingBar;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        bool newPauseState = !GameManager.Instance.IsPaused();
        GameManager.Instance.SetPaused(newPauseState);
        pauseMenuPanel.SetActive(newPauseState);
    }

    public void ResumeGame()
    {
        GameManager.Instance.SetPaused(false);
        pauseMenuPanel.SetActive(false);
    }

    public void LoadMainMenuAsync()
    {
        GameManager.Instance.SetPaused(false);

        if (SceneLoader.Instance != null)
            SceneLoader.Instance.LoadScene(mainMenuSceneName);
        else
            SceneManager.LoadScene(mainMenuSceneName);
    }
    public void OnSettingsPressed()
    {
        pauseMenuPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void OnBackFromSettings()
    {
        settingsPanel.SetActive(false);
        pauseMenuPanel.SetActive(true);
    }
}