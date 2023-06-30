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

    private void Start()
    {
        rm = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<ResourceManagement>();
        ls = FindObjectOfType<LightSource>();
        interactable = true;

        gamepad = Gamepad.current;
    }

    private void Update()
    {
        droppedThisFrame = false;

        RaycastHit2D grabCheck = Physics2D.Raycast(grabDetect.position, Vector2.right * transform.localScale, rayDist);
        GameObject objectToGrab = (grabCheck.collider != null && grabCheck.collider.CompareTag("dragable")) ? grabCheck.collider.gameObject : null;

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
            if (Input.GetKeyDown(KeyCode.I) || (gamepad != null && gamepad.buttonWest.wasPressedThisFrame))
            {
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