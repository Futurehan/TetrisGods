using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixMusic : MonoBehaviour
{
    public AudioSource chip;
    public AudioSource piano;
    public AudioSource classic;
    public AudioSource dance;
    public AudioSource onClickSoundSourcec;
    public AudioClip clickSound;
    public bool keepFadingIn;
    public bool keepFadingOut;

    public float musicVolume;

    private void Start()
    {
        //set chosen music to full volume
        StartCoroutine(FadeIn(chip, 0.08f, musicVolume));
        //chip.volume = musicVolume;
        //set other music tracks to zero volume
        piano.volume = 0f;
        classic.volume = 0f;
        dance.volume = 0f;
    }



    public void Chiptune()
    {
        //Play button sound
        onClickSoundSourcec.PlayOneShot(clickSound);
        //start fade in corutine
        StartCoroutine(FadeIn(chip, 0.08f, musicVolume));
        //start fade out corutines
        StartCoroutine(FadeOut(piano, 0.08f, 0));
        StartCoroutine(FadeOut(classic, 0.08f, 0));
        StartCoroutine(FadeOut(dance, 0.08f, 0));
    }

    public void Piano()
    {
        StartCoroutine(FadeIn(piano, 0.08f, musicVolume));
        StartCoroutine(FadeOut(chip, 0.08f, 0));
        StartCoroutine(FadeOut(classic, 0.08f, 0));
        StartCoroutine(FadeOut(dance, 0.08f, 0));
        onClickSoundSourcec.PlayOneShot(clickSound);
    }

    public void Classic()
    {
        onClickSoundSourcec.PlayOneShot(clickSound);
        StartCoroutine(FadeIn(classic, 0.08f, musicVolume));
        StartCoroutine(FadeOut(chip, 0.08f, 0));
        StartCoroutine(FadeOut(piano, 0.08f, 0));
        StartCoroutine(FadeOut(dance, 0.08f, 0));
    }

    public void DanceDance()
    {
        onClickSoundSourcec.PlayOneShot(clickSound);
        StartCoroutine(FadeIn(dance, 0.08f, musicVolume));
        StartCoroutine(FadeOut(chip, 0.08f, 0));
        StartCoroutine(FadeOut(classic, 0.08f, 0));
        StartCoroutine(FadeOut(piano, 0.08f, 0));
    }

    IEnumerator FadeIn(AudioSource track, float speed, float maxVolume)
    {
        keepFadingIn = true;
        keepFadingOut = false;
        float totalTime = 0.7f; // fade audio in over 0.7 seconds
        float currentTime = 0;
        while (track.volume < musicVolume)
        {
            currentTime += Time.deltaTime;
            track.volume = Mathf.Lerp(0, musicVolume, currentTime / totalTime);
            yield return 1f;
        }
    }

    IEnumerator FadeOut(AudioSource track, float speed, float minVolume)
    {
        keepFadingIn = false;
        keepFadingOut = true;
        float totalTime = 0.7f; // fade audio out over 0.7 seconds
        float currentTime = 0;
        while (track.volume > 0)
        {
            currentTime += Time.deltaTime;
            track.volume = Mathf.Lerp(1, 0, currentTime / totalTime);
            yield return null;
        }
    }
}
