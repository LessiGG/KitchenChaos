using System;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public static GameHandler Instance { get; private set; }

    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;

    [SerializeField] private float _waitingToStartTimer = 1f;
    [SerializeField] private float _countdownToStartTimer = 3f;
    [SerializeField] private float _gamePlayingTimerMax = 10f;

    private enum State
    {
        WaitingToStart, 
        CountdownToStart,
        GamePlaying,
        GameOver
    }

    private State _currentState;
    private float _gamePlayingTimer;
    private bool _isGamePaused = false;

    private void Awake()
    {
        Instance = this;

        _currentState = State.WaitingToStart;
    }

    private void Start()
    {
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
    }

    private void GameInput_OnPauseAction(object sender, EventArgs e)
    {
        TogglePauseGame();
    }

    private void Update()
    {
        switch (_currentState)
        {
            case State.WaitingToStart:
                _waitingToStartTimer -= Time.deltaTime;

                if (_waitingToStartTimer < 0f)
                {
                    _currentState = State.CountdownToStart;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.CountdownToStart:
                _countdownToStartTimer -= Time.deltaTime;

                if (_countdownToStartTimer < 0f)
                {
                    _currentState = State.GamePlaying;
                    _gamePlayingTimer = _gamePlayingTimerMax;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GamePlaying:
                _gamePlayingTimer -= Time.deltaTime;

                if (_gamePlayingTimer < 0f)
                {
                    _currentState = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameOver:
                break;
        }
    }

    public void TogglePauseGame()
    {
        _isGamePaused = !_isGamePaused;

        if (_isGamePaused)
        {
            Time.timeScale = 0f;
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1f;
            OnGameUnpaused?.Invoke(this, EventArgs.Empty);
        }
    }

    public bool IsGamePlaying()
    {
        return _currentState == State.GamePlaying;
    }

    public bool IsCountdownToStartActive()
    {
        return _currentState == State.CountdownToStart;
    }

    public float GetCountdownToStartTimer()
    {
        return _countdownToStartTimer;
    }

    public bool IsGameOver()
    {
        return _currentState == State.GameOver;
    }

    public float GetGamePlayingTimerNormilized()
    {
        return 1 - (_gamePlayingTimer / _gamePlayingTimerMax);
    }
}