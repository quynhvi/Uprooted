using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LightSource : MonoBehaviour
{
    private ResourceManagement ResourceManagement;
    public float chargedLight = 0;
    // Start is called before the first frame update
    void Awake()
    {
        ResourceManagement = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<ResourceManagement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collider) //Detecting if player characters pass through light and adding charged Light to the Level.
    {
        chargedLight = 0.0009f;
        if (ResourceManagement.lightLevelNumber < 1f && collider.gameObject.CompareTag("Cactus")) //Ivy erstmal rausgenommen! >>collider.gameObject.CompareTag("Ivy") || collider.gameObject.CompareTag("VFT") ||<<
        {
            Debug.Log("Player passed through light");
            if (ResourceManagement.lightLevelNumber > 1f)
                return;
            ResourceManagement.lightLevelNumber += chargedLight;
            ResourceManagement.lightBarFill.fillAmount += chargedLight;
            ResourceManagement.waterLevelNumber -= chargedLight;
            ResourceManagement.waterBarFill.fillAmount -= chargedLight;
        }
    }
}
