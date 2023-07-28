using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenLamp : MonoBehaviour
{
    [SerializeField] private GameObject brokenLamp;
    [SerializeField] private GameObject halfBrokenLamp;
    [SerializeField] private GameObject lamp;
    [SerializeField] private GameObject colliderLamp;

    private bool isBroken = false;
    private int playerCount = 0;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Cactus") || other.gameObject.CompareTag("VFT") || other.gameObject.CompareTag("Ivy") || other.gameObject.CompareTag("AloeVera"))
        {
            playerCount++;
        }

        if (playerCount == 1 && !isBroken)
        {
            lamp.SetActive(false);
            colliderLamp.SetActive(false);
            isBroken = true;
        }
    }
}