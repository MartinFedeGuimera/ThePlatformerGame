using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    private bool onGround;
    private bool onWallLeft;
    private bool onWallRight;

    private float groundFriction;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CheckCollision(collision);
        SetGroundFriction(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        CheckCollision(collision);
        SetGroundFriction(collision);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        onGround = false;
        onWallLeft = false;
        onWallRight = false;

        groundFriction = 0;
    }

    private void CheckCollision(Collision2D collision)
    {
        onGround = false;
        onWallLeft = false;
        onWallRight = false;

        for (int i = 0; i < collision.contactCount; i++)
        {
            Vector2 normal = collision.GetContact(i).normal;

            if (normal.y >= 0.5f)
                onGround = true;

            if (normal.x >= 0.5f)
                onWallLeft = true;

            if (normal.x <= -0.5f)
                onWallRight = true;
        }
    }

    private void SetGroundFriction(Collision2D collision)
    {
        PhysicsMaterial2D material = collision.rigidbody.sharedMaterial;

        groundFriction = 0;

        if (material != null)
            groundFriction = material.friction;
    }

    public bool GetOnGround() { return onGround; }
    public bool GetOnWallLeft() { return onWallLeft; }
    public bool GetOnWallRight() { return onWallRight; }
    public float GetGroundFriction() { return groundFriction; }
}
