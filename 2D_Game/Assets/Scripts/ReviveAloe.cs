using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ReviveAloe : MonoBehaviour
{
    public bool aloeRevived = false;
    private PlayerSwap playerSwapScript;

    [SerializeField] PlayableDirector aloeDirector;
    private bool cutscenePlayed = false; // Flag to check if the cutscene has been played
    public GameObject playableAloe;
    [SerializeField] private GameObject aloeDead2;

    private Soundmanager soundmanager;

    // Start is called before the first frame update
    void Start()
    {
        playerSwapScript = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<PlayerSwap>();
        soundmanager = GameObject.FindGameObjectWithTag("Sound").GetComponent<Soundmanager>();
    }

    private void Update()
    {
        if (aloeRevived && !cutscenePlayed)
        {
            aloeDirector.Play();
            cutscenePlayed = true;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("dragable"))
        {
            if (!aloeRevived)
            {
                playableAloe.SetActive(true);
                aloeDead2.SetActive(false);
                soundmanager.playSFX(soundmanager.revive);
                playerSwapScript.possibleCharacters.Add(playableAloe.transform);
                playerSwapScript.SwitchToCharacter(playerSwapScript.possibleCharacters.Count - 1);
                playerSwapScript.whichCharacter = 3;
                aloeRevived = true;
            }
        }
    }
}