using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour {
	private PlayerInputActions playerInputActions;

	private void Awake() {
		playerInputActions = new PlayerInputActions();
		playerInputActions.Player.Enable();

		playerInputActions.Player.Interact.performed += Interact_performed;
		// 설정한 키가 입력되었을 때에만 상호작용
		playerInputActions.Player.InteractAlternate.performed += InteractAlternate_performed;
	}

	public event EventHandler OnInteractAction;
	public event EventHandler OnInteractAlternateAction;

	private void InteractAlternate_performed(InputAction.CallbackContext obj) {
		OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
	}


	private void Interact_performed(InputAction.CallbackContext obj) {
		OnInteractAction?.Invoke(this, EventArgs.Empty); // Not null 이라면 실행
	}

	public Vector2 GetMovementVectorNormalized() {
		var inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

		inputVector = inputVector.normalized;

		return inputVector;
	}
}