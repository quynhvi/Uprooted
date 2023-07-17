using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevivePlant : MonoBehaviour
{
    public bool ivyJustRevived = false;
    private PlayerMovement ivyMovementScript;
    private PlayerSwap playerSwapScript;

    [SerializeField] private GameObject aloeVera;

    // Start is called before the first frame update
    void Awake()
    {
        ivyMovementScript = GetComponent<PlayerMovement>();
        playerSwapScript = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<PlayerSwap>();
        ivyMovementScript.enabled = false;
        aloeVera.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("dragable"))
        {
            if (!ivyJustRevived)
            {
                aloeVera.SetActive(true);
                playerSwapScript.possibleCharacters.Add(transform);
                playerSwapScript.SwitchToCharacter(playerSwapScript.possibleCharacters.Count - 1);
                playerSwapScript.whichCharacter = 2;
                ivyJustRevived = true;
                ivyMovementScript.enabled = true;
            }
        }
    }

}