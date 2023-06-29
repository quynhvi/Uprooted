using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vines : Interactable
{
    private bool isActive;
    public GameObject arm;
    public GameObject noArm;

    private ResourceManagement rm;
    private LightSource ls;

    //public Sprite vine;
    //public Sprite noVine;

    private SpriteRenderer sr;
    //private bool isVine;
    //
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        rm = FindAnyObjectByType<ResourceManagement>();
        ls = FindAnyObjectByType<LightSource>();
    }
    public override void Interact()
    {

        Debug.Log("Interact() called");
        if (isActive)
        {
            arm.SetActive(false);
            // sr.renderer for ivy arm
            // sr.sprite = noVine;
           //  Debug.Log("isactive");
        }
        else
        {
            ls.chargedLight = 0.03f;
            rm.lightLevelNumber -= ls.chargedLight;
            rm.lightBarFill.fillAmount -= ls.chargedLight;
            rm.waterLevelNumber -= ls.chargedLight;
            rm.waterBarFill.fillAmount -= ls.chargedLight;

            arm.SetActive(true);
            //sr.sprite = vine;
            //Debug.Log("isnotactive");
        }
        isActive = !isActive;
    }

    public void testInteract()
    {
        Debug.Log("testInteractive() called");
        Interact();
    }
}
