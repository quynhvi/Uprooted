using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CactusPunch : MonoBehaviour
{
    [SerializeField] private GameObject interactButton;
    [SerializeField] private GameObject arm;
    public bool interactable;

    private ResourceManagement rm;
    private LightSource ls;
    private PlayerSwap ps;
    private PlayerMovement pm;
    private Damageable damagable;

    private Gamepad gamepad;

    // Start is called before the first frame update
    void Start()
    {
        rm = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<ResourceManagement>();
        ls = FindObjectOfType<LightSource>();
        ps = FindObjectOfType<PlayerSwap>();
        pm = GameObject.FindGameObjectWithTag("Cactus").GetComponent<PlayerMovement>();
        damagable = GameObject.FindGameObjectWithTag("damagable").GetComponent<Damageable>();

        interactable = true;

        gamepad = Gamepad.current;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.I) || (gamepad != null && gamepad.buttonWest.wasPressedThisFrame)) && ps.whichCharacter == 0)
        {
            cactusInteract();
        }
    }

    private void cactusInteract()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.5f);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("damagable"))
            {
                damagable.TakeDamage();

                ls.chargedLight = 0.03f;

                if (rm != null && ls != null)
                {
                    rm.lightLevelNumber -= ls.chargedLight;
                    rm.lightBarFill.fillAmount -= ls.chargedLight;
                    rm.waterLevelNumber -= ls.chargedLight;
                    rm.waterBarFill.fillAmount -= ls.chargedLight;
                }

                arm.SetActive(true); // Activate the arm object

                StartCoroutine(DisableArmAfterDelay(0.5f)); // Disable the arm after a certain duration

                interactable = false;
                break;
            }
        }
    }

    private IEnumerator DisableArmAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        arm.SetActive(false); // Disable the arm object
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (interactable && collision.gameObject.CompareTag("damagable") && ps.whichCharacter == 0)
            interactButton.SetActive(true);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        interactButton.SetActive(false);
    }
}