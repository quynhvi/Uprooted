using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Door : MonoBehaviour
{
    private FollowPoint vft;
    public GameObject door;
    public GameObject ivyInteract;

    private ResourceManagement rm;
    private LightSource ls;
    private PlayerSwap ps;

    public GameObject interactButton;
    public GameObject doorFollowPoint;
    public GameObject key;
    private bool interactable;

    public InputActionReference openDoorAction;

    private void OnEnable()
    {
        openDoorAction.action.Enable();
        openDoorAction.action.performed += OnOpenDoor;
    }

    private void Start()
    {
        vft = FindAnyObjectByType<FollowPoint>();
        rm = FindObjectOfType<ResourceManagement>();
        ls = FindObjectOfType<LightSource>();
        ps = FindObjectOfType<PlayerSwap>();

        interactable = true;
    }

    private void OnOpenDoor(InputAction.CallbackContext context)
    {
        if (vft != null && vft.followingKey != null && vft.followingKey.gameObject.CompareTag("Key") && interactable && ps.whichCharacter == 1)
        {
            interactable = false;
            OpenDoor();
        }
    }

    public void OpenDoor()
    {
        if (rm != null && ls != null)
        {
            rm.lightLevelNumber -= ls.chargedLight;
            rm.lightBarFill.fillAmount -= ls.chargedLight;
            rm.waterLevelNumber -= ls.chargedLight;
            rm.waterBarFill.fillAmount -= ls.chargedLight;
        }

        door.SetActive(false);
        ivyInteract.SetActive(true);
        key.SetActive(false);

        if (vft.followingKey != null)
        {
            vft.followingKey.followTarget = doorFollowPoint.transform; // Set the door as the followTarget
            vft.followingKey.isFollowing = true;
            //key.SetActive(false);
        }

        vft.followingKey = null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "VFT" && interactable)
        {
            interactButton.SetActive(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "VFT")
        {
            interactButton.SetActive(false);
        }
    }

    private new T FindAnyObjectByType<T>() where T : MonoBehaviour
    {
        T[] objects = FindObjectsOfType<T>();
        if (objects.Length > 0)
        {
            return objects[0];
        }
        return null;
    }
}