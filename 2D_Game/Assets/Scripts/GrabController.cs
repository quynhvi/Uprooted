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
    private Gamepad gamepad;

    private ResourceManagement rm;
    private LightSource ls;

    private void Start()
    {
        rm = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<ResourceManagement>();
        ls = FindObjectOfType<LightSource>();
        interactable = true;
        gamepad = Gamepad.current;
    }

    private void Update()
    {
        RaycastHit2D grabCheck = Physics2D.Raycast(grabDetect.position, Vector2.right * transform.localScale, rayDist);

        if (grabCheck.collider != null && grabCheck.collider.tag == "dragable")
        {

            if (Input.GetKey(KeyCode.I) || gamepad != null && gamepad.buttonWest.wasPressedThisFrame)
            {
                interactable = false;
                grabCheck.collider.gameObject.transform.parent = grabHolder;
                grabCheck.collider.gameObject.transform.position = grabHolder.position;
                grabCheck.collider.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;

                // Decrease resource levels
                if (rm != null && ls != null)
                {
                    rm.lightLevelNumber -= ls.chargedLight;
                    rm.lightBarFill.fillAmount -= ls.chargedLight;
                    rm.waterLevelNumber -= ls.chargedLight;
                    rm.waterBarFill.fillAmount -= ls.chargedLight;
                }

            }
            else
            {
                grabCheck.collider.gameObject.transform.parent = null;
                grabCheck.collider.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
            }
        }
    }
}
