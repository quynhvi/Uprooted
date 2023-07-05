using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TriggerZoneIvy : MonoBehaviour
{
    public GameObject interactButton;
    public GameObject vine;
    private bool interactable;
    private bool vineOpen = false; // Track whether the vine is open or closed

    private ResourceManagement rm;
    private LightSource ls;
    private PlayerSwap ps;
    private PlayerMovement pm;

    private Gamepad gamepad;

    // Start is called before the first frame update
    void Start()
    {
        rm = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<ResourceManagement>();
        ls = FindObjectOfType<LightSource>();
        ps = FindAnyObjectByType<PlayerSwap>();
        pm = GameObject.FindGameObjectWithTag("Ivy").GetComponent<PlayerMovement>();

        interactable = true;

        gamepad = Gamepad.current;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.I) || (gamepad != null && gamepad.buttonWest.wasPressedThisFrame)) && ps.whichCharacter == 2)
        {
            if (vineOpen)
            {
                CloseVine();
            }
            else if (interactable)
            {
                VineInteract();
            }
        }
    }

    private void CloseVine()
    {
        vine.SetActive(false);
        vineOpen = false;
        interactable = true;
        //pm.enabled = false;
    }

    private void VineInteract()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.5f);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Ivy"))
            {
                ls.chargedLight = 0.03f;

                vine.SetActive(true);
                vineOpen = true;
                //pm.enabled = false;

                if (rm != null && ls != null)
                {
                    rm.lightLevelNumber -= ls.chargedLight;
                    rm.lightBarFill.fillAmount -= ls.chargedLight;
                    rm.waterLevelNumber -= ls.chargedLight;
                    rm.waterBarFill.fillAmount -= ls.chargedLight;
                }
                interactable = false;
                break; // exit loop after finding Ivy
            }
        }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (interactable)
    //        interactButton.SetActive(true);
    //}

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    interactButton.SetActive(false);
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (interactable)
            interactButton.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        interactButton.SetActive(false);
    }
}
