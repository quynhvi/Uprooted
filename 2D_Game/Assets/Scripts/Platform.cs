using UnityEngine;
using UnityEngine.InputSystem;

public class Platform : MonoBehaviour
{
    private PlatformEffector2D effector;
    public float waitTime;
    private Rigidbody2D cactus;
    private Rigidbody2D vft;
    private Rigidbody2D ivy;
    public int currentPlayerLayer;
    public int currentPlatformLayer;
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
        if (collision.gameObject.CompareTag("Cactus") || collision.gameObject.CompareTag("VFT") || collision.gameObject.CompareTag("Ivy"))
        {
            if (PlayerSwapScript.whichCharacter == 0) //check if cactus
            {
                currentPlayerLayer = LayerMask.NameToLayer("Cactus");
            }
            if (PlayerSwapScript.whichCharacter == 1) //check if vft
            {
                currentPlayerLayer = LayerMask.NameToLayer("VFT");
            }
            if (PlayerSwapScript.whichCharacter == 2) //check if ivy
            {
                currentPlayerLayer = LayerMask.NameToLayer("Ivy");
            }
        }
    }

    private void Update()
    {
        if (IsFalling)
            fallTimer += Time.deltaTime;

        if (Keyboard.current.downArrowKey.wasPressedThisFrame || Keyboard.current.sKey.wasPressedThisFrame) //Hold Key
        {
            Physics2D.IgnoreLayerCollision(currentPlayerLayer, currentPlatformLayer, true);
            IsFalling = true;
        }
        if (fallTimer >= 0.2f)
        {
            Physics2D.IgnoreLayerCollision(currentPlayerLayer, currentPlatformLayer, false);
            IsFalling = false;
            fallTimer = 0;
        }
    }
}
