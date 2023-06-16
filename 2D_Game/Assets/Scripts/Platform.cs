using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private PlatformEffector2D effector;
    public float waitTime;
    private Rigidbody2D cactus;
    private Rigidbody2D vft;
    private Rigidbody2D ivy;
    private int currentPlayerLayer;
    private int currentPlatformLayer;
    public PlayerSwap PlayerSwapScript;
    public float fallTimer;
    public bool IsFalling = false;

    private void Awake()
    {
        effector = GetComponent<PlatformEffector2D>();
        PlayerSwapScript = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<PlayerSwap>();
        currentPlatformLayer = LayerMask.NameToLayer("Platform");
        currentPlayerLayer = 0;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        //Debug.Log("Collision with platform detected");
        //check if current player character is colliding with the platform
        if (collision.gameObject.CompareTag("Cactus") || collision.gameObject.CompareTag("VFT") || collision.gameObject.CompareTag("Ivy"))
        {
            //check which element of the possiblecharactersList is the current player character (3x if-statements)
            if (PlayerSwapScript.whichCharacter == 0) //check if cactus
            {
                //get the current player characters layer and store it in the "currentPlayerLayer" variable as a number(int)
                currentPlayerLayer = LayerMask.NameToLayer("Cactus"); //returns the index number of the layermask
                // Debug.Log("Player at Index " + currentPlayerLayer + "is standing on platform" );
            }
            if (PlayerSwapScript.whichCharacter == 1) //check if vft
            {
                currentPlayerLayer = LayerMask.NameToLayer("VFT"); //returns the index number of the layermask
                // Debug.Log("Player at Index " + currentPlayerLayer + "is standing on platform");
            }
            if (PlayerSwapScript.whichCharacter == 2) //check if ivy
            {
                currentPlayerLayer = LayerMask.NameToLayer("Ivy"); //returns the index number of the layermask
                //Debug.Log("Player at Index " + currentPlayerLayer + "is standing on platform");
            }
        }
    }
 
    private void Update()
    {
        if (IsFalling)
            fallTimer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) //Hold Key
        {
            Physics2D.IgnoreLayerCollision(currentPlayerLayer, currentPlatformLayer, true); //Collision of current Player Character and current Platform is ignored
            IsFalling = true;
        }
        if (fallTimer >= 0.2f)
        {
            Physics2D.IgnoreLayerCollision(currentPlayerLayer, currentPlatformLayer, false); //Current Player Character and current Platform are colliding again
            IsFalling = false;
            fallTimer = 0;
        }
    }


}
