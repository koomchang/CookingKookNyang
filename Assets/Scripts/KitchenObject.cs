using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private ClearCounter clearCounter;

    public KitchenObjectSO GetKitchenObjectSO() {
        return kitchenObjectSO;
    }

    public void SetClearCounter(ClearCounter clearCounter) {
        if (this.clearCounter != null) {
            this.clearCounter.ClearKitchenObject(); 
        }

        this.clearCounter = clearCounter; 
        
        if (clearCounter.HasKitchenObject()) { // 만약 본인 clearCounter 가 이미 kitchenObject 갖고 있다면
            Debug.LogError("Counter already has a KitchenObject!"); 
        }

        // 만약 본인 clearCounter 가 kitchenObject 갖지 않는다면
        clearCounter.SetKitchenObject(this); // 본인 kitchenObject 대입

        transform.parent = clearCounter.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public ClearCounter GetClearCounter() {
        return clearCounter;
    }
}
