using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    private const string PlayerPrefsMusicVolume = "MusicVolume";

    public static MusicManager Instance { get; private set; }

    private AudioSource _audioSource;
    private float _volume = 0.3f;

    private void Awake()
    {
        Instance = this;

        _audioSource = GetComponent<AudioSource>();

        _volume = PlayerPrefs.GetFloat(PlayerPrefsMusicVolume, 1f);
        _audioSource.volume = _volume;
    }

    public void ChangeVolumeBySlider(float value)
    {
        _volume = value;
        _audioSource.volume = _volume;

        PlayerPrefs.SetFloat(PlayerPrefsMusicVolume, _volume);
        PlayerPrefs.Save();
    }

    public float GetVolume()
    {
        return _volume;
    }
}