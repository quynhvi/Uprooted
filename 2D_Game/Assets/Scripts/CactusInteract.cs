using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusInteract : MonoBehaviour
{
    
    public GameObject arm;
    private Vector2 armSize = new Vector2(1f, 0.1f);
    private CactusArm armClass;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("I pressed");

            arm.SetActive(true);
            CheckInteraction();
            
            

        }
        else
        {
            arm.SetActive(false);
        }
        
    }

    private void CheckInteraction()
    {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(armSize, armSize, 0, Vector2.zero);
        Debug.Log("CheckInteraction() called");

        if (hits.Length > 0)
        {
            foreach (RaycastHit2D rc in hits)
            {
                if (rc.IsInteractable())
                {
                    

                    Collider2D collider = rc.collider;

                    armClass = collider.GetComponent<CactusArm>();
                    armClass.Interact();
                    return;
                }

            }
        }

    }
}
