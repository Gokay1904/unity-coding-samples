using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagement : MonoBehaviour
{
    private static SoundManagement _soundManagement;
    public static SoundManagement soundManagement 
    {
        get
        {
            return _soundManagement;
        }
    }

    public float soundFadingTime;
    private float currentTime = 0f;

    [SerializeField]
    private Coroutine _playingCoroutine;
    public Coroutine playingCoroutine { get { return _playingCoroutine; } set { _playingCoroutine = value; } }

    public SoundWithAmbient[] soundsWithAmbient;

    private SoundWithAmbient playingSoundWithAmbient;

    public Sound[] sounds;

    private AudioClip audioClip;

    private void Awake()
    {
        _soundManagement = this.GetComponent<SoundManagement>();

        foreach (SoundWithAmbient s in soundsWithAmbient)
        {
            s.musicSource = gameObject.AddComponent<AudioSource>();
            s.musicSource.clip = s.musicClip;
            s.musicSource.volume = s.volume;
            s.musicSource.pitch = s.pitch;

            s.ambientSource = gameObject.AddComponent<AudioSource>();
            s.ambientSource.clip = s.ambientClip;
            s.ambientSource.volume = s.volume;
            s.ambientSource.pitch = s.pitch;
         }

        
        foreach (Sound s in sounds)
        {
            s.musicSource = gameObject.AddComponent<AudioSource>();
            s.musicSource.clip = s.musicClip;
            s.musicSource.volume = s.volume;
            s.musicSource.pitch = s.pitch;
         }

    }

    public void PlaySound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s.musicClip != null)
        {
            s.musicSource.Play();
        }
    }

    public Sound GetSound(string name)
    {
        return Array.Find(sounds, sound => sound.name == name);
    }
    public void PlaySoundWithAmbient(string name)
    {
        SoundWithAmbient s = Array.Find(soundsWithAmbient, soundWithAmbient => soundWithAmbient.name == name);

        if (s.musicClip != null) 
        {
            s.musicSource.Play();
        }
        if (s.ambientClip!=null) {
            if (s.ambientClip != null)
            {
                s.ambientSource.loop = true;
                s.ambientSource.Play();
                playingSoundWithAmbient = s;
            }
        }   
    }
    public IEnumerator AmbientWithMusic()
    {
        currentTime = 0;
        while (playingSoundWithAmbient.musicSource.volume < 1f || playingSoundWithAmbient.ambientSource.volume < 1f)
        {
            currentTime += Time.deltaTime;
            playingSoundWithAmbient.musicSource.volume = Mathf.Lerp(playingSoundWithAmbient.musicSource.volume,1,currentTime / soundFadingTime);
            playingSoundWithAmbient.ambientSource.volume = Mathf.Lerp(playingSoundWithAmbient.ambientSource.volume,1,currentTime / soundFadingTime);
            yield return null;
        }

        playingSoundWithAmbient.musicSource.volume = 1;
        playingSoundWithAmbient.ambientSource.volume = 1;
    }

    public IEnumerator OnlyMusic()
    {
        currentTime = 0;
        while (playingSoundWithAmbient.musicSource.volume < 1f || playingSoundWithAmbient.ambientSource.volume > 0f)
        {
            currentTime += Time.deltaTime;
            playingSoundWithAmbient.musicSource.volume = Mathf.Lerp(0,1, currentTime / soundFadingTime);
            playingSoundWithAmbient.ambientSource.volume = Mathf.Lerp(1, 0, currentTime / soundFadingTime);
            yield return null;
        }

        playingSoundWithAmbient.musicSource.volume = 1;
        playingSoundWithAmbient.ambientSource.volume = 0;
        playingCoroutine = null;
    }

    public IEnumerator OnlyAmbient()
    {
        currentTime = 0;
        while (playingSoundWithAmbient.musicSource.volume > 0f|| playingSoundWithAmbient.ambientSource.volume < 1f)
        {
            currentTime += Time.deltaTime;
            playingSoundWithAmbient.musicSource.volume = Mathf.Lerp(1, 0, currentTime / soundFadingTime);
            playingSoundWithAmbient.ambientSource.volume = Mathf.Lerp(0, 1, currentTime / soundFadingTime);
            yield return null;
        }
     
        playingSoundWithAmbient.musicSource.volume = 0;
        playingSoundWithAmbient.ambientSource.volume = 1;
    }
    public IEnumerator SoundOff()
    {
        currentTime = 0;
        while (playingSoundWithAmbient.musicSource.volume > 0f || playingSoundWithAmbient.ambientSource.volume > 0f)
        {
            currentTime += Time.deltaTime;
            playingSoundWithAmbient.musicSource.volume = Mathf.Lerp(playingSoundWithAmbient.musicSource.volume, 0, currentTime / soundFadingTime);
            playingSoundWithAmbient.ambientSource.volume = Mathf.Lerp(playingSoundWithAmbient.ambientSource.volume, 0, currentTime / soundFadingTime);
            yield return null;
        }

        playingSoundWithAmbient.musicSource.volume = 0;
        playingSoundWithAmbient.ambientSource.volume = 0;
    }

    

   
    
}
