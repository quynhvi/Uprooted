using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class SwingHandleScript : MonoBehaviour
{
    private Rigidbody2D rgBody;
    private float rotation;

    public GameObject closetDoor;
    public GameObject positionPoint;

    private Transform _transform;
    public GameObject pivotPoint;

    private Vector3 defaultPosition;
    private Vector3 rightPointPosition;

    private Vector2 connectedAnchor;

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
    public PlayerSwap PlayerSwapScript;

    // Start is called before the first frame update
    void Awake()
    {
        rm = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<ResourceManagement>();
        ls = FindObjectOfType<LightSource>();
        PlayerSwapScript = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<PlayerSwap>();

        _transform = this.GetComponent<Transform>();
        defaultPosition = pivotPoint.transform.position;
        rightPointPosition = positionPoint.transform.position;
        connectedAnchor = this.GetComponent<HingeJoint2D>().connectedAnchor;
        Debug.Log("Default Position of Swing-Handle is" + defaultPosition);

        openDoor.SetActive(false);
        closedDoor.SetActive(true);
        platformOne.SetActive(false);

        if (platformTwo != null)
            platformTwo.SetActive(false);

        if (block != null)
            block.SetActive(false);

        interactable = true;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        InteractWithClosetDoor();
    }
    private void InteractWithClosetDoor()
    {
        Debug.Log("Interaction CALLED");
        if (interactable)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.2f); //radius muss angepasst werden
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Cactus") && PlayerSwapScript.whichCharacter == 0)
                {
                    CheckRotation();
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
                    if (!DoorOpen() && rotation > -30f)
                    {
                        Debug.Log("Door has openend!");
                        ChangeHandlePosition();

                        closedDoor.SetActive(false);
                        openDoor.SetActive(true);
                        platformOne.SetActive(true);

                        if (platformTwo != null)
                            platformTwo.SetActive(true);

                        if (block != null)
                            block.SetActive(true);
                        break; // Exit the loop after finding the first VFT
                    }
                    if (DoorOpen() && rotation < -120f)
                    {
                        Debug.Log("Door has closed!");
                        ChangeHandlePosition();

                        closedDoor.SetActive(true);
                        openDoor.SetActive(false);
                        platformOne.SetActive(false);

                        if (platformTwo != null)
                            platformTwo.SetActive(false);

                        if (block != null)
                            block.SetActive(false);
                        break; // Exit the loop after finding the first VFT
                    }
                }
            }
        }
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
    private void CheckRotation()
    {
        rgBody = this.GetComponent<Rigidbody2D>();
        rotation = rgBody.rotation;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (interactable && collision.CompareTag("Cactus"))
        {
            interactButton.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        interactButton.SetActive(false);
    }

    private void ChangeHandlePosition()
    {
        Debug.Log("Handle Position has changed!");
        if (!DoorOpen())
        {
            //Vector2 vec2 = new Vector2(_closetDoor.transform.position.x + _positionPoint.transform.position.x, _closetDoor.transform.position.y + _positionPoint.transform.position.y);
            pivotPoint.transform.position = rightPointPosition;
        }
        if (DoorOpen())
            pivotPoint.transform.position = defaultPosition;
            //this.GetComponent<HingeJoint2D>().connectedAnchor = _connectedAnchor;
        //gameObject.transform.position = new Vector3(0, -3.7f, 0);
    }
}

