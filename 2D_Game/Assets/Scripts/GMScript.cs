using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GMScript : MonoBehaviour
{
    [SerializeField] private GameObject journalScreen;

    private Gamepad gamepad;
    public InputActionReference journalAction;
    public InputActionReference restartAction;

    MenuInput action;
    private bool paused;

    private void Awake()
    {
        action = new MenuInput();
    }

    // Start is called before the first frame update
    void Start()
    {
        //gamepad = Gamepad.current;
        action.Menu.Journal.performed += _ => DeterminePause();
    }

    private void DeterminePause()
    {
        if (paused)
        {
            CloseJournal();
        }
        else
        {
            OpenJournal();
        }
    }

    private void OnEnable()
    {
        action.Enable();
        

        restartAction.action.Enable();
        restartAction.action.performed += Restart;
    }

    private void OnDisable()
    {
        action.Disable();
        
    }

    public void OpenJournal()
    {
        journalScreen.SetActive(true);
        paused = true;
        Time.timeScale = 0f;
    }

    public void CloseJournal()
    {
        Time.timeScale = 1f;
        paused = false;
        journalScreen.SetActive(false);   
    }

    public void Restart(InputAction.CallbackContext context)
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    //// Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.R) || gamepad != null && gamepad.selectButton.wasPressedThisFrame)
    //    {
    //        // Reload the active scene
    //        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    //    }
    //}

    public void GameOver()
    {
        Debug.Log("Game Over");
    }
}
