using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class SlidingDoorScript : MonoBehaviour
{
    public GameObject _slidingDoor;
    public Vector3 _doorPosition;

    public GameObject _leftPoint;
    public GameObject _rightPoint;

    public GameObject _platformRight;
    public GameObject _platformLeft;

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

        _doorPosition = _rightPoint.transform.position;

        _slidingDoor.SetActive(true);
        _platformRight.SetActive(false);
        _platformLeft.SetActive(true);

        if (_block != null)
            _block.SetActive(false);

        _interactable = true;
    }

    // Update is called once per frame
    private bool DoorIsRight()
    {
        if (_doorPosition == _rightPoint.transform.position)
        {
            return true;
        }
        else
            return false;
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
        if (_interactable)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.2f); //radius muss angepasst werden
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("VFT") && PlayerSwapScript.whichCharacter == 1)
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
                    if (DoorIsRight()) //RIGHT TO LEFT
                    {
                        _slidingDoor.transform.position = _leftPoint.transform.position;
                        _doorPosition = _slidingDoor.transform.position;
                        _platformRight.SetActive(true);
                        _platformLeft.SetActive(false);

                        if (_block != null)
                            _block.SetActive(false);
                        break; 
                    }
                    if (!DoorIsRight()) //LEFT TO RIGHT
                    {
                        _slidingDoor.transform.position = _rightPoint.transform.position;
                        _doorPosition = _slidingDoor.transform.position;
                        _platformLeft.SetActive(true);
                        _platformRight.SetActive(false);

                        if (_block != null)
                            _block.SetActive(true);
                        break;
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
}

