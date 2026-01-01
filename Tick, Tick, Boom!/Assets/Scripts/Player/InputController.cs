using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private float moveDirection;
    private bool tryJump;
    private bool jumpHeld;

    private void Update()
    {
        moveDirection = Input.GetAxisRaw("Horizontal");
        
        tryJump = Input.GetKeyDown(KeyCode.Space);

        jumpHeld = Input.GetKey(KeyCode.Space);
    }

    public float GetMoveDirection() { return moveDirection; }
    public bool GetTryJump() { return tryJump; }
}
