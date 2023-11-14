using UnityEngine;

public class CuttingCounter : BaseCounter {
	[SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

	public override void Interact(Player player) {
		if (!HasKitchenObject()) {
			if (player.HasKitchenObject())
				if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
					player.GetKitchenObject().SetKitchenObjectParent(this);
		}
		else {
			if (player.HasKitchenObject()) { }
			else {
				GetKitchenObject().SetKitchenObjectParent(player);
			}
		}
	}

	public override void InteractAlternate(Player player) {
		if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO())) {
			var outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());

			GetKitchenObject().DestroySelf();
			KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
		}
	}

	private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO) {
		foreach (var CuttingRecipeSO in cuttingRecipeSOArray)
			if (CuttingRecipeSO.input == inputKitchenObjectSO)
				return true;
		return false;
	}

	private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO) {
		foreach (var CuttingRecipeSO in cuttingRecipeSOArray)
			if (CuttingRecipeSO.input == inputKitchenObjectSO)
				return CuttingRecipeSO.output;
		return null;
	}
}