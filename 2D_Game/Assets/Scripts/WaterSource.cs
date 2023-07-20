using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterSource : MonoBehaviour
{
    private ResourceManagement RessourceManagement;
    public float chargedWater;
    //public Image waterPuddle;
    // Start is called before the first frame update
    void Awake()
    {
        RessourceManagement = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<ResourceManagement>();
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        chargedWater = 0.0018f;
        if (RessourceManagement.lightLevelNumber < 1f && collider.gameObject.CompareTag("Cactus")) //Ivy erstmal rausgenommen! >>collider.gameObject.CompareTag("Ivy") || collider.gameObject.CompareTag("VFT") |<<
        {
            Debug.Log("Player charging in water");
            if (RessourceManagement.waterLevelNumber > 1f)
                return;
            RessourceManagement.waterLevelNumber += chargedWater;
            RessourceManagement.waterBarFill.fillAmount += chargedWater;
            //waterPuddle.fillAmount -= chargedWater;
        }
    }
}
