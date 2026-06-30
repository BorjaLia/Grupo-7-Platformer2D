using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("UI Panels")]
    [SerializeField] private GameObject pauseBackground;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject settingsPanel;

    private const string _menuScene = "MainMenu";

    public static event Action<bool> OnPauseToggled;

    private static bool _isPaused = false;
    private static bool _gameEnded = false;

    private void Start()
    {
        if (pauseBackground) pauseBackground.SetActive(false);
        if (pausePanel) pausePanel.SetActive(false);
        if (settingsPanel) settingsPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !_gameEnded)
        {
            Debug.Log("Pause");
            TogglePause();
            HandlePauseStateChanged(_isPaused);
        }
    }

    private static void TogglePause()
    {
        _isPaused = !_isPaused;

        Time.timeScale = _isPaused ? 0.0f : 1.0f;

        OnPauseToggled?.Invoke(_isPaused);
    }

    private void HandlePauseStateChanged(bool isPaused)
    {
        if (pauseBackground)
        {
            pauseBackground.SetActive(isPaused);
        }
        if (pausePanel)
        {
            pausePanel.SetActive(isPaused && (!settingsPanel || !settingsPanel.activeSelf));
        }
    }

    public void RetryGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ResumeGame()
    {
        TogglePause();
        HandlePauseStateChanged(_isPaused);
    }

    public void OpenSettings()
    {
        if (pausePanel) pausePanel.SetActive(false);
        if (settingsPanel) settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        if (settingsPanel) settingsPanel.SetActive(false);
        if (pausePanel) pausePanel.SetActive(true);
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene(_menuScene);
    }

    public static void EndGame()
    {
        TogglePause();
        _gameEnded = true;
    }
}
