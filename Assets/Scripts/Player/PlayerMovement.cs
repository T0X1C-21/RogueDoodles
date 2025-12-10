using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {

    private Vector3 moveDirection;
    private float moveSpeed;

    private void Start() {
        moveSpeed = DataManager.Instance.GetPlayerData().moveSpeed;
    }

    private void Update() {
        Movement();
        FlipPlayer();
    }

    private void Movement() {
        float moveDirectionX;
        float moveDirectionY;

        if (Keyboard.current.wKey.isPressed) {
            moveDirectionY = 1;
        } else if (Keyboard.current.sKey.isPressed) {
            moveDirectionY = -1;
        } else {
            moveDirectionY = 0;
        }
        if (Keyboard.current.dKey.isPressed) {
            moveDirectionX = 1;
        } else if (Keyboard.current.aKey.isPressed) {
            moveDirectionX = -1;
        } else {
            moveDirectionX = 0;
        }

        moveDirection = new Vector2(moveDirectionX, moveDirectionY);
        transform.position += moveDirection.normalized * moveSpeed * Time.deltaTime;
    }

    private void FlipPlayer() {
        if(moveDirection.x != 0) {
            Vector3 scale = this.transform.localScale;
            this.transform.localScale = new Vector3(moveDirection.x, scale.y, scale.z);
        }
    }
}