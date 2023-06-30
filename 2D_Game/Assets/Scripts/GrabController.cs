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

    private void Start()
    {
        rm = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<ResourceManagement>();
        ls = FindObjectOfType<LightSource>();
        interactable = true;

        gamepad = Gamepad.current;
        playerSwap = GetComponent<PlayerSwap>(); // Get the PlayerSwap script reference from the player object
    }

    private void Update()
    {
        droppedThisFrame = false;

        RaycastHit2D grabCheck = Physics2D.Raycast(grabDetect.position, Vector2.right * transform.localScale, rayDist);
        GameObject objectToGrab = (grabCheck.collider != null && grabCheck.collider.CompareTag("dragable")) ? grabCheck.collider.gameObject : null;

        if (heldObject != null && playerSwap.whichCharacter == 1)
        if ((Input.GetKeyDown(KeyCode.I) || (gamepad != null && gamepad.buttonWest.wasPressedThisFrame)) && heldObject != null)
        {
            Debug.Log("Object Released");

            heldObject.transform.parent = null;
            heldObject.GetComponent<Rigidbody2D>().isKinematic = false;
            heldObject = null;
            droppedThisFrame = true;
        }

        if (heldObject == null && objectToGrab != null && !droppedThisFrame)
        {
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
        else if (heldObject == null && grabCheck.collider.CompareTag("dragable") && !droppedThisFrame && playerSwap.whichCharacter == 1)
        {
            Debug.Log("grabbed");
            // Grab the object if it is interactable and the grab input is pressed, and the second character is the current one
            if ((Input.GetKeyDown(KeyCode.I) || (gamepad != null && gamepad.buttonWest.wasPressedThisFrame)) && playerSwap.whichCharacter == 1)
            {
                grabCheck.collider.gameObject.transform.parent = grabHolder;
                grabCheck.collider.gameObject.transform.position = grabHolder.position;
                grabCheck.collider.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
                heldObject = grabCheck.transform.gameObject;
                objectToGrab.transform.parent = grabHolder;
                objectToGrab.transform.position = grabHolder.position;
                objectToGrab.GetComponent<Rigidbody2D>().isKinematic = true;
                heldObject = objectToGrab;

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
    }
}