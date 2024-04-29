using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private GameObject _hasProgressGameObject;
    [SerializeField] private Image _barImage;

    private IHasProgress _hasProgress;

    private void Start()
    {
        _hasProgress = _hasProgressGameObject.GetComponent<IHasProgress>();

        if (_hasProgress == null)
        {
            Debug.LogError("GameObject " + _hasProgressGameObject + " does not have component that implements IHasProgress!");
        }

        _hasProgress.OnProgressChanged += HasProgress_OnProgressChanged;

        _barImage.fillAmount = 0;
        Hide();
    }

    private void HasProgress_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        _barImage.fillAmount = e.ProgressNormilized;

        if (e.ProgressNormilized == 0f || e.ProgressNormilized == 1f)
            Hide();
        else
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