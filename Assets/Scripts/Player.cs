using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;
    private void Update() {
        
        Vector2 inputVector = new Vector2(0,0); // 키보드 상 x, y 축만 존재

        if (Input.GetKey(KeyCode.W)) { // 상
            inputVector.y = +1;
        } 
        if (Input.GetKey(KeyCode.S)) { // 하
            inputVector.y = -1;
        }
        if (Input.GetKey(KeyCode.A)) { // 좌
            inputVector.x = -1;
        }
        if (Input.GetKey(KeyCode.D)) { // 우
            inputVector.x = +1;
        }

        inputVector = inputVector.normalized;

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y); // x, y, z 축
        transform.position += moveDir * moveSpeed * Time.deltaTime;
        Debug.Log(inputVector);
    }
}
