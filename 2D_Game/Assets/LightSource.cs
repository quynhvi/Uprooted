using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LightSource : MonoBehaviour
{
    public RessourceManagement _RessourceManagement;
    public float _chargedLight;
    // Start is called before the first frame update
    void Awake()
    {
        _RessourceManagement = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<RessourceManagement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collider) //Detecting if player characters pass through light and adding charged Light to the Level.
    {
        _chargedLight = 0.0009f;
        if (_RessourceManagement._lightLevelNumber < 1f && collider.gameObject.CompareTag("VFT") || collider.gameObject.CompareTag("Cactus")) //Ivy erstmal rausgenommen! >>collider.gameObject.CompareTag("Ivy") ||<<
        {
            Debug.Log("Player passed through light");
            if (_RessourceManagement._lightLevelNumber > 1f)
                return;
            _RessourceManagement._lightLevelNumber += _chargedLight;
            _RessourceManagement._lightBarFill.fillAmount += _chargedLight;
            _RessourceManagement._waterLevelNumber -= _chargedLight;
            _RessourceManagement._waterBarFill.fillAmount -= _chargedLight;
        }
    }
}
