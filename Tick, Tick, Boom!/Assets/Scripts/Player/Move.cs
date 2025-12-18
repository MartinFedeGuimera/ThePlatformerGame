using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField, Range(1f, 30f)] private float maxSpeed = 15f;
    [SerializeField, Range(1f, 30f)] private float maxAcceleration = 30f;
    [SerializeField, Range(1f, 30f)] private float maxAirAcceleration = 25f;

    private Vector2 desiredVelocity;
    private Vector2 velocity;
    private float direction;
    private float maxSpeedChange;

    private Ground ground;
    private Rigidbody2D body;
    private InputController inputController;

    private bool onGround;

    private void Awake()
    {
        ground = GetComponent<Ground>();
        body = GetComponent<Rigidbody2D>();
        inputController = GetComponent<InputController>();
    }

    private void Update()
    {
        direction = inputController.GetMoveDirection();
    }

    private void FixedUpdate()
    {
        onGround = ground.GetOnGround();

        velocity = body.velocity;

        CalculateDesiredVelocity();

        CalculateMaxSpeedChange();

        UpdateVelocity();

        ApplyFriction();

        body.velocity = velocity;
    }

    private void CalculateDesiredVelocity()
    {
        desiredVelocity.x = direction * maxSpeed;
    }

    private void CalculateMaxSpeedChange()
    {
        float maxAcc = onGround ? maxAcceleration : maxAirAcceleration;
        maxSpeedChange =  maxAcc * Time.fixedDeltaTime;
    }

    private void UpdateVelocity()
    {
        Vector2 velocityDif = desiredVelocity - velocity;

        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
    }

    private void ApplyFriction()
    {
        float friction = ground.GetGroundFriction();
        float frictionStep = friction * Time.fixedDeltaTime;

        velocity.x = Mathf.MoveTowards(velocity.x, 0f, frictionStep);
    }
}
