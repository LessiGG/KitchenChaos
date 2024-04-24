using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 7f;
    [SerializeField] private float _rotateSpeed = 10f;

    [SerializeField] private GameInput _gameInput;

    private bool _isWalking;

    private void Update()
    {
        Vector2 inputVector = _gameInput.GetMovementVectorNormilized();

        Vector3 moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);

        _isWalking = moveDirection != Vector3.zero;

        transform.position += moveDirection * _moveSpeed * Time.deltaTime;
        transform.forward = Vector3.Slerp(transform.forward, moveDirection, _rotateSpeed * Time.deltaTime);
    }

    public bool IsWalking()
    {
        return _isWalking;
    }
}