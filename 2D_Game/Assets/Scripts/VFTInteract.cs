using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFTInteract : MonoBehaviour
{
    public GameObject interactIcon;
    private Door door;
    private Vector2 boxSize = new Vector2(0.1f, 1f);

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            //Debug.Log("I pressed");
            
        }
    }

    public void VFTInteractable()
    {
        interactIcon.SetActive(true);
    }

    public void noVFTInteractable()
    {
        interactIcon.SetActive(false);
    }
}
