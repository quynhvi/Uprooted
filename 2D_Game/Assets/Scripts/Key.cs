using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Key : MonoBehaviour
{
    public bool isFollowing;
    public float followSpeed;
    public Transform followTarget;

    private ResourceManagement rm;
    private LightSource ls;
    private PlayerSwap ps;

    public GameObject interactButton;
    private bool interactable;

    public InputActionReference collectKeyAction;

    private void OnEnable()
    {
        collectKeyAction.action.Enable();
        collectKeyAction.action.performed += OnCollectKey;
    }


    private void Start()
    {
        rm = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<ResourceManagement>();
        ls = FindObjectOfType<LightSource>();
        ps = FindObjectOfType<PlayerSwap>();
        interactable = true;
    }

    private void Update()
    {
        if (isFollowing)
        {
            transform.position = Vector3.Lerp(transform.position, followTarget.position, followSpeed * Time.deltaTime);
        }
    }

    private void OnCollectKey(InputAction.CallbackContext context)
    {
        if (!isFollowing && interactable)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.5f);
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("VFT") && ps.whichCharacter == 1)
                {
                    ls.chargedLight = 0.03f;

                    FollowPoint vft = FindAnyObjectByType<FollowPoint>();
                    if (vft != null)
                    {
                        Debug.Log("Colliding with key");
                        followTarget = vft.followPoint;
                        isFollowing = true;
                        vft.followingKey = this;

                        // Decrease resource levels
                        if (rm != null && ls != null)
                        {
                            rm.lightLevelNumber -= ls.chargedLight;
                            rm.lightBarFill.fillAmount -= ls.chargedLight;
                            rm.waterLevelNumber -= ls.chargedLight;
                            rm.waterBarFill.fillAmount -= ls.chargedLight;
                        }

                        interactable = false;
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