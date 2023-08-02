using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Vine : MonoBehaviour
{
    //public GameObject interactButton;
    public GameObject vine;
    //public GameObject interactZone;

    private bool interactable;
    private bool vineOpen = false; // Track whether the vine is open or closed

    private ResourceManagement rm;
    private LightSource ls;
    private PlayerSwap ps;
    private PlayerMovement pm;

    private Gamepad gamepad;

    public Animator animator;
    private bool isAnimationPlaying = false;
    private PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        rm = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<ResourceManagement>();
        ls = FindObjectOfType<LightSource>();
        ps = FindAnyObjectByType<PlayerSwap>();
        pm = GameObject.FindGameObjectWithTag("Ivy").GetComponent<PlayerMovement>();
        playerMovement = GameObject.FindGameObjectWithTag("Ivy").GetComponent<PlayerMovement>();

        interactable = true;

        gamepad = Gamepad.current;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.I) || (gamepad != null && gamepad.buttonWest.wasPressedThisFrame)) && ps.whichCharacter == 2)
        {
            if (vineOpen)
            {
                CloseVine();
            }
            else if (!vineOpen)
            {
                StartCoroutine(OpenVineAfterAnimation());
                //VineInteract();
            }
        }
    }

    private IEnumerator OpenVineAfterAnimation()
    {
        isAnimationPlaying = true;
        animator.SetBool("isInteracting", true);
        playerMovement.enabled = false;
        yield return new WaitForSeconds(3f);
        playerMovement.enabled = true;

        vine.SetActive(true);
        vineOpen = true;
        //Time.timeScale = 0f;

        isAnimationPlaying = false;
        animator.SetBool("isInteracting", false);
    }

    private void CloseVine()
    {
        vine.SetActive(false);
        vineOpen = false;
        interactable = true;
        //pm.enabled = true;
    }

    private void VineInteract()
    {
        animator.SetBool("isInteracting", true);
        ls.chargedLight = 0.03f;

        vine.SetActive(true);
        vineOpen = true;
        //pm.enabled = false;

        if (rm != null && ls != null)
        {
            rm.lightLevelNumber -= ls.chargedLight;
            rm.lightBarFill.fillAmount -= ls.chargedLight;
            rm.waterLevelNumber -= ls.chargedLight;
            rm.waterBarFill.fillAmount -= ls.chargedLight;
        }
    }
}