using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class ChangeGameplayMusic : MonoBehaviour
{
    public AudioClip[] musics;

    private AudioSource audioSource;

    private int clipNum;

    private void Start()
    {
        clipNum = 0;
        audioSource = GetComponent<AudioSource>();

        SetClip();
    }

    private void SetClip()
    {
        audioSource.clip = musics[clipNum];
        audioSource.volume = SettingsScript.music;

        audioSource.Play();
    }
}
