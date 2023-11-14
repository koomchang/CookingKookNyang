public class CuttingCounter : BaseCounter {

	public override void Interact(Player player) {
		if (!HasKitchenObject()) {
			if (player.HasKitchenObject()) {
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
}