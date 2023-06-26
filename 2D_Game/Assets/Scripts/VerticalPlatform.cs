using UnityEngine;
using UnityEngine.InputSystem;

public class VerticalPlatform : MonoBehaviour
{
    private PlatformEffector2D effector;
    public float waitTime;
    public Rigidbody2D cactus;
    public Rigidbody2D vft;
    public Rigidbody2D ivy;

    private void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    private void Update()
    {
        if (Keyboard.current.downArrowKey.wasReleasedThisFrame || Keyboard.current.sKey.wasReleasedThisFrame)
        {
            waitTime = 0.3f;
        }

        if (Keyboard.current.downArrowKey.isPressed || Keyboard.current.sKey.isPressed)
        {
            if (waitTime <= 0)
            {
                effector.rotationalOffset = 180f;
                waitTime = 0.5f;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
        if (Keyboard.current.spaceKey.isPressed)
        {
            effector.rotationalOffset = 0;
        }
    }
}