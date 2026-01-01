using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField, Range(1f, 20f)] private float maxJumpHeight = 10f;
    [SerializeField, Range(1f, 20f)] private float wallJumpPush = 5f;

    [SerializeField, Range(1f, 5f)] private float upwardGravity = 1f;
    [SerializeField, Range(1f, 5f)] private float downwardGravity = 2f;
    [SerializeField, Range(1f, 5f)] private float wallSlideGravity = 0.5f;

    [SerializeField, Range(0.1f, 1f)] private float maxCoyoteTime = 0.2f;
    [SerializeField, Range(0.1f, 1f)] private float maxJumpBuffer = 0.2f;
    private float coyoteTime;
    private float jumpBuffer;

    private Vector2 velocity;

    private Ground ground;
    private Rigidbody2D body;
    private InputController inputController;

    private bool tryJump;
    private bool onGround;
    private bool onWallLeft;
    private bool onWallRight;

    private void Awake()
    {
        ground = GetComponent<Ground>();
        body = GetComponent<Rigidbody2D>();
        inputController = GetComponent<InputController>();
    }

    private void Update()
    {
        tryJump = inputController.GetTryJump();

        if(tryJump)
            jumpBuffer = maxJumpBuffer;
    }

    private void FixedUpdate()
    {
        onGround = ground.GetOnGround();
        onWallLeft = ground.GetOnWallLeft();
        onWallRight = ground.GetOnWallRight();

        velocity = body.velocity;

        coyoteTime -= Time.fixedDeltaTime;
        jumpBuffer -= Time.fixedDeltaTime;

        if (onGround)
        {
            coyoteTime = maxCoyoteTime;
        }

        if (jumpBuffer > 0)
        {
            TryJump();
        }

        GravityHandler();

        body.velocity = velocity;
    }

    private void TryJump()
    {
        if (onGround || coyoteTime > 0)
        {
            velocity.y = maxJumpHeight;
            return;
        }

        if (onWallLeft)
        {
            velocity = new Vector2(wallJumpPush, maxJumpHeight);
            return;
        }
        else if (onWallRight)
        {
            velocity = new Vector2(-wallJumpPush, maxJumpHeight);
            return;
        }
    }


    private void GravityHandler()
    {
        if (velocity.y > 0.001)
        {
            body.gravityScale = upwardGravity;
            return;
        }

        if (onWallLeft || onWallRight)
        {
            body.gravityScale = wallSlideGravity;
            return;
        }

        if (velocity.y > 0.001)
        {
            body.gravityScale = upwardGravity;

            return;
        }
        else if (velocity.y < 0.001)
        {
            body.gravityScale = downwardGravity;

            return;
        }
    }   
}
