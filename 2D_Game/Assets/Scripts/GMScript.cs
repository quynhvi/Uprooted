using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GMScript : MonoBehaviour
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
        if (Input.GetKeyDown(KeyCode.R) || gamepad != null && gamepad.selectButton.wasPressedThisFrame)
        {
            // Reload the active scene
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
    }
}
