using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vines : Interactable
{
    private bool isActive;
    public GameObject arm;
    public GameObject noArm;

    public override void Interact()
    {
        if (isActive)
        {
            arm.SetActive(true);
            // sr.renderer for ivy arm
        }
        else
        {
            noArm.SetActive(false);
        }
        isActive = !isActive;
    }
}
