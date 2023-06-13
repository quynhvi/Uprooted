using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterSource : MonoBehaviour
{
    public RessourceManagement _RessourceManagement;
    public float _chargedWater;
    public Image _waterPuddle;
    // Start is called before the first frame update
    void Awake()
    {
        _RessourceManagement = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<RessourceManagement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collider)
    {
        _chargedWater = 0.0018f;
        if (_RessourceManagement._lightLevelNumber < 1f && collider.gameObject.CompareTag("Cactus")) //Ivy erstmal rausgenommen! >>collider.gameObject.CompareTag("Ivy") || collider.gameObject.CompareTag("VFT") |<<
        {
            Debug.Log("Player charging in water");
            if (_RessourceManagement._waterLevelNumber > 1f)
                return;
            _RessourceManagement._waterLevelNumber += _chargedWater;
            _RessourceManagement._waterBarFill.fillAmount += _chargedWater;
            _waterPuddle.fillAmount -= _chargedWater;
        }
    }
}
