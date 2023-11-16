using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter {

	[SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;

	public override void Interact(Player player) {
		if (!HasKitchenObject()) {
			if (player.HasKitchenObject())
				if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())) {
					player.GetKitchenObject().SetKitchenObjectParent(this);
				}
		}
		else {
			if (player.HasKitchenObject()) { }
			else {
				GetKitchenObject().SetKitchenObjectParent(player);
			}
		}
	}
	
	
	private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO) {
		var fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
		return fryingRecipeSO != null;
	}

	private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO) {
		var fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
		if (fryingRecipeSO != null) return fryingRecipeSO.output;

		return null;
	}

	private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO) {
		foreach (var fryingRecipeSO in fryingRecipeSOArray)
			if (fryingRecipeSO.input == inputKitchenObjectSO)
				return fryingRecipeSO;
		return null;
	}
}