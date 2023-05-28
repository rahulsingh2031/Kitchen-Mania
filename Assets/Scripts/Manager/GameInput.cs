using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }
    private PlayerActionInput playerActionInput;
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    public event Action OnPauseAction;
    private void Awake()
    {
        Instance = this;
        playerActionInput = new PlayerActionInput();
        playerActionInput.Enable();

        playerActionInput.Player.Interact.performed += Interact_performed;
        playerActionInput.Player.InteractAlternate.performed += InteractAlternate_performed;
        playerActionInput.Player.Pause.performed += Pause_performed;
    }


    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {

        OnPauseAction?.Invoke();
    }
    private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {

        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }
    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }
    public Vector2 GetMovementVectorNormalized()
    {

        Vector2 _inputVector = playerActionInput.Player.Move.ReadValue<Vector2>();

        _inputVector = _inputVector.normalized;

        return _inputVector;
    }
}
