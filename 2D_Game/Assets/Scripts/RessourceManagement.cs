using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RessourceManagement : MonoBehaviour
{
    public Image lightBarFill;
    public Image waterBarFill;
    public float lightLevelNumber;
    public float waterLevelNumber;
    public LightSource _lightSource;

    // Start is called before the first frame update
    void Awake()
    {
        _lightSource = GameObject.FindGameObjectWithTag("light").GetComponent<LightSource>();
        lightBarFill.fillAmount = 0.4f; //Light level is set to 40% at the start of the game
        lightLevelNumber = 0.4f;
        waterBarFill.fillAmount = 0.6f; //Water Level is set to 60% at the start of the game
        waterLevelNumber = 0.6f;
    }

    // Update is called once per frame
    void Update()
    {
        CheckForDeath();
    }

    private void CheckForDeath()
    {
        if (lightLevelNumber <= 0 || waterLevelNumber <= 0 || waterLevelNumber >= 1)
            this.gameObject.GetComponent<GMScript>().GameOver();
    }
}
