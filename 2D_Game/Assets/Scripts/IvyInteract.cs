using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IvyInteract : MonoBehaviour
{
    public GameObject interactIcon;
    private Vines vine;
    private Vector2 boxSize = new Vector2(0.1f, 1f);


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("I pressed");
            CheckInteraction();
        }
    }

    public void ArmInteractable()
    {
        interactIcon.SetActive(true);
    }

    public void NoArmInteractable()
    {
        interactIcon.SetActive(false);
    }

    private void CheckInteraction()
    {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, boxSize, 0, Vector2.zero);
        Debug.Log("CheckInteraction() called");

        if (hits.Length > 0)
        {
            foreach (RaycastHit2D rc in hits)
            {
                if (rc.IsInteractable())
                {
                    // Debug.Log("interactable");

                    Collider2D collider = rc.collider;
                    vine = collider.GetComponent<Vines>();
                    vine.Interact();
                    return;
                }

            }
        }
    }
}
