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

    Soundmanager soundmanger;
    private bool isLowHPSoundPlaying = false; // Flag to track if low HP sound is playing
    private AudioSource lowHPSoundAudioSource; // Reference to the AudioSource playing low HP sound

    // Start is called before the first frame update
    void Awake()
    {
        lightSource = GameObject.FindGameObjectWithTag("light").GetComponent<LightSource>();
        lightBarFill.fillAmount = 0.4f; //Light level is set to 40% at the start of the game
        lightLevelNumber = 0.4f;
        waterBarFill.fillAmount = 0.6f; //Water Level is set to 60% at the start of the game
        waterLevelNumber = 0.6f;

        soundmanger = GameObject.FindGameObjectWithTag("Sound").GetComponent<Soundmanager>();
        lowHPSoundAudioSource = soundmanger.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckForDeath();
        LowHP();
    }

    private void CheckForDeath()
    {
        if (lightLevelNumber <= 0 || waterLevelNumber <= 0 || waterLevelNumber >= 1)
            this.gameObject.GetComponent<GMScript>().GameOver();
    }

    private void LowHP()
    {
        // If the water level is below 0.2 or above 0.8, and the sound is not currently playing
        if ((waterLevelNumber <= 0.2f || waterLevelNumber >= 0.8f || lightLevelNumber <= 0.2) && !isLowHPSoundPlaying)
        {
            lowHPSoundAudioSource.PlayOneShot(soundmanger.lowHPSound);
            isLowHPSoundPlaying = true; // Set the flag to true to indicate that the sound is playing
        }
        // If the water level is within the normal range, and the sound is currently playing
        else if (waterLevelNumber > 0.2f && waterLevelNumber < 0.8f && isLowHPSoundPlaying && lightLevelNumber > 0.2)
        {
            lowHPSoundAudioSource.Stop(); // Stop the low HP sound
            isLowHPSoundPlaying = false; // Set the flag to false to indicate that the sound is not playing
        }
    }
}