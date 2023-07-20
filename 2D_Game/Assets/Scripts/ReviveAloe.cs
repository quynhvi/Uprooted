using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ReviveAloe : MonoBehaviour
{
    public bool aloeRevived = false;
    private PlayerMovement aloeMovement;
    private PlayerSwap playerSwapScript;
    private SpriteRenderer spriteRenderer;

    [SerializeField] PlayableDirector aloeDirector;
    private bool cutscenePlayed = false; // Flag to check if the cutscene has been played
    [SerializeField] private GameObject code2;


    public Sprite revivedSprite;  // Add the sprite that represents the revived state

    // Start is called before the first frame update
    void Start()
    {
        playerSwapScript = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<PlayerSwap>();
        aloeMovement = GetComponent<PlayerMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        aloeMovement.enabled = false;
    }

    private void Update()
    {
        if (aloeRevived && !cutscenePlayed)
        {
            aloeDirector.Play();
            cutscenePlayed = true;
            code2.SetActive(true);
        }

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