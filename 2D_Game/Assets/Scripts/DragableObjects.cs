using UnityEngine;
using UnityEngine.InputSystem;

public class DragableObjects : MonoBehaviour
{
    [SerializeField] private Transform grabPoint;
    [SerializeField] private Transform rayPoint;
    [SerializeField] private float rayDistance;

    private GameObject grabbedObject;
    private int layerIndex;

    private void Start()
    {
        layerIndex = LayerMask.NameToLayer("DragableObjects");
    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(rayPoint.position, transform.right, rayDistance);

        if (hit.collider != null && hit.collider.gameObject.layer == layerIndex)
        {
            Debug.Log("hit object");
            if (Input.GetKeyDown(KeyCode.I) && grabbedObject == null)
            {
                grabbedObject = hit.collider.gameObject;
                grabbedObject.GetComponent<Rigidbody2D>().isKinematic = true;
                grabbedObject.transform.position = grabPoint.position;
                grabbedObject.transform.SetParent(transform);
            }
            else if (Input.GetKeyDown(KeyCode.I) && grabbedObject != null)
            {
                grabbedObject.GetComponent<Rigidbody2D>().isKinematic = false;
                grabbedObject.transform.SetParent(null);
                grabbedObject = null;
            }
        }
        Debug.DrawRay(rayPoint.position, transform.right * rayDistance);
    }
}

//    private void OnCollisionEnter2D(Collision2D collision)
//    {
//        if (interactable && collision.gameObject.CompareTag("VFT"))
//        {
//            interactButton.SetActive(true);
//        }
//    }

//    private void OnCollisionExit2D(Collision2D collision)
//    {
//        interactButton.SetActive(false);
//    }

//    private T FindAnyObjectByType<T>() where T : MonoBehaviour
//    {
//        T[] objects = FindObjectsOfType<T>();
//        if (objects.Length > 0)
//        {
//            return objects[0];
//        }
//        return null;
//    }
//}