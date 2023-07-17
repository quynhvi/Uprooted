using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Code : MonoBehaviour
{
    [SerializeField] private GameObject codeUI;
    [SerializeField] private GameObject codeText;

    private bool isCodeCollected = false; // Track if the code is already collected

    private void Update()
    {
        if (gameObject.activeInHierarchy) // Check if the game object is active
        {
            CollectCode();
        }
    }

    private void CollectCode()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.5f);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("VFT"))
            {
                if (!isCodeCollected)
                {
                    isCodeCollected = true;
                    codeUI.SetActive(true);
                    codeText.SetActive(true);
                    StartCoroutine(DisableCodeTextAfterDelay(2f)); // Disable the text after 2 seconds
                    StartCoroutine(DeactivateCodeAfterDelay(2f)); // Deactivate the code object after 2 seconds
                }
                break; // Exit the loop after code is collected
            }
        }
    }

    private IEnumerator DisableCodeTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        codeText.SetActive(false); // Disable the text object
    }

    private IEnumerator DeactivateCodeAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false); // Deactivate the code object
    }
}