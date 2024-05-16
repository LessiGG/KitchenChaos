using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _mainMenuButton;

    private void Awake()
    {
        _resumeButton.onClick.AddListener(() => 
        {
            GameHandler.Instance.TogglePauseGame();
        });
        _mainMenuButton.onClick.AddListener(() => 
        {
            Loader.Load(Loader.Scene.MainMenuScene);
        });
    }

    private void Start()
    {
        GameHandler.Instance.OnGamePaused += GameHandler_OnGamePaused;
        GameHandler.Instance.OnGameUnpaused += GameHandler_OnGameUnpaused;

        Hide();
    }

    private void GameHandler_OnGameUnpaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void GameHandler_OnGamePaused(object sender, System.EventArgs e)
    {
        Show();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}