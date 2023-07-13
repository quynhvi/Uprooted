using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CabinetDoorScript : MonoBehaviour
{
    public GameObject openDoor;
    public GameObject closedDoor;

    public GameObject platformOne;
    public GameObject platformTwo;

    public GameObject block;

    public GameObject interactButton;

    public InputActionReference interactAction;
    public bool interactable;

    private ResourceManagement rm;
    private LightSource ls;
    private PlayerSwap PlayerSwapScript;

    // Start is called before the first frame update
    void Awake()
    {
        rm = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<ResourceManagement>();
        ls = FindObjectOfType<LightSource>();
        PlayerSwapScript = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<PlayerSwap>();

        openDoor.SetActive(false);
        closedDoor.SetActive(true);
        platformOne.SetActive(false);

        if(platformTwo != null)
            platformTwo.SetActive(false);

        if (block != null)
            block.SetActive(false);

        interactable = true;
    }
    private void OnEnable()
    {
        interactAction.action.Enable();
        interactAction.action.performed += InteractWithClosetDoor;
    }

    private void OnDisable()
    {
        interactAction.action.Disable();
        interactAction.action.performed -= InteractWithClosetDoor;
    }

    private void InteractWithClosetDoor(InputAction.CallbackContext context)
    {
        if(interactable)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.2f); //radius muss angepasst werden
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("VFT") && PlayerSwapScript.whichCharacter == 1) //VFT is colliding and currently being played!
                {
                    ls.chargedLight = 0.03f;

                    // Decrease resource levels
                    if (rm != null && ls != null)
                    {
                        rm.lightLevelNumber -= ls.chargedLight;
                        rm.lightBarFill.fillAmount -= ls.chargedLight;
                        rm.waterLevelNumber -= ls.chargedLight;
                        rm.waterBarFill.fillAmount -= ls.chargedLight;
                    }
                    //if square or I is pressed then set open door active, closed door deactived, platform activated
                    if (!DoorOpen())
                    {
                        Debug.Log("Door has openend!");
                        closedDoor.SetActive(false);
                        openDoor.SetActive(true);
                        platformOne.SetActive(true);

                        if(platformTwo != null)
                            platformTwo.SetActive(true);

                        if (block != null)
                            block.SetActive(true);
                        break; // Exit the loop after finding the first VFT
                    }
                    if (DoorOpen())
                    {
                        Debug.Log("Door has closed!");
                        closedDoor.SetActive(true);
                        openDoor.SetActive(false);
                        platformOne.SetActive(false);

                        if(platformTwo != null)
                            platformTwo.SetActive(false);

                        if(block != null)
                            block.SetActive(false);
                        break; // Exit the loop after finding the first VFT
                    }
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (interactable && collision.CompareTag("VFT"))
        {
            interactButton.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        interactButton.SetActive(false);
    }
    private bool DoorOpen()
    {
        if (openDoor.activeSelf)
        {
            return true;
        }
        else
            return false;
    }
}
