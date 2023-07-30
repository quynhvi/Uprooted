using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AloeNumpad : MonoBehaviour
{
    [SerializeField] private GameObject numpad;
    [SerializeField] private GameObject interactButton;

    private PlayerSwap ps;
    private bool interactable;
    private bool isNumpadActive = false; // Flag to track if the numpad is active

    public Animator animator;
    private bool isAnimationPlaying = false;

    Controls action;
    public InputActionReference numpadAction;

    private void Awake()
    {
        action = new Controls();
    }

    private void Start()
    {
        ps = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<PlayerSwap>();
        interactable = true;
        action.Default.Interact.performed += _ => DetermineNumpad();
    }

    private void OnEnable()
    {

        // Enable the input action for Interact when the numpad is not active
        if (!isNumpadActive)
        {
            action.Default.Interact.Enable();
        }

        // Enable the input action for Numpad when the numpad is active
        if (isNumpadActive)
        {
            numpadAction.action.Enable();
        }
    }

    private void OnDisable()
    {
        // Disable the input action for Interact when the numpad is not active
        if (!isNumpadActive)
        {
            action.Default.Interact.Disable();
        }

        // Disable the input action for Numpad when the numpad is active
        if (isNumpadActive)
        {
            numpadAction.action.Disable();
        }
    }

    private void DetermineNumpad()
    {
        if (isNumpadActive)
        {
            CloseNumpad();
        }
        else
        {
            InteractNumpad();
        }
    }

    public void InteractNumpad()
    {
        if (interactable && !isNumpadActive) // Check if the numpad is not active
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.2f);
            bool canInteract = false; // Flag to track if the interaction can happen
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("AloeArm") && ps.whichCharacter == 3 || ps.whichCharacter == 2 && gameObject.CompareTag("AloeArm")) // Aloe is colliding and currently being played!
                {
                    canInteract = true;
                    break;
                }
            }

            if (canInteract)
            {
                animator.SetBool("isInteracting", true);
                StartCoroutine(OpenNumpadAfterAnimation());
            }
            else
            {
                animator.SetBool("isInteracting", false); // Move this line here
            }
        }
    }

    private IEnumerator OpenNumpadAfterAnimation()
    {
        isAnimationPlaying = true;
        yield return new WaitForSeconds(1.5f);

        numpad.SetActive(true);
        interactable = false;
        isNumpadActive = true; // Set the numpad as active
        Time.timeScale = 0f;

        animator.SetBool("isInteracting", false); // Move this line outside the coroutine to stop the animation

        isAnimationPlaying = false;
    }

    // Call this method to close the numpad and re-enable player input.
    public void CloseNumpad()
    {
        numpad.SetActive(false);
        isNumpadActive = false; // Set the numpad as inactive
        interactable = true;
        Time.timeScale = 1f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (interactable && collision.CompareTag("Numpad"))
        {
            interactButton.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        interactButton.SetActive(false);
    }
}