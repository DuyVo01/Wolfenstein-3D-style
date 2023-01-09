using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnvironmentObjects : MonoBehaviour
{
    public static AudioSource objectAudio;
    
    private void Awake()
    {
        objectAudio = GetComponent<AudioSource>();
    }

    public static void PlayObjectAudio(AudioClip clip)
    {
        objectAudio.clip = clip;
        objectAudio.Play();
    }
}
