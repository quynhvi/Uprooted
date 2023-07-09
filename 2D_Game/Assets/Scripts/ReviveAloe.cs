using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReviveAloe : MonoBehaviour
{
    public bool aloeRevived = false;
    private PlayerMovement aloeMovement;
    private PlayerSwap playerSwapScript;
    private SpriteRenderer spriteRenderer;

    public Sprite revivedSprite;  // Add the sprite that represents the revived state

    // Start is called before the first frame update
    void Start()
    {
        playerSwapScript = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<PlayerSwap>();
        aloeMovement = GetComponent<PlayerMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        aloeMovement.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("dragable"))
        {
            if (!aloeRevived)
            {
                playerSwapScript.possibleCharacters.Add(transform);
                playerSwapScript.SwitchToCharacter(playerSwapScript.possibleCharacters.Count - 1);
                playerSwapScript.whichCharacter = 3;
                aloeRevived = true;
                aloeMovement.enabled = true;

                // Change the sprite to the revived sprite
                spriteRenderer.sprite = revivedSprite;
            }
        }
    }
}