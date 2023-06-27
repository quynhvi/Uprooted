using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private KeyFollowPoint vft;
    public GameObject door;
    public GameObject ivyInteract;

    private ResourceManagement rm;
    private LightSource ls;

    public GameObject interactButton;
    private bool interactable;

    // Start is called before the first frame update
    void Start()
    {
        vft = FindAnyObjectByType<KeyFollowPoint>();
        rm = FindObjectOfType<ResourceManagement>();
        ls = FindObjectOfType<LightSource>();

        interactable = true;
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.I))
        {
            if (vft != null && vft.followingKey != null && vft.followingKey.gameObject.CompareTag("Key"))
            {
                interactable = false;
                OpenDoor();
            }
        }
    }

    private void OpenDoor()
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

        if (vft.followingKey != null)
        {
            vft.followingKey.followTarget = door.transform; // Set the door as the followTarget
            vft.followingKey.isFollowing = true;
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
}