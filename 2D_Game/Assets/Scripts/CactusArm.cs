using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusArm : Interactable
{
    private bool isActive;
    public GameObject armChild;
    public GameObject noArmChild;
    

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
        if(Input.GetKeyDown(KeyCode.I))
        {
            armChild.SetActive(true);
        }
        else
        {
            armChild.SetActive(false);
        }
        
    }
}
