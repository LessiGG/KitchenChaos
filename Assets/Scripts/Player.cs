using System;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    public static Player Instance { get; private set; }

    public event EventHandler OnPickedSomething;

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs 
    {
        public BaseCounter SelectedCounter;
    }

    [SerializeField] private float _moveSpeed = 7f;
    [SerializeField] private float _rotateSpeed = 10f;
    [SerializeField] private float _interactDistance = 2f;
    [Space]
    [SerializeField] private GameInput _gameInput;
    [SerializeField] private LayerMask _countersLayerMask;
    [SerializeField] private Transform _kitchenObjectHoldPoint;

    private const float PlayerRadius = 0.7f;
    private const float PlayerHeight = 2f;

    private bool _isWalking;
    private Vector3 _lastInteractDirection;
    private BaseCounter _selectedCounter;
    private KitchenObject _kitchenObject;

    private void Awake()
    {
        if (Instance != null)
            Debug.LogError("There is more than one Player instance");

        Instance = this;
    }

    private void Start()
    {
        _gameInput.OnInteractAction += GameInput_OnInteractAction;
        _gameInput.OnInteractAlternateAction += _gameInput_OnInteractAlternateAction;
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if (GameHandler.Instance.IsGamePlaying() == false)
            return;

        _selectedCounter?.Interact(this);
    }

    private void _gameInput_OnInteractAlternateAction(object sender, EventArgs e)
    {
        if (GameHandler.Instance.IsGamePlaying() == false)
            return;

        _selectedCounter?.InteractAlternate(this);
    }

    private void Update()
    {
        HandleMovemenent();
        HandleInteractions();
    }

    public bool IsWalking()
    {
        return _isWalking;
    }

    private void HandleInteractions()
    {
        Vector2 inputVector = _gameInput.GetMovementVectorNormilized();
        Vector3 moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDirection !=  Vector3.zero)
            _lastInteractDirection = moveDirection;

        if (Physics.Raycast(transform.position, _lastInteractDirection, out RaycastHit raycastHit, _interactDistance, _countersLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter)) 
            {
                if (baseCounter != _selectedCounter)
                {
                    SetSelectedCounter(baseCounter);
                }
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }
    }

    private void HandleMovemenent()
    {
        Vector2 inputVector = _gameInput.GetMovementVectorNormilized();
        Vector3 moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance = _moveSpeed * Time.deltaTime;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * PlayerHeight, PlayerRadius, moveDirection, moveDistance);

        if (!canMove)
        {
            Vector3 moveDirectionX = new Vector3(moveDirection.x, 0, 0).normalized;
            canMove = (moveDirection.x < -0.5f || moveDirection.x > 0.5f) && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * PlayerHeight, PlayerRadius, moveDirectionX, moveDistance);

            if (canMove)
            {
                moveDirection = moveDirectionX;
            }
            else
            {
                Vector3 moveDirectionZ = new Vector3(0, 0, moveDirection.z).normalized;
                canMove = (moveDirection.z < -0.5f || moveDirection.z > 0.5f) && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * PlayerHeight, PlayerRadius, moveDirectionZ, moveDistance);

                if (canMove)
                {
                    moveDirection = moveDirectionZ;
                }
            }
        }

        if (canMove)
            transform.position += moveDirection * moveDistance;

        _isWalking = moveDirection != Vector3.zero;
        transform.forward = Vector3.Slerp(transform.forward, moveDirection, _rotateSpeed * Time.deltaTime);
    }

    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        _selectedCounter = selectedCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs { SelectedCounter = _selectedCounter });
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return _kitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        _kitchenObject = kitchenObject;

        if (kitchenObject != null)
            OnPickedSomething?.Invoke(this, EventArgs.Empty);
    }

    public KitchenObject GetKitchenObject()
    {
        return _kitchenObject;
    }

    public void ClearKitchenObject()
    {
        _kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return _kitchenObject != null;
    }
}