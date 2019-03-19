using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixMusic : MonoBehaviour
{
    public AudioSource chip;
    public AudioSource piano;
    public AudioSource classic;
    public AudioSource dance;
    public AudioSource onClickSound;

    private void Start()
    {
        //set chosen music to full volume
        chip.volume = 1f;
        //set other music tracks to zero volume
        piano.volume = 0f;
        classic.volume = 0f;
        dance.volume = 0f;
    }



    public void Chiptune()
    {
        //Play button sound
        onClickSound.Play();
        //set chosen music to full volume
        chip.volume = 1f;
        //set other music tracks to zero volume
        piano.volume = 0f;
        classic.volume = 0f;
        dance.volume = 0f;
    }

    public void Piano()
    {
        onClickSound.Play();
        chip.volume = 0f;
        piano.volume = 1f;
        classic.volume = 0f;
        dance.volume = 0f;
    }

    public void Classic()
    {
        onClickSound.Play();
        chip.volume = 0f;
        piano.volume = 0f;
        classic.volume = 1f;
        dance.volume = 0f;
    }

    public void DanceDance()
    {
        onClickSound.Play();
        chip.volume = 0f;
        piano.volume = 0f;
        classic.volume = 0f;
        dance.volume = 0.75f;
    }
}
