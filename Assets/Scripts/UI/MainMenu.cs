using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Paneles")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject controlsPanel;
    [SerializeField] private GameObject creditsPanel;
    [SerializeField] private GameObject settingsPanel;

    [Header("Nombre de la escena del juego")]
    [SerializeField] private string gameplaySceneName = "Gameplay";

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void OnPlayPressed()
    {
        if (SceneLoader.Instance != null)
            SceneLoader.Instance.LoadScene(gameplaySceneName);
        else
            SceneManager.LoadScene(gameplaySceneName);
    }

    public void OnControlsPressed()
    {
        mainMenuPanel.SetActive(false);
        controlsPanel.SetActive(true);
    }

    public void OnCreditsPressed()
    {
        mainMenuPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }

    public void OnBackToMenuPressed()
    {
        controlsPanel.SetActive(false);
        creditsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
    public void OnSettingsPressed()
    {
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void OnBackFromSettings()
    {
        settingsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void OnExitPressed()
    {
        Application.Quit();
        Debug.Log("Salir del juego");
    }
}
