using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class Code : MonoBehaviour
{
    [SerializeField] private GameObject codeUI;
    [SerializeField] private GameObject codeText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        this.gameObject.SetActive(false);
        codeUI.SetActive(true);
        codeText.SetActive(true);
        StartCoroutine(DisableCodeAfterDelay(0.5f)); // Disable the arm after a certain duration
    }

    private IEnumerator DisableCodeAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        codeText.SetActive(false); // Disable the text object
    }
}
