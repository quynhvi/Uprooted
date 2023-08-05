using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soundmanager : MonoBehaviour
{
    public AudioSource musicSrc;
    public AudioSource sfxSrc;
    public AudioClip backgroundMusic, lightCharge, waterCharge, lowHPSound, bookOpen, bookClose, revive, keyOpen,
        cactusPunch, fabricRip, codeWrong, drawer, jump, jumppad, aloeInteract, keyFound, closet,
        switchCharacter, vftLetGo, vftPickUp, cactusWalk, vftWalk, ivyWalk, aloeWalk, ivyVine, land;

    private void Start()
    {
        musicSrc.clip = backgroundMusic;
        musicSrc.loop = true;
        musicSrc.Play();
    }

    public void playSFX(AudioClip clip, float volume = 1f, int priority = 128)
    {
        if (!sfxSrc.isPlaying)
        {
            sfxSrc.volume = volume;
            sfxSrc.priority = priority;
            sfxSrc.PlayOneShot(clip);
        }
    }
}
