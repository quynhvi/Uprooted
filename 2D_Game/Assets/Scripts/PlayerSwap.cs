using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSwap : MonoBehaviour
{
    public Transform character;
    public List<Transform> possibleCharacters;
    public int whichCharacter;
    public ParticleSystem m_ParticleSystem;
    public CameraFollow cam;

    public InputActionReference swapLeftAction;
    public InputActionReference swapRightAction;
    public InputActionReference switchCharacter1Action;
    public InputActionReference switchCharacter2Action;
    public InputActionReference switchCharacter3Action;

    void Start()
    {
        if (possibleCharacters.Count > 0)
        {
            whichCharacter = 0;
            character = possibleCharacters[whichCharacter];
            character.GetComponent<PlayerMovement>().enabled = true;

            m_ParticleSystem.transform.position = character.position;
            m_ParticleSystem.Play();

            for (int i = 1; i < possibleCharacters.Count; i++)
            {
                possibleCharacters[i].GetComponent<PlayerMovement>().enabled = false;
            }

            cam.SetTarget(character);
        }
        Swap();
    }

    private void OnEnable()
    {
        swapLeftAction.action.Enable();
        swapLeftAction.action.performed += _ => SwapLeft();

        swapRightAction.action.Enable();
        swapRightAction.action.performed += _ => SwapRight();

        switchCharacter1Action.action.Enable();
        switchCharacter1Action.action.performed += _ => SwitchToCharacter(0);

        switchCharacter2Action.action.Enable();
        switchCharacter2Action.action.performed += _ => SwitchToCharacter(1);

        switchCharacter3Action.action.Enable();
        switchCharacter3Action.action.performed += _ => SwitchToCharacter(2);
    }

    private void OnDisable()
    {
        swapLeftAction.action.Disable();
        swapLeftAction.action.performed -= _ => SwapLeft();

        swapRightAction.action.Disable();
        swapRightAction.action.performed -= _ => SwapRight();

        switchCharacter1Action.action.Disable();
        switchCharacter1Action.action.performed -= _ => SwitchToCharacter(0);

        switchCharacter2Action.action.Disable();
        switchCharacter2Action.action.performed -= _ => SwitchToCharacter(1);

        switchCharacter3Action.action.Disable();
        switchCharacter3Action.action.performed -= _ => SwitchToCharacter(2);
    }

    void SwapLeft()
    {
        if (whichCharacter == 0)
        {
            whichCharacter = possibleCharacters.Count - 1;
        }
        else
        {
            whichCharacter -= 1;
        }
        Swap();
    }

    void SwapRight()
    {
        if (whichCharacter == possibleCharacters.Count - 1)
        {
            whichCharacter = 0;
        }
        else
        {
            whichCharacter += 1;
        }
        Swap();
    }

    void SwitchToCharacter(int index)
    {
        if (index >= 0 && index < possibleCharacters.Count)
        {
            character.GetComponent<PlayerMovement>().enabled = false;
            character = possibleCharacters[index];
            character.GetComponent<PlayerMovement>().enabled = true;

            m_ParticleSystem.transform.position = character.position;
            m_ParticleSystem.Play();

            // Update the currentPlayerLayer in the Platform script
            var platformObjects = GameObject.FindGameObjectsWithTag("Platform");
            foreach (var platformObject in platformObjects)
            {
                var platformScript = platformObject.GetComponent<Platform>();
                platformScript.currentPlayerLayer = character.gameObject.layer;
            }

            cam.SetTarget(character); // Update the camera's target to the newly selected character
        }
    }

    void Swap()
    {
        character.GetComponent<PlayerMovement>().enabled = false;
        character = possibleCharacters[whichCharacter];
        character.GetComponent<PlayerMovement>().enabled = true;

        m_ParticleSystem.transform.position = character.position;
        m_ParticleSystem.Play();

        cam.SetTarget(character);

        for (int i = 0; i < possibleCharacters.Count; i++)
        {
            if (i != whichCharacter)
            {
                possibleCharacters[i].GetComponent<PlayerMovement>().enabled = false;
            }
        }
    }
}