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

    public InputActionReference interactAction;

    private void Start()
    {
        ps = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<PlayerSwap>();
        interactable = true;
    }

    private void OnEnable()
    {
        interactAction.action.Enable();
        interactAction.action.performed += InteractNumpad;
    }

    private void OnDisable()
    {
        interactAction.action.Disable();
        interactAction.action.performed -= InteractNumpad;
    }

    public void InteractNumpad(InputAction.CallbackContext context)
    {
        if (interactable)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.2f);
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("AloeArm") && ps.whichCharacter == 3 || ps.whichCharacter == 2 && gameObject.CompareTag("AloeArm")) // Aloe is colliding and currently being played!
                {
                    numpad.SetActive(true);
                    
                    interactable = false;
                    Time.timeScale = 0f;
                }
            }
        }
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
