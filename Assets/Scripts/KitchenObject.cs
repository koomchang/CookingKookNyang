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
        if (this.clearCounter != null) { // 이전 카운터가 존재한다면
            this.clearCounter.ClearKitchenObject(); // 이전 카운터에서 오브젝트 제거
        }        
        this.clearCounter = clearCounter; // 이전 카운터를 새로운 카운터로 대체

        if (clearCounter.HasKitchenObject()) { // (그럴리 없겠지만) 만약 새로운 카운터에 오브젝트 있다면 디버깅
            Debug.LogError("Counter already has a KitchenObject!");
        }
        clearCounter.SetKitchenObject(this);

        transform.parent = clearCounter.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public ClearCounter GetClearCounter() {
        return clearCounter;
    }

    // public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent) {
    //     if (this.kitchenObjectParent != null) {
    //         this.kitchenObjectParent.ClearKitchenObject(); 
    //     }

    //     this.kitchenObjectParent = kitchenObjectParent; 
        
    //     if (kitchenObjectParent.HasKitchenObject()) { // 만약 본인 clearCounter 가 이미 kitchenObject 갖고 있다면
    //         Debug.LogError("IKitchenObjectParent already has a KitchenObject!"); 
    //     }

    //     // 만약 본인 clearCounter 가 kitchenObject 갖지 않는다면
    //     kitchenObjectParent.SetKitchenObject(this); // 본인 kitchenObject 대입

    //     transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
    //     transform.localPosition = Vector3.zero;
    // }

    // public IKitchenObjectParent GetKitchenObjectParent() {
    //     return kitchenObjectParent;
    // }
}
