using UnityEngine;

[RequireComponent (typeof(Animator))]
public class StoveBurnFlashingBarUI : MonoBehaviour
{
    private const string IsFlashing = "IsFlashing";

    [SerializeField] private StoveCounter _stoveCounter;
    [SerializeField] private float _burnShowProgressAmount = .5f;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;

        _animator.SetBool(IsFlashing, false);
    }

    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        bool show = _stoveCounter.IsFried() && e.ProgressNormilized >= _burnShowProgressAmount;

        _animator.SetBool(IsFlashing, show);
    }
}