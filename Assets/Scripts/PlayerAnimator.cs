using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Player _player;

    private Animator _animator;

    private const string IsWalking = "IsWalking";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _animator.SetBool(IsWalking, _player.IsWalking());
    }
}