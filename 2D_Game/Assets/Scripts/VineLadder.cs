using UnityEngine;

public class VineLadder : MonoBehaviour
{
    [SerializeField] private float climbingSpeed = 5f;

    private bool isPlayerOnLadder = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Cactus"))
        {
            Debug.Log("cactus entered");
            isPlayerOnLadder = true;
            other.attachedRigidbody.gravityScale = 0f;
            // player ignores all layers (collision) except iy ladder layer
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Cactus"))
        {
            isPlayerOnLadder = false;
            other.attachedRigidbody.gravityScale = 1f;
            // ignore deactivated
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (isPlayerOnLadder && other.CompareTag("Cactus"))
        {
            float verticalInput = Input.GetAxis("Vertical");

            // Apply climbing movement to the player
            other.attachedRigidbody.velocity = new Vector2(other.attachedRigidbody.velocity.x, verticalInput * climbingSpeed);
        }
    }
}