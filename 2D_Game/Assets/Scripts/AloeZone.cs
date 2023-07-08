using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AloeZone : MonoBehaviour
{
    [SerializeField] private GameObject interactButton;
    [SerializeField] private GameObject aloeArm;
    [SerializeField] private GameObject interactZone;

    private bool interactable;
    private bool aloeOpen = false; // Track whether the aloe arm is open or closed
    private Vector2 boxSize = new Vector2(5f, 5f);

    private ResourceManagement rm;
    private LightSource ls;
    private PlayerSwap ps;
    private PlayerMovement pm;

    private Gamepad gamepad;

    // Start is called before the first frame update
    void Start()
    {
        rm = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<ResourceManagement>();
        ls = FindObjectOfType<LightSource>();
        ps = FindAnyObjectByType<PlayerSwap>();
        pm = GameObject.FindGameObjectWithTag("AloeVera").GetComponent<PlayerMovement>();

        interactable = true;

        gamepad = Gamepad.current;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.I) || (gamepad != null && gamepad.buttonWest.wasPressedThisFrame)) && ps.whichCharacter == 2)
        {
            if (aloeOpen)
            {
                CloseArm();
            }
            else if (interactable)
            {
                AloeInteract();
            }
        }
    }

    private void CloseArm()
    {
        aloeArm.SetActive(false);
        aloeOpen = false;
        interactable = true;
        pm.enabled = true;
    }

    private void AloeInteract()
    {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, boxSize, 0, Vector2.zero);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.CompareTag("AloeVera"))
            {
                Debug.Log("Aloe hits zone");
                ls.chargedLight = 0.03f;

                aloeArm.SetActive(true);
                aloeOpen = true;
                pm.enabled = false;

                if (rm != null && ls != null)
                {
                    rm.lightLevelNumber -= ls.chargedLight;
                    rm.lightBarFill.fillAmount -= ls.chargedLight;
                    rm.waterLevelNumber -= ls.chargedLight;
                    rm.waterBarFill.fillAmount -= ls.chargedLight;
                }
                interactable = false;
                break; // exit loop after finding aloe
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("AloeVera") && ps.whichCharacter == 3)
        {
            interactButton.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        interactButton.SetActive(false);
    }
}
