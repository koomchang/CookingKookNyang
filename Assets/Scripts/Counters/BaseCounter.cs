using UnityEngine;
using System;
using Unity.Netcode;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent {

	public static event EventHandler OnAnyObjectPlacedHere;

	public static void ResetStaticData() {
		OnAnyObjectPlacedHere = null;
	}
	
	[SerializeField] private Transform counterTopPoint;

	private KitchenObject kitchenObject;

	public Transform GetKitchenObjectFollowTransform() {
		return counterTopPoint;
	}

	public void SetKitchenObject(KitchenObject kitchenObject) {
		this.kitchenObject = kitchenObject;

		if (kitchenObject != null) {
			OnAnyObjectPlacedHere?.Invoke(this, EventArgs.Empty);
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

	public virtual void Interact(Player player) {
		Debug.Log("BaseCounter Interact();");
	}

	public virtual void InteractAlternate(Player player) {
		// Debug.Log("BaseCounter InteractAlternate();");
	}
	
	public NetworkObject GetNetworkObject() {
		return null;
	}
}