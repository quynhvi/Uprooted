using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

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
    //public Transform ButtonPosition3;
    //public Transform ButtonPosition4;

    private void OnPlay()
    {
        if (SelectedButton == 1)
        {
            LoadNextLevel();
            
        }
        else if (SelectedButton == 2)
        {
            // When the button with the pointer is clicked, this piece of script is activated
            Application.Quit();
        }
    }
    private void OnButtonUp()
    {
        // Checks if the pointer needs to move down or up, in this case the poiter moves up one button
        if (SelectedButton > 1)
        {
            SelectedButton -= 1;
        }
        MoveThePointer();
        return;
    }
    private void OnButtonDown()
    {
        // Checks if the pointer needs to move down or up, in this case the poiter moves down one button
        if (SelectedButton < NumberOfButtons)
        {
            SelectedButton += 1;
        }
        MoveThePointer();
        return;
    }
    private void MoveThePointer()
    {
        // Moves the pointer
        if (SelectedButton == 1)
        {
            Point.transform.position = ButtonPosition1.position;
        }
        else if (SelectedButton == 2)
        {
            Point.transform.position = ButtonPosition2.position;
        }
        //else if (SelectedButton == 3)
        //{
        //    Point.transform.position = ButtonPosition3.position;
        //}
        //else if (SelectedButton == 4)
        //{
        //    Point.transform.position = ButtonPosition4.position;
        //}
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
