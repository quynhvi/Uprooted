using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusArm : Interactable
{
    private bool isActive;
    public GameObject armChild;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact()
    {
        if(isActive)
        {
            armChild.SetActive(false);
        }
        else
        {
            armChild.SetActive(true);
        }
        isActive = !isActive;
    }
}
