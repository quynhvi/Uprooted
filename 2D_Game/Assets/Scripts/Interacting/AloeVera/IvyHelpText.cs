using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IvyHelpText : MonoBehaviour
{
    [SerializeField] private GameObject ivyText;
    private RevivePlant revivePlant;

    private void Start()
    {
        revivePlant = GameObject.FindGameObjectWithTag("Ivy").GetComponent<RevivePlant>();
        ivyText.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!revivePlant.ivyJustRevived)
        {
            if (collision.CompareTag("Cactus") || collision.CompareTag("VFT"))
            {
                ivyText.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ivyText.SetActive(false);
    }
}
