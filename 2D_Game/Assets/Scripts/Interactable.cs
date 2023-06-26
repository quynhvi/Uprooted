using UnityEngine;

[RequireComponent (typeof(BoxCollider2D))]

public abstract class Interactable : MonoBehaviour
{
    private void Reset()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    public abstract void Interact();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Debug.Log("OnTriggerEnter2D called");
        if (collision.CompareTag("Ivy"))
        {
            collision.GetComponent<IvyInteract>().ArmInteractable();
        }

        //if (collision.CompareTag("VFT"))
        //{
        //    collision.GetComponent<VFTInteract>().VFTInteractable();
        //}
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Debug.Log("OnTriggerExit2D called");
        if (collision.CompareTag("Ivy"))
        {
            collision.GetComponent<IvyInteract>().NoArmInteractable();
        }

        if (collision.CompareTag("VFT"))
        {
            collision.GetComponent<VFTInteract>().noVFTInteractable();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "VFT")
        {
            collision.gameObject.GetComponent<VFTInteract>().VFTInteractable();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "VFT")
        {
            collision.gameObject.GetComponent<VFTInteract>().noVFTInteractable();
        }
    }
}
