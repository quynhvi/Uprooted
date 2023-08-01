using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSource : MonoBehaviour
{
    private ResourceManagement RessourceManagement;
    public float chargedWater;
    public bool isCharging;

    Soundmanager soundmanager;

    void Awake()
    {
        RessourceManagement = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<ResourceManagement>();
        soundmanager = GameObject.FindGameObjectWithTag("Sound").GetComponent<Soundmanager>();
    }
    private void Start()
    {
        RessourceManagement.waterSources.Add(this);
    }

    private void OnDestroy()
    {
        RessourceManagement.waterSources.Remove(this);
    }
    private void OnTriggerStay2D(Collider2D collider)
    {
        chargedWater = 0.002f;
        if (RessourceManagement.waterLevelNumber < 1f && collider.gameObject.CompareTag("Cactus"))
        {
            soundmanager.playSFX(soundmanager.waterCharge);
            isCharging = true;

            if (RessourceManagement.waterLevelNumber > 1f)
                return;
            RessourceManagement.waterLevelNumber += chargedWater;
            RessourceManagement.waterBarFill.fillAmount += chargedWater;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        chargedWater = 0f; // Reset the chargedWater value when the player exits the water source trigger
        isCharging = false;
    }
}