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
    private DamageableCurtain damageableCurtain;
    private DamagableGutter damageableGutter;


    private Gamepad gamepad;

    // Start is called before the first frame update
    void Start()
    {
        rm = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<ResourceManagement>();
        ls = FindObjectOfType<LightSource>();
        ps = FindObjectOfType<PlayerSwap>();
        pm = GameObject.FindGameObjectWithTag("Cactus").GetComponent<PlayerMovement>();
        damagable = GameObject.FindGameObjectWithTag("damagable").GetComponent<Damageable>();
        damageableCurtain = GameObject.FindGameObjectWithTag("Curtain").GetComponent<DamageableCurtain>();
        damageableGutter = GameObject.FindGameObjectWithTag("Gutter").GetComponent<DamagableGutter>();

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
                arm.SetActive(true); // Activate the arm object
                Debug.Log("punch");
                damagable.TakeDamage();

                ls.chargedLight = 0.03f;

                if (rm != null && ls != null)
                {
                    rm.lightLevelNumber -= ls.chargedLight;
                    rm.lightBarFill.fillAmount -= ls.chargedLight;
                    rm.waterLevelNumber -= ls.chargedLight;
                    rm.waterBarFill.fillAmount -= ls.chargedLight;
                }
                StartCoroutine(DisableArmAfterDelay(0.5f)); // Disable the arm after a certain duration

                //interactable = false;
                break;
            }

            if (collider.CompareTag("Curtain"))
            {
                arm.SetActive(true); // Activate the arm object
                Debug.Log("punch");
                damageableCurtain.TakeDamage();

                ls.chargedLight = 0.03f;

                if (rm != null && ls != null)
                {
                    rm.lightLevelNumber -= ls.chargedLight;
                    rm.lightBarFill.fillAmount -= ls.chargedLight;
                    rm.waterLevelNumber -= ls.chargedLight;
                    rm.waterBarFill.fillAmount -= ls.chargedLight;
                }
                StartCoroutine(DisableArmAfterDelay(0.5f)); // Disable the arm after a certain duration

                //interactable = false;
                break;
            }

            if (collider.CompareTag("Gutter"))
            {
                arm.SetActive(true); // Activate the arm object
                Debug.Log("punch");
                damageableGutter.TakeDamage();

                ls.chargedLight = 0.03f;

                if (rm != null && ls != null)
                {
                    rm.lightLevelNumber -= ls.chargedLight;
                    rm.lightBarFill.fillAmount -= ls.chargedLight;
                    rm.waterLevelNumber -= ls.chargedLight;
                    rm.waterBarFill.fillAmount -= ls.chargedLight;
                }
                StartCoroutine(DisableArmAfterDelay(0.5f)); // Disable the arm after a certain duration

                //interactable = false;
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
        if (interactable && collision.gameObject.CompareTag("Curtain") && ps.whichCharacter == 0)
            interactButton.SetActive(true);
        if (interactable && collision.gameObject.CompareTag("Gutter") && ps.whichCharacter == 0)
            interactButton.SetActive(true);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        interactButton.SetActive(false);
    }
}