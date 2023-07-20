using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneManager : MonoBehaviour
{
    public GameObject[] uiElementsToDeactivate; // Array of UI elements to be deactivated during the cutscene
    public PlayableDirector cutsceneDirector;
    [SerializeField] private GameObject journalTip;

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