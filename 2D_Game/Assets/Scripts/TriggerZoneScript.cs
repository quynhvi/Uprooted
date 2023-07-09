using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZoneScript : MonoBehaviour
{
    private Rigidbody2D cactus;
    private Rigidbody2D vft;
    private Rigidbody2D ivy;
    public int currentPlayerLayer;
    public int closetBorderLayer;
    public PlayerSwap PlayerSwapScript;

    // Start is called before the first frame update
    void Awake()
    {
        PlayerSwapScript = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<PlayerSwap>();
        closetBorderLayer = LayerMask.NameToLayer("Closet Borders");
        IgnoreAll();
    }

    // Update is called once per frame
    void Update()
    {
        currentPlayerLayer = PlayerSwapScript.possibleCharacters[PlayerSwapScript.whichCharacter].gameObject.layer;
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (PlayerSwapScript.whichCharacter == 0 && collider.gameObject.CompareTag("Cactus")) //check if cactus
        {
            currentPlayerLayer = LayerMask.NameToLayer("Cactus");
            Physics2D.IgnoreLayerCollision(currentPlayerLayer, closetBorderLayer, false);
        }
        if (PlayerSwapScript.whichCharacter == 1 && collider.gameObject.CompareTag("VFT")) //check if vft
        {
            currentPlayerLayer = LayerMask.NameToLayer("VFT");
            Physics2D.IgnoreLayerCollision(currentPlayerLayer, closetBorderLayer, false);;
        }
        if (PlayerSwapScript.whichCharacter == 2 && collider.gameObject.CompareTag("Ivy")) //check if ivy
        {
            currentPlayerLayer = LayerMask.NameToLayer("Ivy");
            Physics2D.IgnoreLayerCollision(currentPlayerLayer, closetBorderLayer, false);
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Cactus") || collider.gameObject.CompareTag("VFT") || collider.gameObject.CompareTag("Ivy"))
        {
            if (PlayerSwapScript.whichCharacter == 0) //check if cactus
            {
                Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Cactus"), closetBorderLayer, true);
            }
            if (PlayerSwapScript.whichCharacter == 1) //check if vft
            {
                Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("VFT"), closetBorderLayer, true);
            }
            if (PlayerSwapScript.whichCharacter == 2) //check if ivy
            {
                Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Ivy"), closetBorderLayer, true);
            }
        }
    }
    private void IgnoreAll()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Cactus"), closetBorderLayer, true);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("VFT"), closetBorderLayer, true);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Ivy"), closetBorderLayer, true);
    }
}
