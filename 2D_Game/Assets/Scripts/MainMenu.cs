using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    public GameObject Point;
    public Animator transition;
    public float transitionTime = 1f;

    private int SelectedButton = 1;
    [SerializeField] private int NumberOfButtons;
    public Animator transitionAnim;

    public Transform ButtonPosition1;
    public Transform ButtonPosition2;
    public Transform ButtonPosition3;

    [SerializeField] private GameObject startFront;
    [SerializeField] private GameObject quitFront;
    [SerializeField] private GameObject creditsFront;
    [SerializeField] private GameObject credits;

    private bool creditsOpen = false;
    private Soundmanager soundmanager;

    private void Start()
    {
        soundmanager = GameObject.FindObjectOfType<Soundmanager>();
        UpdateButtonHighlights();
    }

    private void OnPlay()
    {
        if (SelectedButton == 1)
        {
            soundmanager.playSFX(soundmanager.UIButton);
            Debug.Log("retry");
            LoadNextLevel();
        }
        else if (SelectedButton == 2)
        {
            soundmanager.playSFX(soundmanager.UIButton);
            // When the button with the pointer is clicked, this piece of script is activated
            Application.Quit();
        }
        else if (SelectedButton == 3)
        {
            soundmanager.playSFX(soundmanager.UIButton);
            // When the button with the pointer is clicked, this piece of script is activated
            if (creditsOpen)
            {
                credits.SetActive(false);
                Time.timeScale = 1f;
                creditsOpen = false;
            }
            else
            {
                credits.SetActive(true);
                Time.timeScale = 0f;
                creditsOpen = true;
            }
            Debug.Log("Credits");
        }
    }

    private void OnButtonUp()
    {
        if (!creditsOpen && SelectedButton > 1)
        {
            SelectedButton -= 1;
            soundmanager.playSFX(soundmanager.jump);
            UpdateButtonHighlights();
        }
    }

    private void OnButtonDown()
    {
        if (!creditsOpen && SelectedButton < NumberOfButtons)
        {
            SelectedButton += 1;
            soundmanager.playSFX(soundmanager.jump);
            UpdateButtonHighlights();
        }
    }

    private void UpdateButtonHighlights()
    {
        startFront.SetActive(SelectedButton == 1);
        quitFront.SetActive(SelectedButton == 2);
        creditsFront.SetActive(SelectedButton == 3);
    }

    private void MoveThePointer()
    {
        if (SelectedButton == 1)
        {
            Point.transform.position = ButtonPosition1.position;
        }
        else if (SelectedButton == 2)
        {
            Point.transform.position = ButtonPosition2.position;
        }
        else if (SelectedButton == 3)
        {
            Point.transform.position = ButtonPosition3.position;
        }
    }

    public void LoadNextLevel()
    {
        // When the button with the pointer is clicked, this piece of script is activated
        Time.timeScale = 1f;
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        Debug.Log(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }
}