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
    public bool isGrounded; // Flag indicating if the player is grounded

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask platformLayer;

    public InputActionReference movementAction;
    public InputActionAsset inputActions;
    public InputActionReference jumpAction;

    public Animator animator;


    public void OnEnable()
    {
        // Enable the player's movement input action
        movementAction.action.Enable();
        movementAction.action.performed += OnMovement;
        movementAction.action.canceled += OnMovement;

        // Enable the player's jump input action
        jumpAction.action.Enable();
        jumpAction.action.performed += OnJump;
    }

    public void OnDisable()
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
        animator.SetFloat("Speed", Math.Abs(horizontal));
        // Check if the player is not jumping and set the isJumping parameter to false
        if (context.phase == InputActionPhase.Canceled)
        {
            animator.SetBool("isJumping", false);
        }
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (isGrounded) // Check if the player is grounded
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            isGrounded = false; // Set grounded flag to false after jumping
            animator.SetBool("isJumping", true);
        }
        else
        {
            //animator.SetBool("isJumping", false);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        //animator.SetFloat("Speed", Mathf.Abs(horizontal));
        Flip();

        // Perform the ground check
        isGrounded = IsGrounded() || IsGroundedOnPlatform() || IsOnCharacter() || IsOnObject();
    }

    private bool IsGrounded()
    {
        // Perform an overlap circle check to detect if the player is grounded
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, 0.2f, groundLayer);

        // Check if any of the colliders (except the player itself) are found
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject != gameObject)
            {
                return true;
            }
        }

        return false;
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
            if (collider.CompareTag("Cactus") || collider.CompareTag("VFT") || collider.CompareTag("Ivy") || collider.CompareTag("AloeVera"))
            {
                if (collider.gameObject != this.gameObject)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private bool IsOnObject()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, 0.2f);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("dragable") || collider.CompareTag("damagable") || collider.CompareTag("movable"))
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log($"Collided with {collision.collider} - {collision.contacts[0].otherCollider} - {collision.contacts[0].otherCollider}");
    }
}