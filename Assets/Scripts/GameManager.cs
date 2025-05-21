using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private int score = 0;
    private int totalBadCivilians = 0;
    private int remainingBadCivilians = 0;
    private DroneHealth playerHealth;

    public GameState CurrentState { get; private set; } = GameState.Playing;

    [Header("Pantallas de fin de juego")]
    [SerializeField] private GameObject victoryScreen;
    [SerializeField] private GameObject defeatScreen;

    [Header("Nombre de la escena del menú")]
    [SerializeField] private string mainMenuSceneName = "MainMenu";

    private bool victoryShown = false;
    private bool defeatShown = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        victoryShown = false;
        defeatShown = false;

        if (victoryScreen != null)
            victoryScreen.SetActive(false);

        if (defeatScreen != null)
            defeatScreen.SetActive(false);

        SetPaused(false);

        // Reset stats
        totalBadCivilians = 0;
        remainingBadCivilians = 0;

        playerHealth = FindObjectOfType<DroneHealth>();
    }

    private void Update()
    {
        CheckVictoryCondition();
        CheckDefeatCondition();
    }

    private void CheckVictoryCondition()
    {
        if (victoryShown || defeatShown) return;

        if (totalBadCivilians == 0) return; // aún no se han registrado civiles malos

        if (remainingBadCivilians <= 0)
        {
            ShowVictoryScreen();
        }
    }

    private void CheckDefeatCondition()
    {
        if (victoryShown || defeatShown) return;

        if (playerHealth != null && playerHealth.CurrentHealth <= 0)
        {
            ShowDefeatScreen();
        }
    }

    private void ShowVictoryScreen()
    {
        victoryShown = true;
        SetPaused(true);
        if (victoryScreen != null)
            victoryScreen.SetActive(true);
    }

    private void ShowDefeatScreen()
    {
        defeatShown = true;
        SetPaused(true);
        if (defeatScreen != null)
            defeatScreen.SetActive(true);
    }

    public void RegisterBadCivilian()
    {
        totalBadCivilians++;
        remainingBadCivilians++;
        Debug.Log($"Registrado nuevo BadCivilian. Total: {totalBadCivilians}");
    }

    public void EliminateBadCivilian()
    {
        remainingBadCivilians--;
        Debug.Log($"Eliminado un bad civilian. Restantes: {remainingBadCivilians}");
    }

    public void IncreaseScore()
    {
        score++;
        Debug.Log("Puntos por enemigo eliminado: " + score);
    }

    public void DecreaseScore()
    {
        score--;
        Debug.Log("Puntos por civil bueno eliminado: " + score);
    }

    public void PlayerDied()
    {
        Debug.Log("¡Has muerto! La partida ha terminado.");
    }

    public void SetPaused(bool isPaused)
    {
        CurrentState = isPaused ? GameState.Paused : GameState.Playing;
        Time.timeScale = isPaused ? 0f : 1f;

        Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isPaused;
    }

    public bool IsPaused()
    {
        return CurrentState == GameState.Paused;
    }

    public void RestartGame()
    {
        SetPaused(false);

        if (SceneLoader.Instance == null)
            Debug.LogWarning("SceneLoader.Instance es NULL en RestartGame");

        SceneLoader.Instance?.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToMainMenu()
    {
        SetPaused(false);

        if (SceneLoader.Instance == null)
            Debug.LogWarning("SceneLoader.Instance es NULL en RestartGame");

        SceneLoader.Instance?.LoadScene(mainMenuSceneName);
    }
}
