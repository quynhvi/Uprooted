using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vines : Interactable
{
    private bool isActive;
    //public GameObject arm;
    //public GameObject noArm;

    public Sprite vine;
    public Sprite noVine;

    private SpriteRenderer sr;
    //private bool isVine;
    //
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    public override void Interact()
    {
        Debug.Log("Interact() called");
        if (isActive)
        {
            //arm.SetActive(false);
            // sr.renderer for ivy arm
            sr.sprite = noVine;
            Debug.Log("isactive");
        }
        else
        {
            //noArm.SetActive(true);
            sr.sprite = vine;
            Debug.Log("isnotactive");
        }
        isActive = !isActive;
    }
}
