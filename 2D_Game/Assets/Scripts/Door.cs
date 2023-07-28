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
    private Key keyClass;

    public GameObject interactButton;
    public GameObject keyNotification;

    public GameObject doorFollowPoint;
    public GameObject key;
    private bool interactable;
    private bool isPlayerInRange; // Flag to check if the player is in range of the door
    public bool hasKey;

    public InputActionReference openDoorAction;

    private Soundmanager soundmanager;

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

        // Find the Key script in the scene and assign it to the keyClass variable
        keyClass = FindObjectOfType<Key>();

        interactable = true;

        soundmanager = GameObject.FindGameObjectWithTag("Sound").GetComponent<Soundmanager>();
    }

    private void OnOpenDoor(InputAction.CallbackContext context)
    {
        if (vft != null && vft.followingKey != null && vft.followingKey.gameObject.CompareTag("Key") && interactable && ps.whichCharacter == 1 && isPlayerInRange)
        {
            soundmanager.playSFX(soundmanager.keyOpen);
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
        }

        vft.followingKey = null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("VFT"))
        {
            //bool hasKey = (keyClass != null && keyClass.isFollowing);

            if (hasKey)
            {
                Debug.Log("has key");
                isPlayerInRange = true; // Player is in range to open the door
                interactButton.SetActive(true);
                keyNotification.SetActive(false); // Hide the "Find the key" notification
            }
            else
            {
                Debug.Log("no key");
                isPlayerInRange = true; // Player is in range, but doesn't have the key yet
                interactButton.SetActive(false);
                keyNotification.SetActive(true); // Show the "Find the key" notification
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("VFT"))
        {
            isPlayerInRange = false; // Player is no longer in range to open the door
            interactButton.SetActive(false);
            keyNotification.SetActive(false); // Hide the "Find the key" notification
        }
    }

    public void SetInteractable(bool value)
    {
        interactable = value;
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