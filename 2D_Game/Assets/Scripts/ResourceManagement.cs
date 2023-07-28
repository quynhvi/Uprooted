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

    // Start is called before the first frame update
    void Awake()
    {
        lightSource = GameObject.FindGameObjectWithTag("light").GetComponent<LightSource>();
        lightBarFill.fillAmount = 0.4f; //Light level is set to 40% at the start of the game
        lightLevelNumber = 0.4f;
        waterBarFill.fillAmount = 0.6f; //Water Level is set to 60% at the start of the game
        waterLevelNumber = 0.6f;

        soundmanger = GameObject.FindGameObjectWithTag("Sound").GetComponent<Soundmanager>();
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
        if (waterLevelNumber >= 0.8 || waterLevelNumber <= 0.2)
        {
            soundmanger.playSFX(soundmanger.lowHPSound);
        }
    }
}
