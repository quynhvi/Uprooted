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
    private PlayerMovement playerMovement;
    private float verticalInput;

    private void Awake()
    {
        effector = GetComponent<PlatformEffector2D>();
        PlayerSwapScript = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<PlayerSwap>();
        currentPlatformLayer = LayerMask.NameToLayer("Platform");
        playerMovement = GameObject.FindGameObjectWithTag("VFT").GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        currentPlayerLayer = PlayerSwapScript.possibleCharacters[PlayerSwapScript.whichCharacter].gameObject.layer;
        if (IsFalling)
            fallTimer += Time.deltaTime;

        if (verticalInput < -0.5f || Keyboard.current.downArrowKey.isPressed) //Hold Key or joystick down
        {
            print(currentPlayerLayer);
            Physics2D.IgnoreLayerCollision(currentPlayerLayer, currentPlatformLayer, true);
            IsFalling = true;
        }
        else
        {
            Physics2D.IgnoreLayerCollision(currentPlayerLayer, currentPlatformLayer, false);
            IsFalling = false;
            fallTimer = 0f;
        }
    }

    private void OnEnable()
    {
        playerMovement.movementAction.action.Enable();
        playerMovement.movementAction.action.performed += OnMovement;
        playerMovement.movementAction.action.canceled += OnMovementCanceled;
    }

    private void OnDisable()
    {
        playerMovement.movementAction.action.Disable();
        playerMovement.movementAction.action.performed -= OnMovement;
        playerMovement.movementAction.action.canceled -= OnMovementCanceled;
    }

    private void OnMovement(InputAction.CallbackContext context)
    {
        Vector2 movementInput = context.ReadValue<Vector2>();
        if (movementInput.y < 0f)
            verticalInput = movementInput.y;
    }

    private void OnMovementCanceled(InputAction.CallbackContext context)
    {
        verticalInput = 0f;
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
}