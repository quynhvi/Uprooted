using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSource : MonoBehaviour
{
    private ResourceManagement ResourceManagement;
    public float chargedLight = 0;
    Soundmanager soundmanager;

    private bool isPlayerInside = false;
    private float timeInLight = 0f;
    public float delayBeforeSound = 1f;

    void Awake()
    {
        ResourceManagement = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<ResourceManagement>();
        soundmanager = GameObject.FindGameObjectWithTag("Sound").GetComponent<Soundmanager>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Cactus"))
        {
            isPlayerInside = true;
            timeInLight = 0f;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Cactus"))
        {
            isPlayerInside = false;
            timeInLight = 0f;
        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (!isPlayerInside)
            return;

        chargedLight = 0.0003f;
        if (collider.gameObject.CompareTag("Cactus"))
        {
            timeInLight += Time.deltaTime;

            if (timeInLight >= delayBeforeSound)
            {
                soundmanager.playSFX(soundmanager.lightCharge);
            }

            // Update the light level
            if (ResourceManagement.lightLevelNumber < 1f)
            {
                ResourceManagement.lightLevelNumber += chargedLight;
                ResourceManagement.lightBarFill.fillAmount += chargedLight;
            }

            // Update the water level
            if (ResourceManagement.waterLevelNumber > 0f)
            {
                ResourceManagement.waterLevelNumber -= chargedLight;
                ResourceManagement.waterBarFill.fillAmount -= chargedLight;
            }
        }
    }
}