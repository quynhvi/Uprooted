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
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Debug.Log("OnTriggerExit2D called");
        if (collision.CompareTag("Ivy"))
        {
            collision.GetComponent<IvyInteract>().NoArmInteractable();
        }
    }
}
