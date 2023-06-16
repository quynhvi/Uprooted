using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //public GameObject interactIcon;
    //private Vector2 boxSize = new Vector2(0.1f, 1f);

    private float horizontal;
    public float speed = 6f;
    private float jumpingPower = 9f;
    private bool isFacingRight = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask platformLayer;

    //private Vines vine;

    // Start is called before the first frame update
    void Start()
    {
        //interactIcon.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.I))
        //{
        //    CheckInteraction();
        //}

        horizontal = Input.GetAxisRaw("Horizontal"); // returns 1, -1 or 0 depending on direction

        if (Input.GetButtonDown("Jump") && IsGrounded() || Input.GetButtonDown("Jump") && IsGroundedOnPlatform())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            // jump higher if pressed longer
        }

        Flip();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        // creates invisible circle at feet of player
        // collide with the ground -> jump
    }

    private bool IsGroundedOnPlatform()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, platformLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight; // set to its oppisite value
            Vector3 localScale = transform.localScale; 
            localScale.x *= -1f; // multiply x component of player's local scale by -1
            transform.localScale = localScale;
        }
    }

    //public void ArmInteractable()
    //{
    //    interactIcon.SetActive(true);
    //}

    //public void NoArmInteractable()
    //{
    //    interactIcon.SetActive(false);
    //}

    //private void CheckInteraction()
    //{
    //    RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, boxSize, 0, Vector2.zero);
    //    Debug.Log("CheckInteraction() called");

    //    if (hits.Length > 0)
    //    {
    //        foreach (RaycastHit2D rc in hits)
    //        {
    //            if (rc.IsInteractable())
    //            {
    //                // Debug.Log("interactable");

    //                Collider2D collider = rc.collider;
    //                vine = collider.GetComponent<Vines>();
    //                vine.Interact();
    //                return;
    //            }

    //        }
    //    }
    //}
}
