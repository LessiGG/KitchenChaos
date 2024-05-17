using TMPro;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class GameStartCountdownUI : MonoBehaviour
{
    private const string NumberPopUp = "NumberPopUp";

    [SerializeField] private TextMeshProUGUI _countdownText;

    private Animator _animator;
    private int _previousCountdownNumber;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        GameHandler.Instance.OnStateChanged += GameHandler_OnStateChanged;

        Hide();
    }

    private void Update()
    {
        int countdownNumber = Mathf.CeilToInt(GameHandler.Instance.GetCountdownToStartTimer());
        _countdownText.text = countdownNumber.ToString();

        if (_previousCountdownNumber != countdownNumber)
        {
            _previousCountdownNumber = countdownNumber;
            _animator.SetTrigger(NumberPopUp);
            SoundManager.Instance.PlayCountdownSound();
        }
    }

    private void GameHandler_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameHandler.Instance.IsCountdownToStartActive())
            Show();
        else
            Hide();
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