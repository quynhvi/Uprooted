using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soundmanager : MonoBehaviour
{
    public AudioSource musicSrc;
    public AudioSource sfxSrc;
    public AudioClip lightCharge, waterCharge, lowHPSound, bookOpen, bookClose, revive, keyOpen,
        cactusPunch, fabricRip, codeWrong, drawer, jump; // backgroundMusic

    private void Start()
    {
        //musicSrc.clip = backgroundMusic;
        //musicSrc.Play();
    }

    public void playSFX(AudioClip clip)
    {
        sfxSrc.PlayOneShot(clip);
    }
}
