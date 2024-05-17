using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class DeliveryResultUI : MonoBehaviour
{
    private const string PopUp = "PopUp";

    [SerializeField] private Image _backgroundImage;
    [SerializeField] private Image _iconImage;
    [SerializeField] private TextMeshProUGUI _messageText;
    [SerializeField] private Color _successColor;
    [SerializeField] private Color _failColor;
    [SerializeField] private Sprite _successSprite;
    [SerializeField] private Sprite _failSprite;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>(); 
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;

        gameObject.SetActive(false);
    }

    private void DeliveryManager_OnRecipeFailed(object sender, System.EventArgs e)
    {
        gameObject.SetActive(true);
        _animator.SetTrigger(PopUp);
        _backgroundImage.color = _failColor;
        _iconImage.sprite = _failSprite;
        _messageText.text = "DELIVERY\nFAILED";
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, System.EventArgs e)
    {
        gameObject.SetActive(true);
        _animator.SetTrigger(PopUp);
        _backgroundImage.color = _successColor;
        _iconImage.sprite = _successSprite;
        _messageText.text = "DELIVERY\nSUCCESS";
    }
}