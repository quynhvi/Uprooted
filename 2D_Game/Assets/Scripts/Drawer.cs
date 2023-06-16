using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawer : MonoBehaviour
{
    public GameObject Opened;
    public GameObject Closed;
    public GameObject Drawers;

    public bool isOpen = false;

    public void Start()
    {
        Drawers.transform.position = Closed.transform.position;
        Drawers.GetComponent<BoxCollider2D>().enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("VFT"))
        {
            if (!isOpen)
            {
                OpenDrawer();
                Drawers.GetComponent<BoxCollider2D>().enabled = true;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("VFT"))
        {
            if (isOpen)
            {
                ClosedDrawer();
                Drawers.GetComponent<BoxCollider2D>().enabled = false;

            }
        }
    }
    private void OpenDrawer()
    {
        Drawers.transform.position = Opened.transform.position;
        isOpen = true;
        Debug.Log("Drawer Open");
    }

    private void ClosedDrawer()
    {
        Drawers.transform.position = Closed.transform.position;
        isOpen = false;
        Debug.Log("Drawer Closed");

    }
}
