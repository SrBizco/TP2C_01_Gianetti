using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Paneles")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject controlsPanel;
    [SerializeField] private GameObject creditsPanel;

    [Header("Nombre de la escena del juego")]
    [SerializeField] private string gameplaySceneName = "Gameplay";

    
    public void OnPlayPressed()
    {
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

    
    public void OnExitPressed()
    {
        Application.Quit();
        Debug.Log("Salir del juego");
    }
}
