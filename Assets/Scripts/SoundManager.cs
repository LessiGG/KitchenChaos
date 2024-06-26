using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private const string PlayerPrefsSoundEffectsVolume = "SoundEffectsVolume";

    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioClipRefsSO _audioClipRefsSO;

    private float _volume = 1f;

    private void Awake()
    {
        Instance = this;

        _volume = PlayerPrefs.GetFloat(PlayerPrefsSoundEffectsVolume, 1f);
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.Instance.OnPickedSomething += Player_OnPickedSomething;
        BaseCounter.OnAnyObjectDroppedHere += BaseCounter_OnAnyObjectPlacedHere;
        TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;
    }

    private void TrashCounter_OnAnyObjectTrashed(object sender, System.EventArgs e)
    {
        TrashCounter trashCounter = sender as TrashCounter;

        PlaySound(_audioClipRefsSO.Trash, trashCounter.transform.position);
    }

    private void BaseCounter_OnAnyObjectPlacedHere(object sender, System.EventArgs e)
    {
        BaseCounter baseCounter = sender as BaseCounter;

        PlaySound(_audioClipRefsSO.ObjectDrop, baseCounter.transform.position);
    }

    private void Player_OnPickedSomething(object sender, System.EventArgs e)
    {
        PlaySound(_audioClipRefsSO.ObjectPickUp, Player.Instance.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;

        PlaySound(_audioClipRefsSO.Chop, cuttingCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;

        PlaySound(_audioClipRefsSO.DeliverySuccess, deliveryCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeFailed(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;

        PlaySound(_audioClipRefsSO.DeliveryFail, deliveryCounter.transform.position);
    }

    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f)
    {
        PlaySound(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volume);
    }
    private void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplier = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplier * _volume);
    }

    public void PlayFootstepsSound(Vector3 position, float volume)
    {
        PlaySound(_audioClipRefsSO.Footsteps, position, volume);
    }

    public void PlayCountdownSound()
    {
        PlaySound(_audioClipRefsSO.Warning, Vector3.zero);
    }

    public void PlayWarningSound(Vector3 position)
    {
        PlaySound(_audioClipRefsSO.Warning, position);
    }

    public void ChangeVolumeBySlider(float value)
    {
        _volume = value;

        PlayerPrefs.SetFloat(PlayerPrefsSoundEffectsVolume, _volume);
        PlayerPrefs.Save();
    }

    public float GetVolume()
    {
        return _volume;
    }
}