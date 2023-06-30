using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineTest : MonoBehaviour
{
    public GameObject interactButton;
    public GameObject vine;
    private bool interactable;

    private ResourceManagement rm;
    private LightSource ls;

    // Start is called before the first frame update
    void Start()
    {
        rm = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<ResourceManagement>();
        ls = FindObjectOfType<LightSource>();
        interactable = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.I))
        {
            VineInteract();
        }
    }

    private void VineInteract()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.5f);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Ivy"))
            {
                ls.chargedLight = 0.03f;

                vine.SetActive(true);

                if (rm != null && ls != null)
                {
                    rm.lightLevelNumber -= ls.chargedLight;
                    rm.lightBarFill.fillAmount -= ls.chargedLight;
                    rm.waterLevelNumber -= ls.chargedLight;
                    rm.waterBarFill.fillAmount -= ls.chargedLight;
                }
                interactable = false;
                break; // exit loop after finding Ivy
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (interactable)
            interactButton.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        interactButton.SetActive(false);
    }
}
