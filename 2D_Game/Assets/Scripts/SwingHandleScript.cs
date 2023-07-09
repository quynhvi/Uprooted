using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class SwingHandleScript : MonoBehaviour
{
    private Rigidbody2D _rgBody;
    private float _rotation;

    public GameObject _closetDoor;
    public GameObject _positionPoint;

    private Transform _transform;
    public GameObject _pivotPoint;

    private Vector3 _defaultPosition;
    private Vector3 _rightPointPosition;

    private Vector2 _connectedAnchor;

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

        _transform = this.GetComponent<Transform>();
        _defaultPosition = _pivotPoint.transform.position;
        _rightPointPosition = _positionPoint.transform.position;
        _connectedAnchor = this.GetComponent<HingeJoint2D>().connectedAnchor;
        Debug.Log("Default Position of Swing-Handle is" + _defaultPosition);

        _openDoor.SetActive(false);
        _closedDoor.SetActive(true);
        _platformOne.SetActive(false);

        if (_platformTwo != null)
            _platformTwo.SetActive(false);

        if (_block != null)
            _block.SetActive(false);

        _interactable = true;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        InteractWithClosetDoor();
    }
    private void InteractWithClosetDoor()
    {
        Debug.Log("Interaction CALLED");
        if (_interactable)
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
                    if (!DoorOpen() && _rotation > -30f)
                    {
                        Debug.Log("Door has openend!");
                        ChangeHandlePosition();

                        _closedDoor.SetActive(false);
                        _openDoor.SetActive(true);
                        _platformOne.SetActive(true);

                        if (_platformTwo != null)
                            _platformTwo.SetActive(true);

                        if (_block != null)
                            _block.SetActive(true);
                        break; // Exit the loop after finding the first VFT
                    }
                    if (DoorOpen() && _rotation < -120f)
                    {
                        Debug.Log("Door has closed!");
                        ChangeHandlePosition();

                        _closedDoor.SetActive(true);
                        _openDoor.SetActive(false);
                        _platformOne.SetActive(false);

                        if (_platformTwo != null)
                            _platformTwo.SetActive(false);

                        if (_block != null)
                            _block.SetActive(false);
                        break; // Exit the loop after finding the first VFT
                    }
                }
            }
        }
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
    private void CheckRotation()
    {
        _rgBody = this.GetComponent<Rigidbody2D>();
        _rotation = _rgBody.rotation;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_interactable && collision.CompareTag("Cactus"))
        {
            _interactButton.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _interactButton.SetActive(false);
    }

    private void ChangeHandlePosition()
    {
        Debug.Log("Handle Position has changed!");
        if (!DoorOpen())
        {
            //Vector2 vec2 = new Vector2(_closetDoor.transform.position.x + _positionPoint.transform.position.x, _closetDoor.transform.position.y + _positionPoint.transform.position.y);
            _pivotPoint.transform.position = _rightPointPosition;
        }
        if (DoorOpen())
            _pivotPoint.transform.position = _defaultPosition;
            //this.GetComponent<HingeJoint2D>().connectedAnchor = _connectedAnchor;
        //gameObject.transform.position = new Vector3(0, -3.7f, 0);
    }
}

