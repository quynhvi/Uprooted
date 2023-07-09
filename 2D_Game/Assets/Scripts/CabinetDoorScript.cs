using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Path;
using UnityEngine;
using UnityEngine.InputSystem;

public class CabinetDoorScript : MonoBehaviour
{
    public GameObject _openDoor;
    public GameObject _closedDoor;

    public GameObject _platformOne;
    public GameObject _platformTwo;

    public GameObject _block;

    public GameObject _interactButton;

    public InputActionReference _interactAction;
    public bool _interactable;

    private ResourceManagement rm;
    private LightSource ls;
    public PlayerSwap PlayerSwapScript;

    // Start is called before the first frame update
    void Awake()
    {
        rm = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<ResourceManagement>();
        ls = FindObjectOfType<LightSource>();
        PlayerSwapScript = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<PlayerSwap>();

        _openDoor.SetActive(false);
        _closedDoor.SetActive(true);
        _platformOne.SetActive(false);

        if(_platformTwo != null)
            _platformTwo.SetActive(false);

        if (_block != null)
            _block.SetActive(false);

        _interactable = true;
    }
    private void OnEnable()
    {
        _interactAction.action.Enable();
        _interactAction.action.performed += InteractWithClosetDoor;
    }

    private void OnDisable()
    {
        _interactAction.action.Disable();
        _interactAction.action.performed -= InteractWithClosetDoor;
    }

    private void InteractWithClosetDoor(InputAction.CallbackContext context)
    {
        if(_interactable)
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
                        _closedDoor.SetActive(false);
                        _openDoor.SetActive(true);
                        _platformOne.SetActive(true);

                        if(_platformTwo != null)
                            _platformTwo.SetActive(true);

                        if (_block != null)
                            _block.SetActive(true);
                        break; // Exit the loop after finding the first VFT
                    }
                    if (DoorOpen())
                    {
                        Debug.Log("Door has closed!");
                        _closedDoor.SetActive(true);
                        _openDoor.SetActive(false);
                        _platformOne.SetActive(false);

                        if(_platformTwo != null)
                            _platformTwo.SetActive(false);

                        if(_block != null)
                            _block.SetActive(false);
                        break; // Exit the loop after finding the first VFT
                    }
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_interactable && collision.CompareTag("VFT"))
        {
            _interactButton.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _interactButton.SetActive(false);
    }
    private bool DoorOpen()
    {
        if (_openDoor.activeSelf)
        {
            return true;
        }
        else
            return false;
    }
}
