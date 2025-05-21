using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;

    [Header("Pantalla de carga")]
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Slider loadingBar;
    [SerializeField] private GameObject pressAnyKeyText;

    private bool readyToActivate = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (loadingScreen != null)
            loadingScreen.SetActive(false);

        if (pressAnyKeyText != null)
            pressAnyKeyText.SetActive(false);
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        if (loadingScreen != null) loadingScreen.SetActive(true);
        if (pressAnyKeyText != null) pressAnyKeyText.SetActive(false);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        while (operation.progress < 0.9f)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            if (loadingBar != null)
                loadingBar.value = progress;

            yield return null;
        }

        if (loadingBar != null)
            loadingBar.value = 1f;

        yield return new WaitForSeconds(0.5f);

        if (pressAnyKeyText != null)
            pressAnyKeyText.SetActive(true);

        while (!Input.anyKeyDown)
            yield return null;

        operation.allowSceneActivation = true;

        yield return null;

        if (loadingScreen != null)
            loadingScreen.SetActive(false);

        if (pressAnyKeyText != null)
            pressAnyKeyText.SetActive(false);
    }
}