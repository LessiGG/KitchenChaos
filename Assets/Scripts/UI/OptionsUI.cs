using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance { get; private set; }

    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _moveUpButton;
    [SerializeField] private Button _moveDownButton;
    [SerializeField] private Button _moveLeftButton;
    [SerializeField] private Button _moveRightButton;
    [SerializeField] private Button _interactButton;
    [SerializeField] private Button _interactAlternateButton;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _gamepadInteractButton;
    [SerializeField] private Button _gamepadInteractAlternateButton;
    [SerializeField] private Button _gamepadPauseButton;

    [SerializeField] private TextMeshProUGUI _moveUpText;
    [SerializeField] private TextMeshProUGUI _moveDownText;
    [SerializeField] private TextMeshProUGUI _moveLeftText;
    [SerializeField] private TextMeshProUGUI _moveRightText;
    [SerializeField] private TextMeshProUGUI _interactText;
    [SerializeField] private TextMeshProUGUI _interactAlternateText;
    [SerializeField] private TextMeshProUGUI _pauseText;
    [SerializeField] private TextMeshProUGUI _gamepadInteractText;
    [SerializeField] private TextMeshProUGUI _gamepadInteractAlternateText;
    [SerializeField] private TextMeshProUGUI _gamepadPauseText;
    [SerializeField] private TextMeshProUGUI _soundEffectsValueText;
    [SerializeField] private TextMeshProUGUI _musicValueText;

    [SerializeField] private Transform _pressToRebindKeyTransform;

    [SerializeField] private Slider _SoundEffectsSlider;
    [SerializeField] private Slider _musicSlider;

    private Action _onClosedButtonAction;

    private void Awake()
    {
        Instance = this;

        _closeButton.onClick.AddListener(() =>
        {
            Hide();
            _onClosedButtonAction();
        });

        _SoundEffectsSlider.onValueChanged.AddListener(value =>
        {
            SoundManager.Instance.ChangeVolumeBySlider(value);
            UpdateVisual();
        });

        _musicSlider.onValueChanged.AddListener(value =>
        {
            MusicManager.Instance.ChangeVolumeBySlider(value);
            UpdateVisual();
        });

        _moveUpButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.MoveUp); });
        _moveDownButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.MoveDown); });
        _moveLeftButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.MoveLeft); });
        _moveRightButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.MoveRight); });
        _interactButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Interact); });
        _interactAlternateButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.InteractAlternate); });
        _pauseButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Pause); });
        _gamepadInteractButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.GamepadInteract); });
        _gamepadInteractAlternateButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.GamepadInteractAlternate); });
        _gamepadPauseButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.GamepadPause); });
    }

    private void Start()
    {
        GameHandler.Instance.OnGameUnpaused += GameHandler_OnGameUnpaused;

        UpdateVisual();

        Hide();
    }

    private void GameHandler_OnGameUnpaused(object sender, System.EventArgs e)
    {
        Hide();
        HidePressToRebindKey();
    }

    private void UpdateVisual()
    {
        _soundEffectsValueText.text = Mathf.Round(SoundManager.Instance.GetVolume() * 100f).ToString();
        _SoundEffectsSlider.value = SoundManager.Instance.GetVolume();
        _musicValueText.text = Mathf.Round(MusicManager.Instance.GetVolume() * 100f).ToString();
        _musicSlider.value = MusicManager.Instance.GetVolume();

        _moveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveUp);
        _moveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveDown);
        _moveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveLeft);
        _moveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveRight);
        _interactText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        _interactAlternateText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlternate);
        _pauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
        _gamepadInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.GamepadInteract);
        _gamepadInteractAlternateText.text = GameInput.Instance.GetBindingText(GameInput.Binding.GamepadInteractAlternate);
        _gamepadPauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.GamepadPause);
    }

    public void Show(Action onClosedButtonAction)
    {
        _onClosedButtonAction = onClosedButtonAction;

        gameObject.SetActive(true);

        _closeButton.Select();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void ShowPressToRebindKey()
    {
        _pressToRebindKeyTransform.gameObject.SetActive(true);
    }

    private void HidePressToRebindKey()
    {
        _pressToRebindKeyTransform.gameObject.SetActive(false);
    }

    private void RebindBinding(GameInput.Binding binding)
    {
        ShowPressToRebindKey();
        GameInput.Instance.RebindBinding(binding, () =>
        {
            HidePressToRebindKey();
            UpdateVisual();
        });
    }

    private void Set()
    {

    }
}