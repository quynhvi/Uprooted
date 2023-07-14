using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class PlayerSwap : MonoBehaviour
{
    public Transform character;
    public List<Transform> possibleCharacters;
    public int whichCharacter;
    public ParticleSystem m_ParticleSystem;
    //public CameraFollow cam;
    public CinemachineVirtualCamera cam;

    public InputActionReference swapLeftAction;
    public InputActionReference swapRightAction;
    public InputActionReference switchCharacter1Action;
    public InputActionReference switchCharacter2Action;
    public InputActionReference switchCharacter3Action;
    public InputActionReference switchCharacter4Action;

    void Start()
    {
        if (possibleCharacters.Count > 0)
        {
            whichCharacter = 0;
            character = possibleCharacters[whichCharacter];
            EnableCharacterMovement(character);

            m_ParticleSystem.transform.position = character.position;
            m_ParticleSystem.Play();

            for (int i = 1; i < possibleCharacters.Count; i++)
            {
                DisableCharacterMovement(possibleCharacters[i]);
            }

            //cam.SetTarget(character);
            cam.Follow = character;
        }
        Swap();
    }

    public void OnEnable()
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

        switchCharacter4Action.action.Enable();
        switchCharacter4Action.action.performed += _ => SwitchToCharacter(3);
    }

    public void OnDisable()
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

        switchCharacter4Action.action.Disable();
        switchCharacter4Action.action.performed -= _ => SwitchToCharacter(3);
    }

    public void SwapLeft()
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

    public void SwapRight()
    {
        if (possibleCharacters.Count > 0)
        {
            whichCharacter = (whichCharacter + 1) % possibleCharacters.Count;
            Swap();
        }
    }

    public void SwitchToCharacter(int index)
    {
        if (index >= 0 && index < possibleCharacters.Count)
        {
            whichCharacter = index;

            DisableCharacterMovement(character);

            character = possibleCharacters[index];

            EnableCharacterMovement(character);

            m_ParticleSystem.transform.position = character.position;
            m_ParticleSystem.Play();

            //cam.SetTarget(character); // Update the camera's target to the newly selected character
            cam.Follow = character;
        }
    }

    public void Swap()
    {
        DisableCharacterMovement(character);

        if (possibleCharacters.Count > 0)
        {
            whichCharacter = Mathf.Clamp(whichCharacter, 0, possibleCharacters.Count - 1);
            character = possibleCharacters[whichCharacter];

            if (character != null)
            {
                EnableCharacterMovement(character);

                m_ParticleSystem.transform.position = character.position;
                m_ParticleSystem.Play();

                //cam.SetTarget(character);
                cam.Follow = character;

                for (int i = 0; i < possibleCharacters.Count; i++)
                {
                    if (i != whichCharacter)
                    {
                        DisableCharacterMovement(possibleCharacters[i]);
                    }
                }
            }
        }
    }

    void EnableCharacterMovement(Transform characterTransform)
    {
        var playerMovement = characterTransform.GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.enabled = true;
        }
    }

    void DisableCharacterMovement(Transform characterTransform)
    {
        var playerMovement = characterTransform.GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }
    }
}