using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LightSource : MonoBehaviour
{
    private ResourceManagement ResourceManagement;
    public float chargedLight = 0;

    Soundmanager soundmanager;
    private bool isPlayerInside = false; // Track if the player is inside the light source
    private float timeInLight = 0f; // Track the time the player has been in the light source
    public float delayBeforeSound = 1f; // Adjust this value to change the delay before the sound plays

    void Awake()
    {
        ResourceManagement = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<ResourceManagement>();
        soundmanager = GameObject.FindGameObjectWithTag("Sound").GetComponent<Soundmanager>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Cactus"))
        {
            isPlayerInside = true; // Player enters the light source
            timeInLight = 0f; // Reset the time the player has been in the light source
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Cactus"))
        {
            isPlayerInside = false; // Player exits the light source
            timeInLight = 0f; // Reset the time in light when the player exits
        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (!isPlayerInside) // Check if the player is not inside the light source
            return;

        chargedLight = 0.0005f;
        if (ResourceManagement.lightLevelNumber < 1f && collider.gameObject.CompareTag("Cactus"))
        {
            // Increment the time the player has been in the light source
            timeInLight += Time.deltaTime;

            if (timeInLight >= delayBeforeSound)
            {
                soundmanager.playSFX(soundmanager.lightCharge);
                //Debug.Log("Player passed through light");
                if (ResourceManagement.lightLevelNumber > 1f)
                    return;
                ResourceManagement.lightLevelNumber += chargedLight;
                ResourceManagement.lightBarFill.fillAmount += chargedLight;
                ResourceManagement.waterLevelNumber -= chargedLight;
                ResourceManagement.waterBarFill.fillAmount -= chargedLight;
            }
        }
    }
}