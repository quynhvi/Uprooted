using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{
    private Gamepad gamepad;
    // Start is called before the first frame update
    void Start()
    {
        gamepad = Gamepad.current;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M) || gamepad != null && gamepad.buttonEast.wasPressedThisFrame)
        {
            SceneManager.LoadScene(0);
            Debug.Log("quit");
        }
    }
}
