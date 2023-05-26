using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwap : MonoBehaviour
{
    public Transform character;
    public List<Transform> possibleCharacters;
    public int whichCharacter;
    public ParticleSystem m_ParticleSystem;
    public CameraFollow cam;
    public Vector3 cameraOffset; // Offset between the character and the camera


    // Start is called before the first frame update
    void Start()
    {
        if (character == null && possibleCharacters.Count >= 1)
        {
            character = possibleCharacters[0];
        }
        Swap();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (whichCharacter == 0)
            {
                whichCharacter = possibleCharacters.Count - 1; // switch in the list
            }
            else
            {
                whichCharacter -= 1;
            }
            Swap();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (whichCharacter == possibleCharacters.Count -1)
            {
                whichCharacter = 0; 
            }
            else
            {
                whichCharacter += 1;
            }
            Swap();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchToCharacter(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchToCharacter(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchToCharacter(2);
        }
    }

    void SwitchToCharacter(int index)
    {
        if (index >= 0 && index < possibleCharacters.Count)
        {
            character.GetComponent<PlayerMovement>().enabled = false;
            character = possibleCharacters[index];
            character.GetComponent<PlayerMovement>().enabled = true;

            m_ParticleSystem.transform.position = character.position; // spawn at player
            m_ParticleSystem.Play();


            cam.SetTarget(character); // Update the camera's target to the newly selected character
        }
    }
  

    public void Swap()
    {
        character = possibleCharacters[whichCharacter];
        character.GetComponent<PlayerMovement>().enabled = true;
        m_ParticleSystem.transform.position = character.position; // spawn at player
        m_ParticleSystem.Play();

        for (int i = 0; i < possibleCharacters.Count; i++)
        {
            if (possibleCharacters[i] !=character)
            {
                possibleCharacters[i].GetComponent<PlayerMovement>().enabled = false; // if character not chosen -> can't move
                Debug.Log("Hallo");
            }
        }
        cam.SetTarget(character);
    }
}
