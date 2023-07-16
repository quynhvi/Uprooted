using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarcelonaChair : MonoBehaviour
{
    [SerializeField] private GameObject damagedChair;
    [SerializeField] private GameObject chair;

    private void OnCollisionEnter(Collision collision)
    {
        damagedChair.SetActive(true);
        chair.SetActive(false);
    }
}
