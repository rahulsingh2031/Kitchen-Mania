using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Player : MonoBehaviour, IKitchenObjectParent
{
    public static Player Instance { get; private set; }
    public event Action<BaseCounter> OnSelectedCounterChanged;
    public event Action OnPickupSomething;

    [SerializeField] private float moveSpeed;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask counterLayerMask;


    [SerializeField] private Transform kitchenObjectHoldPoint;
    private KitchenObject kitchenObject;


    private BaseCounter selectedCounter;
    private bool _isWalking;
    private Vector3 _lastInteractDir;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        gameInput.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
    }
    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {

        if (!GameManager.Instance.IsGamePlaying()) return;
        if (selectedCounter != null)
        {
            selectedCounter.Interact(this);

        }
    }
    private void GameInput_OnInteractAlternateAction(object sender, EventArgs e)
    {
        if (!GameManager.Instance.IsGamePlaying()) return;
        if (selectedCounter != null)
        {
            selectedCounter.InteractAlternate(this);
        }
    }
    private void Update()
    {//TODO: Refactor the movement and rotation code in a seperate method
        HandleMovement();
        HandleInteraction();
    }

    private void HandleInteraction()
    {
        Vector2 _inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 _moveDir = new Vector3(_inputVector.x, 0f, _inputVector.y);

        if (_moveDir != Vector3.zero)
        {
            _lastInteractDir = _moveDir;
        }

        float _interactDistance = 2f;
        if (Physics.Raycast(transform.position, _lastInteractDir, out RaycastHit hit, _interactDistance, counterLayerMask))
        {

            if (hit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                SetSelectedCounter(baseCounter);
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
    private void HandleMovement()
    {
        Vector2 _inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 _moveDir = new Vector3(_inputVector.x, 0f, _inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = 0.7f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, _moveDir, moveDistance);
        if (!canMove)
        {   //since we can't move ,we check if there is any x,y component direction on which we can move on.


            //checking if we can move in X-Direction
            Vector3 _moveDirX = new Vector3(_moveDir.x, 0f, 0f).normalized;

            //moveDir.x != 0 is for a minor visual bug where player not to Z only moveDir 
            canMove = _moveDir.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, _moveDirX, moveDistance);
            if (canMove)
            {
                //Can move only on the X
                _moveDir = _moveDirX;
            }
            else
            {
                //checking if we can move in Y-Direction, if X-Direction is blocked

                Vector3 _moveDirZ = new Vector3(0, 0, _moveDir.z).normalized;
                canMove = _moveDir.z != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, _moveDirZ, moveDistance);
                if (canMove)
                {   //can only move on the z
                    _moveDir = _moveDirZ;
                }
                else
                {
                    //cannot move in any direction
                }
            }
        }
        if (canMove)
        {
            transform.position += _moveDir * moveDistance;
        }

        _isWalking = _moveDir != Vector3.zero;
        float _rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, _moveDir, Time.deltaTime * _rotateSpeed);
    }

    public bool IsWalking()
    {
        return _isWalking;
    }

    private void SetSelectedCounter(BaseCounter baseCounter)
    {
        this.selectedCounter = baseCounter;
        OnSelectedCounterChanged?.Invoke(selectedCounter);
    }


    public Transform GetKitchenObjectFollowTransform()
    {
        return kitchenObjectHoldPoint;
    }
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        OnPickupSomething?.Invoke();
        this.kitchenObject = kitchenObject;
    }
    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }
    public void ClearKitchenObject()
    {
        this.kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
