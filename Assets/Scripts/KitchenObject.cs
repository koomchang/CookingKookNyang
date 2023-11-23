using Unity.Netcode;
using UnityEngine;

public class KitchenObject : NetworkBehaviour {
	[SerializeField] private KitchenObjectSO kitchenObjectSO;

	private IKitchenObjectParent kitchenObjectParent;

	public KitchenObjectSO GetKitchenObjectSO() {
		return kitchenObjectSO;
	}

	public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent) {
		if (this.kitchenObjectParent != null)
			// 이전 카운터가 존재한다면
			this.kitchenObjectParent.ClearKitchenObject(); // 이전 카운터에서 오브젝트 제거

		this.kitchenObjectParent = kitchenObjectParent; // 이전 카운터를 새로운 카운터로 대체

		if (kitchenObjectParent.HasKitchenObject())
			// (그럴리 없겠지만) 만약 새로운 카운터에 오브젝트 있다면 디버깅
			Debug.LogError("IKitchenObjectParent already has a KitchenObject!");

		kitchenObjectParent.SetKitchenObject(this);

		// transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
		// transform.localPosition = Vector3.zero;
	}

	public IKitchenObjectParent GetKitchenObjectParent() {
		return kitchenObjectParent;
	}

	public void DestroySelf() {
		kitchenObjectParent.ClearKitchenObject();
		Destroy(gameObject);
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
}