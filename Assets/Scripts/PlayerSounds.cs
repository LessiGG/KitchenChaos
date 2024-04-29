using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField] private float _footStepsTimerMax = .1f;
    [SerializeField] private float _footstepsVolume = 1f;

    private Player _player;
    private float _footstepsTimer;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void Update()
    {
        _footstepsTimer -= Time.deltaTime;

        if (_footstepsTimer < 0f)
        {
            _footstepsTimer = _footStepsTimerMax;

            if (_player.IsWalking())
            {
                SoundManager.Instance.PlayFootstepsSound(_player.transform.position, _footstepsVolume);
            }
        }
    }
}