using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class RevivePlant : MonoBehaviour
{
    public bool ivyJustRevived = false;
    private PlayerMovement ivyMovementScript;
    private PlayerSwap playerSwapScript;

    private SpriteRenderer spriteRenderer;
    public Sprite revivedSprite;

    [SerializeField] private GameObject aloeVera;
    [SerializeField] private GameObject fakeAloe;
    [SerializeField] private GameObject ivySpeech;
    [SerializeField] private PlayableDirector ivyDirector;
    private bool cutscenePlayed = false; // Flag to check if the cutscene has been played

    private Soundmanager soundmanager;

    // Start is called before the first frame update
    void Awake()
    {
        ivyMovementScript = GetComponent<PlayerMovement>();
        playerSwapScript = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<PlayerSwap>();
        ivyMovementScript.enabled = false;
        aloeVera.SetActive(false);
        spriteRenderer = GetComponent<SpriteRenderer>();
        soundmanager = GameObject.FindGameObjectWithTag("Sound").GetComponent<Soundmanager>();
    }

    private void Update()
    {
        if (ivyJustRevived && !cutscenePlayed)
        {
            ivyDirector.Play();
            cutscenePlayed = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("dragable"))
        {
            if (!ivyJustRevived)
            {
                soundmanager.playSFX(soundmanager.revive);
                aloeVera.SetActive(true);
                fakeAloe.SetActive(false);
                ivySpeech.SetActive(false);
                playerSwapScript.possibleCharacters.Add(transform);
                playerSwapScript.SwitchToCharacter(playerSwapScript.possibleCharacters.Count - 1);
                playerSwapScript.whichCharacter = 2;
                ivyJustRevived = true;
                ivyMovementScript.enabled = true;

                spriteRenderer.sprite = revivedSprite;
            }
        }
    }

}