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
        ivyMovementScript = this.gameObject.GetComponent<PlayerMovement>();
        playerSwapScript = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<PlayerSwap>();
        ivyMovementScript.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckforRevival();
    }

    private void OnCollisionStay2D(Collision2D contact)
    {
        if (contact.gameObject.CompareTag("WaterCan") && Input.GetKeyDown(KeyCode.I))
        {
            ivyJustRevived = true;
        }
    }

    public void CheckforRevival()
    {
        // If Ivy got interacted with and not already in possibleCharacters, then revive Ivy and enable movement
        if (ivyJustRevived && !playerSwapScript.possibleCharacters.Contains(this.transform))
        {
            playerSwapScript.possibleCharacters.Add(this.transform);
            ivyMovementScript.enabled = true;
        }
        else if (!ivyJustRevived && playerSwapScript.possibleCharacters.Contains(this.transform))
        {
            playerSwapScript.possibleCharacters.Remove(this.transform);
            ivyMovementScript.enabled = false;
        }
    }
}