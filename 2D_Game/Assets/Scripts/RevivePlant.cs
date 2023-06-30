using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevivePlant : MonoBehaviour
{
    public bool ivyJustRevived = false;
    private PlayerMovement ivyMovementScript;
    private PlayerSwap playerSwapScript;

    // Start is called before the first frame update
    void Awake()
    {
        ivyMovementScript = GetComponent<PlayerMovement>();
        playerSwapScript = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<PlayerSwap>();
        ivyMovementScript.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckforRevival();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("dragable"))
        {
            if (!ivyJustRevived)
            {
                ivyJustRevived = true;
                ivyMovementScript.enabled = true;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("dragable"))
        {
            //ivyJustRevived = false;
            ivyMovementScript.enabled = false;
        }
    }

    public void CheckforRevival()
    {
        if (ivyJustRevived && !playerSwapScript.possibleCharacters.Contains(transform))
        {
            playerSwapScript.possibleCharacters.Add(transform);
            if (!ivyMovementScript.enabled) // Enable movement only if the script is currently disabled
            {
                ivyMovementScript.enabled = true;
            }
        }
        else if (!ivyJustRevived && playerSwapScript.possibleCharacters.Contains(transform))
        {
            playerSwapScript.possibleCharacters.Remove(transform);
            if (ivyMovementScript.enabled) // Disable movement only if the script is currently enabled
            {
                ivyMovementScript.enabled = false;
            }
        }
    }
}