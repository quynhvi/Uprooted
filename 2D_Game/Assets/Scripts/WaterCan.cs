using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCan : MonoBehaviour
{
    public GameObject interactButton;
    private GrabController grabController;

    private void Start()
    {
        grabController = FindObjectOfType<GrabController>(); // Find the GrabController script in the scene
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (grabController != null && grabController.interactable && collision.gameObject.CompareTag("VFT"))
        {
            interactButton.SetActive(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        interactButton.SetActive(false);
    }
}
