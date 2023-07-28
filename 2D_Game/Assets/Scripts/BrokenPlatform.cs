using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenPlatform : MonoBehaviour
{
    [SerializeField] private GameObject brokenPlatform;
    [SerializeField] private GameObject halfBrokenPlatform;
    [SerializeField] private GameObject platform;
    [SerializeField] private GameObject colliderPlatform;

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
            platform.SetActive(false);
            halfBrokenPlatform.SetActive(true);
        }

        if (playerCount >= 2 && !isBroken)
        {
            isBroken = true;
            brokenPlatform.SetActive(true);
            halfBrokenPlatform.SetActive(false);
            platform.SetActive(false);
            colliderPlatform.SetActive(false);
        }
        else if (playerCount < 2 && isBroken)
        {
            isBroken = false;
            brokenPlatform.SetActive(false);
            platform.SetActive(true);
        }
    }
}