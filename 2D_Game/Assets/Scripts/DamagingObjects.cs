using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DamagingObject : MonoBehaviour
{
    [SerializeField] private GameObject interactButton;
    private bool interactable;

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
        ps = FindObjectOfType<PlayerSwap>();
        pm = GameObject.FindGameObjectWithTag("Ivy").GetComponent<PlayerMovement>();

        interactable = true;

        gamepad = Gamepad.current;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.I) || (gamepad != null && gamepad.buttonWest.wasPressedThisFrame)) && ps.whichCharacter == 2)
        {
            CactusInteract();
        }
    }

    private void CactusInteract()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.5f);

        foreach (Collider2D collider in colliders)
        {
            Damageable damageableObject = collider.GetComponent<Damageable>();
            if (damageableObject != null)
            {
                damageableObject.TakeDamage();
                ls.chargedLight = 0.03f;

                if (rm != null && ls != null)
                {
                    rm.lightLevelNumber -= ls.chargedLight;
                    rm.lightBarFill.fillAmount -= ls.chargedLight;
                    rm.waterLevelNumber -= ls.chargedLight;
                    rm.waterBarFill.fillAmount -= ls.chargedLight;
                }
                interactable = false;
                break;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (interactable)
            interactButton.SetActive(true);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        interactButton.SetActive(false);
    }
}