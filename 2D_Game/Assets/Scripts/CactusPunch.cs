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
    //private DamageableCurtain damageableCurtain;
    private DamagableGutter damageableGutter;
    private DamagableBox1 damageableBox1;
    private DamageableGiraffe damageableGiraffe;
    private DamageableBox2 damageableBox2;
    private DamageableBox3 damageableBox3;
    private DamageableBasket damageableBasket;
    private DamageableGarbage damageableGarbage;

    private Gamepad gamepad;

    private Soundmanager soundmanager;

    public Animator animator;
    private bool isPunching = false;

    // Start is called before the first frame update
    void Start()
    {
        rm = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<ResourceManagement>();
        ls = FindObjectOfType<LightSource>();
        ps = FindObjectOfType<PlayerSwap>();
        pm = GameObject.FindGameObjectWithTag("Cactus").GetComponent<PlayerMovement>();

        damagable = GameObject.FindGameObjectWithTag("damagable").GetComponent<Damageable>();
        //damageableCurtain = GameObject.FindGameObjectWithTag("Curtain").GetComponent<DamageableCurtain>();
        damageableGutter = GameObject.FindGameObjectWithTag("Gutter").GetComponent<DamagableGutter>();
        damageableBox1 = GameObject.FindGameObjectWithTag("Box1").GetComponent<DamagableBox1>();
        damageableBox2 = GameObject.FindGameObjectWithTag("Box2").GetComponent<DamageableBox2>();
        damageableBox3 = GameObject.FindGameObjectWithTag("Box3").GetComponent<DamageableBox3>();
        damageableGiraffe = GameObject.FindGameObjectWithTag("Giraffe").GetComponent<DamageableGiraffe>();
        damageableBasket = GameObject.FindGameObjectWithTag("Basket").GetComponent<DamageableBasket>();
        damageableGarbage = GameObject.FindGameObjectWithTag("Garbage").GetComponent<DamageableGarbage>();

        interactable = true;

        gamepad = Gamepad.current;

        soundmanager = GameObject.FindGameObjectWithTag("Sound").GetComponent<Soundmanager>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.I) || (gamepad != null && gamepad.buttonWest.wasPressedThisFrame)) && ps.whichCharacter == 0)
        {
            if (!isPunching)
            {
                animator.SetBool("isInteracting", true);
                StartCoroutine(ResetPunchingFlag());
                cactusInteract();
            }
        }
    }

    private void cactusInteract()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.5f);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("damagable"))
            {
                Damageable();
                break;
            }

            //if (collider.CompareTag("Curtain"))
            //{
            //    Curtain();
            //    break;
            //}

            if (collider.CompareTag("Gutter"))
            {
                Gutter();
                break;
            }

            if (collider.CompareTag("Box1"))
            {
                Box1();
                break;
            }

            if (collider.CompareTag("Box2"))
            {
                Box2();
                break;
            }

            if (collider.CompareTag("Box3"))
            {
                Box3();
                break;
            }

            if (collider.CompareTag("Giraffe"))
            {
                Giraffe();
                break;
            }

            if (collider.CompareTag("Basket"))
            {
                Basket();
                break;
            }
            if (collider.CompareTag("Garbage"))
            {
                Garabge();
                break;
            }
        }
    }

    private IEnumerator ResetPunchingFlag()
    {
        yield return new WaitForSeconds(0.5f);

        // Reset the IsPunching parameter in the animator
        animator.SetBool("isInteracting", false);

        // Reset the flag to allow future punches
        isPunching = false;
    }

    private void Damageable()
    {
        soundmanager.playSFX(soundmanager.cactusPunch);
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
        //break;
    }

    //private void Curtain()
    //{
    //    arm.SetActive(true); // Activate the arm object
    //    Debug.Log("punch");
    //    damageableCurtain.TakeDamage();

    //    ls.chargedLight = 0.03f;

    //    if (rm != null && ls != null)
    //    {
    //        rm.lightLevelNumber -= ls.chargedLight;
    //        rm.lightBarFill.fillAmount -= ls.chargedLight;
    //        rm.waterLevelNumber -= ls.chargedLight;
    //        rm.waterBarFill.fillAmount -= ls.chargedLight;
    //    }
    //    StartCoroutine(DisableArmAfterDelay(0.5f)); // Disable the arm after a certain duration

    //    //interactable = false;
    //    //break;
    //}

    private void Gutter()
    {
        soundmanager.playSFX(soundmanager.cactusPunch);
        arm.SetActive(true); // Activate the arm object
        Debug.Log("punch");
        damageableGutter.TakeDamage();

        ls.chargedLight = 0.03f;

        if (rm != null && ls != null)
        {
            rm.waterLevelNumber -= ls.chargedLight;
            rm.waterBarFill.fillAmount -= ls.chargedLight;
        }

        StartCoroutine(DisableArmAfterDelay(0.5f)); // Disable the arm after a certain duration
    }

    private void Box1()
    {
        soundmanager.playSFX(soundmanager.cactusPunch);
        arm.SetActive(true); // Activate the arm object
        Debug.Log("punch");
        damageableBox1.TakeDamage();

        ls.chargedLight = 0.03f;

        if (rm != null && ls != null)
        {
            rm.waterLevelNumber -= ls.chargedLight;
            rm.waterBarFill.fillAmount -= ls.chargedLight;
        }

        StartCoroutine(DisableArmAfterDelay(0.5f)); // Disable the arm after a certain duration
    }

    private void Box2()
    {
        soundmanager.playSFX(soundmanager.cactusPunch);
        arm.SetActive(true); // Activate the arm object
        Debug.Log("punch");
        damageableBox2.TakeDamage();

        ls.chargedLight = 0.03f;

        if (rm != null && ls != null)
        {
            rm.waterLevelNumber -= ls.chargedLight;
            rm.waterBarFill.fillAmount -= ls.chargedLight;
        }

        StartCoroutine(DisableArmAfterDelay(0.5f)); // Disable the arm after a certain duration
    }

    private void Box3()
    {
        soundmanager.playSFX(soundmanager.cactusPunch);
        arm.SetActive(true); // Activate the arm object
        Debug.Log("punch");
        damageableBox3.TakeDamage();

        ls.chargedLight = 0.03f;

        if (rm != null && ls != null)
        {
            rm.waterLevelNumber -= ls.chargedLight;
            rm.waterBarFill.fillAmount -= ls.chargedLight;
        }

        StartCoroutine(DisableArmAfterDelay(0.5f)); // Disable the arm after a certain duration
    }

    private void Giraffe()
    {
        soundmanager.playSFX(soundmanager.cactusPunch);
        arm.SetActive(true); // Activate the arm object
        Debug.Log("punch");
        damageableGiraffe.TakeDamage();

        ls.chargedLight = 0.03f;

        if (rm != null && ls != null)
        {
            rm.waterLevelNumber -= ls.chargedLight;
            rm.waterBarFill.fillAmount -= ls.chargedLight;
        }

        StartCoroutine(DisableArmAfterDelay(0.5f)); // Disable the arm after a certain duration
    }

    private void Basket()
    {
        soundmanager.playSFX(soundmanager.cactusPunch);
        arm.SetActive(true); // Activate the arm object
        Debug.Log("punch");
        damageableBasket.TakeDamage();

        ls.chargedLight = 0.03f;

        if (rm != null && ls != null)
        {
            rm.waterLevelNumber -= ls.chargedLight;
            rm.waterBarFill.fillAmount -= ls.chargedLight;
        }

        StartCoroutine(DisableArmAfterDelay(0.5f)); // Disable the arm after a certain duration
    }

    private void Garabge()
    {
        soundmanager.playSFX(soundmanager.cactusPunch);
        arm.SetActive(true); // Activate the arm object
        Debug.Log("punch");
        damageableGarbage.TakeDamage();

        ls.chargedLight = 0.03f;

        if (rm != null && ls != null)
        {
            rm.waterLevelNumber -= ls.chargedLight;
            rm.waterBarFill.fillAmount -= ls.chargedLight;
        }

        StartCoroutine(DisableArmAfterDelay(0.5f)); // Disable the arm after a certain duration
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
        //if (interactable && collision.gameObject.CompareTag("Curtain") && ps.whichCharacter == 0)
        //    interactButton.SetActive(true);
        if (interactable && collision.gameObject.CompareTag("Gutter") && ps.whichCharacter == 0)
            interactButton.SetActive(true);
        if (interactable && collision.gameObject.CompareTag("Box1") && ps.whichCharacter == 0)
            interactButton.SetActive(true);
        if (interactable && collision.gameObject.CompareTag("Box2") && ps.whichCharacter == 0)
            interactButton.SetActive(true);
        if (interactable && collision.gameObject.CompareTag("Box3") && ps.whichCharacter == 0)
            interactButton.SetActive(true);
        if (interactable && collision.gameObject.CompareTag("Giraffe") && ps.whichCharacter == 0)
            interactButton.SetActive(true);
        if (interactable && collision.gameObject.CompareTag("Basket") && ps.whichCharacter == 0)
            interactButton.SetActive(true);
        if (interactable && collision.gameObject.CompareTag("Garbage") && ps.whichCharacter == 0)
            interactButton.SetActive(true);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        interactButton.SetActive(false);
    }
}