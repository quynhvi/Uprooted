using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GrabController : MonoBehaviour
{
    public Transform grabDetect;
    public Transform grabHolder;
    public float rayDist;
    public bool interactable;

    public GameObject heldObject;
    public bool droppedThisFrame;

    private ResourceManagement rm;
    private LightSource ls;

    private Gamepad gamepad;
    private PlayerSwap playerSwap; // Reference to the PlayerSwap script

    public Animator animator;

    private void Start()
    {
        rm = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<ResourceManagement>();
        ls = FindObjectOfType<LightSource>();
        interactable = true;

        gamepad = Gamepad.current;
        playerSwap = FindAnyObjectByType<PlayerSwap>().GetComponent<PlayerSwap>();
    }

    private void Update()
    {
        droppedThisFrame = false;

        RaycastHit2D grabCheck = Physics2D.Raycast(grabDetect.position, Vector2.right * transform.localScale, rayDist);

        if (heldObject != null && playerSwap.whichCharacter == 1)
        {
            if (heldObject.CompareTag("Key"))
            {
                return;
            }
            // Release the object if it is held and the grab input is pressed
            if (Input.GetKeyDown(KeyCode.I) || (gamepad != null && gamepad.buttonWest.wasPressedThisFrame))
            {
                Debug.Log("Object Released");
                heldObject.transform.parent = null;
                heldObject.GetComponent<Rigidbody2D>().isKinematic = false;
                heldObject = null;
                droppedThisFrame = true;
            }
        }
        if (grabCheck != false)
        {
            if (heldObject == null && grabCheck.collider.CompareTag("dragable") && !droppedThisFrame && playerSwap.whichCharacter == 1)
            {
                // Grab the object if it is interactable and the grab input is pressed, and the second character is the current one
                if ((Input.GetKeyDown(KeyCode.I) || (gamepad != null && gamepad.buttonWest.wasPressedThisFrame)) && playerSwap.whichCharacter == 1)
                {
                    animator.SetBool("isInteracting", true);
                    // Start the coroutine to handle the grabbing process after the animation
                    StartCoroutine(GrabAfterAnimation(grabCheck.collider.gameObject));
                }
            }
        }
    }

    // Coroutine to handle grabbing process after the animation
    private IEnumerator GrabAfterAnimation(GameObject grabbedObject)
    {
        // Wait for the end of the animation (replace `0.5f` with the duration of your animation clip)
        yield return new WaitForSeconds(1f);

        animator.SetBool("isInteracting", false); // Reset the animation parameter

        // Continue with grabbing the object
        grabbedObject.transform.parent = grabHolder;
        grabbedObject.transform.position = grabHolder.position;
        grabbedObject.GetComponent<Rigidbody2D>().isKinematic = true;
        heldObject = grabbedObject;

        // Decrease resource levels
        if (rm != null && ls != null)
        {
            rm.lightLevelNumber -= ls.chargedLight;
            rm.lightBarFill.fillAmount -= ls.chargedLight;
            rm.waterLevelNumber -= ls.chargedLight;
            rm.waterBarFill.fillAmount -= ls.chargedLight;
        }
    }
}