using System;
using UnityEngine;

public class ContainerCounter : BaseCounter {
	[SerializeField] private KitchenObjectSO kitchenObjectSO;
	public event EventHandler OnPlayerGrabbedObject;

	public override void Interact(Player player) {
		if (!player.HasKitchenObject()) {
			var kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab); // 해당 위치에 생성
			kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);
			OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
		}
	}
}