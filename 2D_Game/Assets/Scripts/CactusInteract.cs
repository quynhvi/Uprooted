using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CactusInteract : MonoBehaviour
{
    
    
    public GameObject interactIcon;
    private Vector2 armSize = new Vector2(5f, 5f);
    private CactusArm armClass;
    //public GameObject arm;
    private Gamepad gamepad;


    // Start is called before the first frame update
    void Start()
    {
        gamepad = Gamepad.current;
    }

    public void ArmInteractable()
    {
        interactIcon.SetActive(true);
    }

    public void NoArmInteractable()
    {
        interactIcon.SetActive(false);
    }


    // Update is called once per frame


    void Update()
    {
        if (Input.GetKey(KeyCode.I) || (gamepad != null && gamepad.buttonWest.wasPressedThisFrame))
        {
            //Debug.Log("I pressed");

            //arm.SetActive(true);
            CheckInteraction();
        }
        else
        {
            //arm.SetActive(false);
        }
    }

    private void CheckInteraction()
    {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(armSize, armSize, 0, Vector2.zero);
        //Debug.Log("CheckInteraction() called");

        if (hits.Length > 0)
        {
            foreach (RaycastHit2D rc in hits)
            {
                if (rc.IsInteractable())
                {

                    //Debug.Log("interactable");
                    Collider2D collider = rc.collider;

                    armClass = collider.GetComponent<CactusArm>();
                    armClass.Interact();
                    //Debug.Log("Interacted");
                    return;
                }

            }
        }

    }
}
