using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Users;

public class GamepadCursor : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private RectTransform cursorTransform;
    [SerializeField] private float cursorSpeed = 1000f;
    [SerializeField] private RectTransform canvasRectTransform;
    [SerializeField] private Canvas canvas;



    private Mouse virtualMouse;
    private bool previousMouseState;
    private Camera mainCamera;

    private void OnEnable()
    {
        mainCamera = Camera.main;

        if (virtualMouse == null)
        {
            virtualMouse = (Mouse) InputSystem.AddDevice("VirtualMouse");
        }
        else if (!virtualMouse.added)
        {
            InputSystem.AddDevice(virtualMouse);
        }

        InputUser.PerformPairingWithDevice(virtualMouse, playerInput.user);

        if (cursorTransform != null)
        {
            Vector2 position = cursorTransform.anchoredPosition;
            InputState.Change(virtualMouse.position, position);
        }

        InputSystem.onAfterUpdate += UpdateMotion;
    }

    private void OnDisable()
    {
        InputSystem.onAfterUpdate -= UpdateMotion;
    }

    private void UpdateMotion()
    {
        if (virtualMouse == null || Gamepad.current == null)
        {
            return;
        }

        Vector2 deltaValue = Gamepad.current.leftStick.ReadValue();
        deltaValue *= cursorSpeed * Time.deltaTime;

        Vector2 currentPosition = virtualMouse.position.ReadValue();
        Vector2 newPosition = currentPosition + deltaValue;

        newPosition.x = Mathf.Clamp(newPosition.x, 0, Screen.width);
        newPosition.x = Mathf.Clamp(newPosition.y, 0, Screen.height);

        InputState.Change(virtualMouse.position, newPosition);
        InputState.Change(virtualMouse.delta, deltaValue);

        bool southButtonPressed = Gamepad.current.buttonSouth.IsPressed();

        if (previousMouseState != southButtonPressed)
        {
            virtualMouse.CopyState<MouseState>(out var mouseState);
            mouseState.WithButton(MouseButton.Left, southButtonPressed);
            InputState.Change(virtualMouse, mouseState);
            previousMouseState = southButtonPressed;
        }

        AnchorCursor(newPosition);
    }

    private void AnchorCursor(Vector2 position)
    {
        Vector2 anchoredPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, position,
            canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : mainCamera, out anchoredPosition);
        cursorTransform.anchoredPosition = anchoredPosition;
    }
}
