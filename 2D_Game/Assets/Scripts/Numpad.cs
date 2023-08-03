using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Playables;

public class Numpad : MonoBehaviour
{
    [SerializeField] private GameObject NumpadScreen;

    string code = "256012";
    string number = null;
    int numberIndex = 0;
    string alpha;
    public TMP_Text UIText = null;

    private Soundmanager soundmanager;

    private void Start()
    {
        soundmanager = GameObject.FindObjectOfType<Soundmanager>();
    }

    public void CodeFunction(string numbers)
    {
        numberIndex++;
        number = number + numbers;
        UIText.text = number;
    }

    public void Enter()
    {
        if (number == code)
        {
            Time.timeScale = 1f;
            UIText.text = "Correct";
            
            SceneManager.LoadScene(0);
        }
        else
        {
            soundmanager.playSFX(soundmanager.codeWrong);
            UIText.text = "Wrong Passcode";
        }
    }

    public void Delete()
    {
        numberIndex++;
        number = null;
        UIText.text = number;
    }
}
