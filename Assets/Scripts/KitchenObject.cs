using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private IKitchenObjectParent kitchenObjectParent;

    public KitchenObjectSO GetKitchenObjectSO() {
        return kitchenObjectSO;
    }

    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent) {
        if (this.kitchenObjectParent != null) {
            this.kitchenObjectParent.ClearKitchenObject(); 
        }

        this.kitchenObjectParent = kitchenObjectParent; 
        
        if (kitchenObjectParent.HasKitchenObject()) { // 만약 본인 clearCounter 가 이미 kitchenObject 갖고 있다면
            Debug.LogError("IKitchenObjectParent already has a KitchenObject!"); 
        }

        // 만약 본인 clearCounter 가 kitchenObject 갖지 않는다면
        kitchenObjectParent.SetKitchenObject(this); // 본인 kitchenObject 대입

        transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public IKitchenObjectParent GetKitchenObjectParent() {
        return kitchenObjectParent;
    }
}
