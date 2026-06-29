using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("UI Panels")]
    [SerializeField] private GameObject _mainPanel;
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private GameObject _creditsPanel;

    private const string _gameplayScene = "Map Test";

    private void Start()
    {
        if (_mainPanel) _mainPanel.SetActive(true);
        if (_settingsPanel) _settingsPanel.SetActive(false);
        if (_creditsPanel) _creditsPanel.SetActive(false);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(_gameplayScene);
    }

    public void OpenSettings()
    {
        if (_mainPanel) _mainPanel.SetActive(false);
        if (_settingsPanel) _settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        if (_mainPanel) _mainPanel.SetActive(true);
        if (_settingsPanel) _settingsPanel.SetActive(false);
    }

    public void OpenCredits()
    {
        if (_mainPanel) _mainPanel.SetActive(false);
        if (_creditsPanel) _creditsPanel.SetActive(true);
    }

    public void CloseCredits()
    {
        if (_mainPanel) _mainPanel.SetActive(true);
        if (_creditsPanel) _creditsPanel.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}