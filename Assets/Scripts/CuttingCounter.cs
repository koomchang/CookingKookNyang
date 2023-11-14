using UnityEngine;

public class CuttingCounter : BaseCounter {
	[SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

	private int cuttingProgress;


	public override void Interact(Player player) {
		if (!HasKitchenObject()) {
			if (player.HasKitchenObject())
				if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())) {
					player.GetKitchenObject().SetKitchenObjectParent(this);
					cuttingProgress = 0;
				}
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
			cuttingProgress++;
			var cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
			if (cuttingProgress >= cuttingRecipeSO.cuttingProgressMax) {
				var outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
				GetKitchenObject().DestroySelf();
				KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
			}
		}
	}

	private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO) {
		var cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
		return cuttingRecipeSO != null;
	}

	private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO) {
		var cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
		if (cuttingRecipeSO != null) return cuttingRecipeSO.output;

		return null;
	}

	private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO) {
		foreach (var CuttingRecipeSO in cuttingRecipeSOArray)
			if (CuttingRecipeSO.input == inputKitchenObjectSO)
				return CuttingRecipeSO;
		return null;
	}
}