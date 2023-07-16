using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AloeZone : MonoBehaviour
{
    [SerializeField] private GameObject interactButton;
    [SerializeField] private GameObject aloeArm;
    //[SerializeField] private GameObject interactZone;

    //private bool interactable;
    //private bool aloeOpen = false; // Track whether the aloe arm is open or closed
    private Vector2 boxSize = new Vector2(5f, 5f);

    private ResourceManagement rm;
    private LightSource ls;
    private PlayerSwap ps;
    private PlayerMovement pm;

    //private Gamepad gamepad;
    public InputActionReference interactAction;

    // Start is called before the first frame update
    void Start()
    {
        rm = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<ResourceManagement>();
        ls = FindObjectOfType<LightSource>();
        ps = FindAnyObjectByType<PlayerSwap>();
        pm = GameObject.FindGameObjectWithTag("AloeVera").GetComponent<PlayerMovement>();

        //interactable = true;

        aloeArm.SetActive(false);

        //gamepad = Gamepad.current;
    }

    //private void OnEnable()
    //{
    //    interactAction.action.Enable();
    //    interactAction.action.performed += AloeInteract;
    //}

    //private void OnDisable()
    //{
    //    interactAction.action.Disable();
    //    interactAction.action.performed -= AloeInteract;
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    if ((Input.GetKeyDown(KeyCode.I) || (gamepad != null && gamepad.buttonWest.wasPressedThisFrame)) && ps.whichCharacter == 3)
    //    {
    //        if (aloeOpen)
    //        {
    //            CloseArm();
    //        }
    //        else if (interactable)
    //        {
    //            AloeInteract();
    //        }
    //    }
    //}

    //private void CloseArm()
    //{
    //    aloeArm.SetActive(false);
    //    aloeOpen = false;
    //    interactable = true;
    //}

    //public void AloeInteract(InputAction.CallbackContext context)
    //{
    //    Debug.Log("aloe press I");
    //    if (interactable)
    //    {
    //        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.2f);

    //        foreach (Collider2D collider in colliders)
    //        {
    //            if (collider.CompareTag("AloeZone") && ps.whichCharacter == 3)
    //            {
    //                aloeArm.SetActive(true);
    //                Debug.Log("Aloe hits zone");
                    
    //                aloeOpen = true;
    //                pm.enabled = true;
    //                ls.chargedLight = 0.03f;

    //                if (rm != null && ls != null)
    //                {
    //                    rm.lightLevelNumber -= ls.chargedLight;
    //                    rm.lightBarFill.fillAmount -= ls.chargedLight;
    //                    rm.waterLevelNumber -= ls.chargedLight;
    //                    rm.waterBarFill.fillAmount -= ls.chargedLight;
    //                }
    //                interactable = false;
    //                break; // exit loop after finding aloe
    //            }
    //        }
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("AloeZone") && ps.whichCharacter == 3 || collision.gameObject.CompareTag("AloeZone") && ps.whichCharacter == 2)
        {
            aloeArm.SetActive(true);
            interactButton.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        interactButton.SetActive(false);
        aloeArm.SetActive(false);
    }
}
