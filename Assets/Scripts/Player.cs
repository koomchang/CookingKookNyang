using System;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent {
	
	public static Player Instance { get; private set; } // singleton in Unity

	public event EventHandler OnPickedSomething;

	[SerializeField] private float moveSpeed = 7f;
	[SerializeField] private GameInput gameInput;
	[SerializeField] private LayerMask countersLayerMask;
	[SerializeField] private Transform kitchenObjectHoldPoint;

	private bool isWalking;
	private KitchenObject kitchenObject;
	private Vector3 lastInteractDir; // game object 를 향하여 계속 이동하지 않아도 game object 와 상호작용할 수 있기 위해 만든 변수
	private BaseCounter selectedCounter;
	
	private void Awake() {
		if (Instance != null) Debug.LogError("There is more than one Player instance");

		Instance = this;
	}

	private void Start() {
		gameInput.OnInteractAction += GameInput_OnInteractAction; // 미리 설정한 키를 입력한다면 상호작용
		gameInput.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
	}

	private void Update() {
		HandleMovement();
		HandleInteractions();
	}

	public Transform GetKitchenObjectFollowTransform() {
		return kitchenObjectHoldPoint;
	}

	public void SetKitchenObject(KitchenObject kitchenObject) {
		this.kitchenObject = kitchenObject;

		if (kitchenObject != null) {
			OnPickedSomething?.Invoke(this, EventArgs.Empty);
		}
	}

	public KitchenObject GetKitchenObject() {
		return kitchenObject;
	}

	public void ClearKitchenObject() {
		kitchenObject = null;
	}

	public bool HasKitchenObject() {
		return kitchenObject != null;
		// 오브젝트 존재한다면 true 반환
		// 오브젝트 존재하지 않다면 false 반환
	}

	private void GameInput_OnInteractAlternateAction(object sender, EventArgs e) {
		if (!KitchenGameManager.Instance.IsGamePlaying()) return;
		if (selectedCounter != null) selectedCounter.InteractAlternate(this);
	}

	private void GameInput_OnInteractAction(object sender, EventArgs e) {
		if (selectedCounter != null) selectedCounter.Interact(this);
	}

	public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;


	public bool IsWalking() {
		return isWalking;
	}

	private void HandleInteractions() {
		var inputVector = gameInput.GetMovementVectorNormalized();

		var moveDir = new Vector3(inputVector.x, 0f, inputVector.y); // x, y, z 축  

		if (moveDir != Vector3.zero)
			// 플레이어가 움직인 경우
			lastInteractDir = moveDir; // 해당 방향 벡터를 변수에 대입

		float interactDistance = 2f;
		if (Physics.Raycast(transform.position, lastInteractDir, out var raycastHit, interactDistance,
			    countersLayerMask)) {
			// 충돌이 존재하는 경우

			// RaycastHit : raycast(레이저빔) 로부터 되돌아온 정보를 저장하고 얻기 위해 사용되는 구조체
			// lastInteractDir : Player 가 마지막으로 움직인 것을 기준으로 raycast 계산

			if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter)) {
				// clearCounter 과 충돌하여 그 정보가 존재한다면
				if (baseCounter != selectedCounter) SetSelectedCounter(baseCounter);
			}
			else {
				SetSelectedCounter(null);
			}
		}
		else {
			SetSelectedCounter(null);
		}
	}

	private void HandleMovement() {
		var inputVector = gameInput.GetMovementVectorNormalized();

		var moveDir = new Vector3(inputVector.x, 0f, inputVector.y); // x, y, z 축

		float moveDistance = moveSpeed * Time.deltaTime;
		float playerRadius = 0.9f;
		float playerHeight = 2f;
		bool canMove = !Physics.CapsuleCast(transform.position,
			transform.position + Vector3.up * playerHeight,
			playerRadius,
			moveDir,
			moveDistance);

		if (!canMove) {
			// 동시에 2가지 방향(x,z)으로 갈 때 하나의 방향에서 충돌이 발생하여 나머지 하나의 방향으로도 갈 수 없을 때
			// 일단 x 방향으로 가도록 설정
			var moveDirX = new Vector3(moveDir.x, 0, 0).normalized; // 정규화하여 대각과 정각의 속도를 일정하게 설정
			canMove = moveDir.x != 0 && !Physics.CapsuleCast(transform.position,
				transform.position + Vector3.up * playerHeight,
				playerRadius,
				moveDirX,
				moveDistance);

			if (canMove) {
				// x 방향으로 가는 것이 가능하다면 x 방향으로 가도록 설정
				moveDir = moveDirX;
			}
			else {
				// x 방향으로 가는 것이 불가능하다면 z 방향으로 가도록 설정
				var moveDirZ = new Vector3(0, 0, moveDir.z).normalized; // 정규화하여 대각과 정각의 속도를 일정하게 설정
				canMove = moveDir.z != 0 && !Physics.CapsuleCast(transform.position,
					transform.position + Vector3.up * playerHeight,
					playerRadius,
					moveDirZ,
					moveDistance);

				if (canMove)
					// z 방향으로만 가도록 설정
					moveDir = moveDirZ;
				// x, z 모든 방향으로 가는 것이 불가능
			}
		}

		if (canMove) transform.position += moveDir * moveDistance;

		isWalking = moveDir != Vector3.zero;

		float rotateSpeed = 10f; // 방향전환 스피드
		transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
	}

	private void SetSelectedCounter(BaseCounter selectedCounter) {
		this.selectedCounter = selectedCounter;

		OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs {
			selectedCounter = selectedCounter
		});
	}

	public class OnSelectedCounterChangedEventArgs : EventArgs {
		public BaseCounter selectedCounter;
	}
}