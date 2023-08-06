using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using static System.TimeZoneInfo;

public class CutsceneManager3 : MonoBehaviour
{
    public PlayableDirector cutsceneDirector;
    public Animator transition;
    public float transitionTime = 1f;

    private void Start()
    {
        // Start the cutscene (e.g., play a timeline)
        StartCutscene();
    }

    private void StartCutscene()
    {
        cutsceneDirector.Play();

        // Subscribe to the stopped event of the PlayableDirector to reactivate UI elements
        cutsceneDirector.stopped += OnCutsceneStopped;
    }

    private void OnCutsceneStopped(PlayableDirector director)
    {
        // Unsubscribe from the stopped event
        director.stopped -= OnCutsceneStopped;

        // Load the first scene (assuming it has build index 0) after the cutscene ends
        //LoadNextLevel();
        SceneManager.LoadScene(4);
    }

    public void LoadNextLevel()
    {
        Time.timeScale = 1f;
        StartCoroutine(LoadLevel(4));
        Debug.Log(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }
}