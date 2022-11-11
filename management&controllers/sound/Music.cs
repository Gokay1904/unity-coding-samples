using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class Sound
{
    public string name;

    public AudioClip musicClip;

    [Range(0f, 2f)]
    public float volume;
    [Range(0f, 2f)]
    public float pitch;

    public AudioSource musicSource;

}
[Serializable]
public class SoundWithAmbient : Sound
{

    public AudioClip ambientClip;

    [HideInInspector]
    public AudioSource ambientSource;

}
