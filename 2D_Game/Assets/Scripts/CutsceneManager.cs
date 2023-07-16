using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneManager : MonoBehaviour
{
    public GameObject[] uiElementsToDeactivate; // Array of UI elements to be deactivated during the cutscene
    public PlayableDirector cutsceneDirector;

    private void Start()
    {
        // Deactivate UI elements at the start of the cutscene
        DeactivateUIElements();

        // Start the cutscene (e.g., play a timeline)
        StartCutscene();
    }

    private void StartCutscene()
    {
        // Play your cutscene timeline or perform any other cutscene-related logic here
        cutsceneDirector.Play();

        // Subscribe to the stopped event of the PlayableDirector to reactivate UI elements
        cutsceneDirector.stopped += OnCutsceneStopped;
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

        // Reactivate each UI element in the array
        foreach (GameObject uiElement in uiElementsToDeactivate)
        {
            uiElement.SetActive(true);
        }
    }
}