using System;
using Unity.Netcode;
using UnityEngine;

public class KitchenObject : NetworkBehaviour {
	[SerializeField] private KitchenObjectSO kitchenObjectSO;

	private IKitchenObjectParent kitchenObjectParent;
	private FollowTransform followTransform;

	protected virtual void Awake() {
		followTransform = GetComponent<FollowTransform>();
	}

	public KitchenObjectSO GetKitchenObjectSO() {
		return kitchenObjectSO;
	}

	public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent) {
		SetKitchenObjectParentServerRpc(kitchenObjectParent.GetNetworkObject());
	}

	[ServerRpc(RequireOwnership = false)]
	private void SetKitchenObjectParentServerRpc(NetworkObjectReference kitchenObjectParentNetworkObjectReference) {
		SetKitchenObjectParentClientRpc(kitchenObjectParentNetworkObjectReference);
	}
	
	[ClientRpc]
	private void SetKitchenObjectParentClientRpc(NetworkObjectReference kitchenObjectParentNetworkObjectReference) {
		kitchenObjectParentNetworkObjectReference.TryGet(out NetworkObject kitchenObjectParentNetworkObject);
		IKitchenObjectParent kitchenObjectParent = kitchenObjectParentNetworkObject.GetComponent<IKitchenObjectParent>();
		
		if (this.kitchenObjectParent != null)
			// 이전 카운터가 존재한다면
			this.kitchenObjectParent.ClearKitchenObject(); // 이전 카운터에서 오브젝트 제거

		this.kitchenObjectParent = kitchenObjectParent; // 이전 카운터를 새로운 카운터로 대체

		if (kitchenObjectParent.HasKitchenObject())
			// (그럴리 없겠지만) 만약 새로운 카운터에 오브젝트 있다면 디버깅
			Debug.LogError("IKitchenObjectParent already has a KitchenObject!");

		kitchenObjectParent.SetKitchenObject(this);
		
		followTransform.SetTargetTransform(kitchenObjectParent.GetKitchenObjectFollowTransform());
	}

	public IKitchenObjectParent GetKitchenObjectParent() {
		return kitchenObjectParent;
	}

	public void DestroySelf() {
		Destroy(gameObject);
	}
	
	public void ClearKitchenObjectOnParent() {
		kitchenObjectParent.ClearKitchenObject();
	}

	public bool TryGetPlate(out PlateKitchenObject plateKitchenObject) {
		if (this is PlateKitchenObject) {
			plateKitchenObject = this as PlateKitchenObject;
			return true;
		}
		else {
			plateKitchenObject = null;
			return false;
		}
	}

	public static void SpawnKitchenObject(KitchenObjectSO kitchenObjectSo, IKitchenObjectParent kitchenObjectParent) {
		KitchenGameMultiplayer.Instance.SpawnKitchenObject(kitchenObjectSo, kitchenObjectParent);
	}
	
	public static void DestroyKitchenObject(KitchenObject kitchenObject) {
		KitchenGameMultiplayer.Instance.DestroyKitchenObject(kitchenObject);
	}
}