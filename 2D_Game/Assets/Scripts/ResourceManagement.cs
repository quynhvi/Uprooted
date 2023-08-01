using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManagement : MonoBehaviour
{
    public Image lightBarFill;
    public Image waterBarFill;
    public float lightLevelNumber;
    public float waterLevelNumber;
    public LightSource lightSource;
    public List<WaterSource> waterSources = new List<WaterSource>();

    public bool IsChargingWater = false;

    Soundmanager soundmanger;
    private bool lowHPPlayed = false; // Flag to track if low HP sound has been played
    public Animator animator;

    // Start is called before the first frame update
    void Awake()
    {
        lightSource = GameObject.FindGameObjectWithTag("light").GetComponent<LightSource>();
        lightBarFill.fillAmount = 0.4f; // Light level is set to 40% at the start of the game
        lightLevelNumber = 0.4f;
        waterBarFill.fillAmount = 0.6f; // Water Level is set to 60% at the start of the game
        waterLevelNumber = 0.6f;
        soundmanger = GameObject.FindGameObjectWithTag("Sound").GetComponent<Soundmanager>();
    }

    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        CheckForDeath();
        LowHP();
        Debug.Log(waterLevelNumber);
    }

    private void CheckForDeath()
    {
        if (lightLevelNumber <= 0 || waterLevelNumber <= 0 || waterLevelNumber > 1)
            this.gameObject.GetComponent<GMScript>().GameOver();
    }

    private void LowHP()
    {
        IsChargingWater = false;
        foreach (WaterSource waterSource in waterSources)
        {
            if (waterSource.isCharging)
            {
                IsChargingWater = true;
            }
        }
        // If the water level is below 0.3 or above 0.7, and the sound has not been played yet
        if (waterLevelNumber <= 0.3f)
        {
            if (!lowHPPlayed)
            {
                soundmanger.playSFX(soundmanger.lowHPSound);
                lowHPPlayed = true; // Set the flag to true to prevent playing the sound repeatedly
            }
            animator.SetBool("LowHealth", true); // Set the "LowHealth" parameter to true
        }
        else if (waterLevelNumber >= 0.6f && IsChargingWater)
        {
            if (!lowHPPlayed)
            {
                soundmanger.playSFX(soundmanger.lowHPSound);
                lowHPPlayed = true; // Set the flag to true to prevent playing the sound repeatedly
            }
            animator.SetBool("HighHealth", true); // Set the "HighHealth" parameter to true
        }
        else
        {
            animator.SetBool("LowHealth", false); // Set the "LowHealth" parameter to false
            animator.SetBool("HighHealth", false); // Set the "HighHealth" parameter to false
            lowHPPlayed = false; // Reset the flag
        }
    }
}