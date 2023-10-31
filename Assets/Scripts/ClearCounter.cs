using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour, IKitchenObjectParent
{

    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform counterTopPoint;

    private KitchenObject kitchenObject;

    public void Interact(Player player) {
        if (kitchenObject == null) { // kitchenObject 존재하지 않다면 
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint); // 해당 위치에 생성
            kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this); // 본인 ClearCounter 인자로 넘기기
        } else { // 이미 kitchenObject 존재한다면
            // 플레이어에게 오브젝트를 준다
            kitchenObject.SetKitchenObjectParent(player);
        }        
    }

    public Transform GetKitchenObjectFollowTransform() {
        return counterTopPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject) {
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject() {
        return kitchenObject;
    }

    public void ClearKitchenObject() {
        kitchenObject = null;
    }

    public bool HasKitchenObject() {
        return kitchenObject != null;
        // 오브젝트 존재한다면 true 반환
        // 오브젝트 존재하지 않다면 false 반환
    }
}
