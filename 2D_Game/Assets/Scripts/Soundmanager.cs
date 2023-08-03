using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soundmanager : MonoBehaviour
{
    public AudioSource musicSrc;
    public AudioSource sfxSrc;
    public AudioClip backgroundMusic, lightCharge, waterCharge, lowHPSound, bookOpen, bookClose, revive, keyOpen,
        cactusPunch, fabricRip, codeWrong, drawer, jump, jumppad, aloeInteract, keyFound, closet,
        switchCharacter, vftLetGo, vftPickUp;

    private void Start()
    {
        musicSrc.clip = backgroundMusic;
        musicSrc.Play();
    }

    public void playSFX(AudioClip clip)
    {
        sfxSrc.PlayOneShot(clip);
    }
}
