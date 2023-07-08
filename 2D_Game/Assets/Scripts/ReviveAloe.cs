using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReviveAloe : MonoBehaviour
{
    public bool aloeRevived = false;
    private PlayerMovement aloeMovement;
    private PlayerSwap playerSwapScript;

    // Start is called before the first frame update
    void Awake()
    {
        aloeMovement = GetComponent<PlayerMovement>();
        playerSwapScript = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<PlayerSwap>();
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
            }
        }
    }
}
