using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    [SerializeField] private StoveCounter _stoveCounter;
    [SerializeField] private float _warningSoundTimerMax = 0.2f;

    private AudioSource _audioSource;
    private float _warningSoundTimer;
    private bool _playWarningSound;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
        _stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
    }

    private void Update()
    {
        if (_playWarningSound)
        {
            _warningSoundTimer -= Time.deltaTime;

            if (_warningSoundTimer < 0)
            {
                _warningSoundTimer = _warningSoundTimerMax;

                SoundManager.Instance.PlayWarningSound(_stoveCounter.transform.position);
            }
        }
    }

    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        float burnShowProgressAmount = 0.5f;
        _playWarningSound = _stoveCounter.IsFried() && e.ProgressNormilized >= burnShowProgressAmount;
    }

    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
    {
        bool playSound = e.State == StoveCounter.State.Frying || e.State == StoveCounter.State.Fried;

        if (playSound)
            _audioSource.Play();
        else
            _audioSource.Pause();
    }
}