using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    public float speed = 6f;
    public float jumpingPower = 9f;
    private bool isFacingRight = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask platformLayer;

    public InputActionReference movementAction;
    public InputActionAsset inputActions;
    public InputActionReference jumpAction;

    private void OnEnable()
    {
        // Enable the player's movement input action
        movementAction.action.Enable();
        movementAction.action.performed += OnMovement;
        movementAction.action.canceled += OnMovement;

        // Enable the player's jump input action
        jumpAction.action.Enable();
        jumpAction.action.performed += OnJump;
    }

    private void OnDisable()
    {
        // Disable the player's movement input action
        movementAction.action.Disable();
        movementAction.action.performed -= OnMovement;
        movementAction.action.canceled -= OnMovement;

        // Disable the player's jump input action
        jumpAction.action.Disable();
        jumpAction.action.performed -= OnJump;
    }

    private void OnMovement(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (IsGrounded() || IsGroundedOnPlatform() || IsOnCharacter())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        Flip();
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private bool IsGroundedOnPlatform()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, platformLayer);
    }

    private bool IsOnCharacter()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, 0.2f);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Cactus") || collider.CompareTag("VFT") || collider.CompareTag("Ivy"))
            {
                return true;
            }
        }

        return false;
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}