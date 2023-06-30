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

        if (Input.GetKeyDown(KeyCode.I) || (gamepad != null && gamepad.buttonWest.wasPressedThisFrame) && heldObject != null)
        {
            Debug.Log("Object Released");

            heldObject.transform.parent = null;
            heldObject.GetComponent<Rigidbody2D>().isKinematic = false;
            heldObject = null;
            droppedThisFrame = true;
        }



        if (heldObject == null && grabCheck.collider.CompareTag("dragable") && !droppedThisFrame)
        {
            if (Input.GetKeyDown(KeyCode.I) || (gamepad != null && gamepad.buttonWest.wasPressedThisFrame))
            {
                grabCheck.collider.gameObject.transform.parent = grabHolder;
                grabCheck.collider.gameObject.transform.position = grabHolder.position;
                grabCheck.collider.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
                heldObject = grabCheck.transform.gameObject;

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