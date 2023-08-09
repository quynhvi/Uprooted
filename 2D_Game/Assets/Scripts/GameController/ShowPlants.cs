using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPlants : MonoBehaviour
{
    [SerializeField] private GameObject plants;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShowPlantsAfterDelay());
    }

    private IEnumerator ShowPlantsAfterDelay()
    {
        Debug.Log("Coroutine started. Waiting for 30 seconds.");
        yield return new WaitForSeconds(20);

        // Show the plants GameObject
        plants.SetActive(true);
    }
}