using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChestDrawer : MonoBehaviour
{
    [SerializeField] private GameObject drawerOpen;
    [SerializeField] private GameObject code;

    private ResourceManagement rm;
    private LightSource ls;

    public GameObject interactButton;
    private bool interactable;
    private bool drawerOpened = false;

    public InputActionReference openDrawerAction;
    private Soundmanager soundmanager;

    private void OnEnable()
    {
        openDrawerAction.action.Enable();
        openDrawerAction.action.performed += OpenDrawer;
    }

    private void OnDisable()
    {
        openDrawerAction.action.Disable();
        openDrawerAction.action.performed -= OpenDrawer;
    }

    private void Start()
    {
        rm = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<ResourceManagement>();
        ls = FindObjectOfType<LightSource>();
        soundmanager = FindObjectOfType<Soundmanager>();
        interactable = true;
    }

    private void OpenDrawer(InputAction.CallbackContext context)
    {
        if (interactable)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.5f);
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("VFT") && !drawerOpened)
                {
                    soundmanager.playSFX(soundmanager.drawer, 0.7f);
                    ls.chargedLight = 0.03f;

                    drawerOpen.SetActive(true);
                    code.SetActive(true);

                    // Decrease resource levels
                    if (rm != null && ls != null)
                    {
                        rm.waterLevelNumber -= ls.chargedLight;
                        rm.waterBarFill.fillAmount -= ls.chargedLight;
                    }
                    drawerOpened = true;
                    interactable = false;
                    break; // Exit the loop after finding the first VFT

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
