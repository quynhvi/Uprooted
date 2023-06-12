using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RessourceManagement : MonoBehaviour
{
    public Image _lightBarFill;
    public Image _waterBarFill;
    public float _lightLevelNumber;
    public float _waterLevelNumber;
    public LightSource _lightSource;

    // Start is called before the first frame update
    void Awake()
    {
        _lightSource = GameObject.FindGameObjectWithTag("light").GetComponent<LightSource>();
        _lightBarFill.fillAmount = 0.4f; //Light level is set to 40% at the start of the game
        _lightLevelNumber = 0.4f;
        _waterBarFill.fillAmount = 0.6f; //Water Level is set to 60% at the start of the game
        _waterLevelNumber = 0.6f;
    }

    // Update is called once per frame
    void Update()
    {
        CheckForDeath();
    }

    private void CheckForDeath()
    {
        if (_lightLevelNumber <= 0 || _waterLevelNumber <= 0 || _waterLevelNumber >= 1)
            this.gameObject.GetComponent<GMScript>().GameOver();
    }
}
