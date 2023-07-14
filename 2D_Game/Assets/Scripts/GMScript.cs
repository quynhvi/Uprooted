using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GMScript : MonoBehaviour
{
    [SerializeField] private GameObject journalScreen;
    [SerializeField] private GameObject GOScreen;
    [SerializeField] private GameObject winScreen;

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
        Time.timeScale = 1f;
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
        SceneManager.LoadScene(2);
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
        GOScreen.SetActive(true);
        Time.timeScale = 0f;
    }
}
