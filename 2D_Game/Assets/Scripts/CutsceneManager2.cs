using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneManager2 : MonoBehaviour
{
    public GameObject[] uiElementsToDeactivate; // Array of UI elements to be deactivated during the cutscene
    public PlayableDirector cutsceneDirector;
    [SerializeField] private GameObject journalTip;

    private bool cutscenePlaying = false; // Flag to check if the cutscene is currently playing

    private void Start()
    {
        

        // Subscribe to the stopped event of the PlayableDirector to reactivate UI elements
        cutsceneDirector.stopped += OnCutsceneStopped;
    }

    private void Update()
    {
        // Check if the cutscene is currently playing
        cutscenePlaying = cutsceneDirector.state == PlayState.Playing;

        // Deactivate UI elements while the cutscene is playing
        if (cutscenePlaying)
        {
            foreach (GameObject uiElement in uiElementsToDeactivate)
            {
                uiElement.SetActive(false);
            }
        }
    }

    private void DeactivateUIElements()
    {
        // Deactivate each UI element in the array
        foreach (GameObject uiElement in uiElementsToDeactivate)
        {
            uiElement.SetActive(false);
        }
    }

    private void OnCutsceneStopped(PlayableDirector director)
    {
        // Unsubscribe from the stopped event
        director.stopped -= OnCutsceneStopped;
        journalTip.SetActive(true);
        StartCoroutine(DisableTextAfterDelay(5f));

        // Reactivate each UI element in the array
        foreach (GameObject uiElement in uiElementsToDeactivate)
        {
            uiElement.SetActive(true);
        }
    }

    private IEnumerator DisableTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        journalTip.SetActive(false); // Disable the arm object
    }
}