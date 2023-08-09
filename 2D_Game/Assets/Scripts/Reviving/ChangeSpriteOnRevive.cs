using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSpriteOnRevive : MonoBehaviour
{
    public Sprite ivyRevivedSprite; // The new sprite to use when Ivy is revived
    public Sprite aloeRevivedSprite; // The new sprite to use when Aloe Vera is revived

    private RevivePlant ivyReviveScript;
    private ReviveAloe aloeReviveScript;

    private Image imageComponent;

    private void Awake()
    {
        imageComponent = GetComponent<Image>();
    }

    private void Start()
    {
        ivyReviveScript = GameObject.FindGameObjectWithTag("Ivy").GetComponent<RevivePlant>();
        aloeReviveScript = GameObject.FindGameObjectWithTag("AloeVera").GetComponent<ReviveAloe>();
    }

    private void Update()
    {
        // Check if Ivy just revived, and update the sprite accordingly
        if (ivyReviveScript.ivyJustRevived)
        {
            imageComponent.sprite = ivyRevivedSprite;
        }

        if (aloeReviveScript.aloeRevived)
        {
            imageComponent.sprite = aloeRevivedSprite;
        }
    }
}