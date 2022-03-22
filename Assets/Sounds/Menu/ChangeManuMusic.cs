using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class ChangeManuMusic : MonoBehaviour
{
    [SerializeField] private Text soundtrack;
    public AudioClip[] musics;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        SetClip();
    }

    private void SetClip()
    {
        audioSource.clip = musics[SettingsScript.musicClip];
        audioSource.volume = SettingsScript.music;
        soundtrack.text = "Soundtrack" + (SettingsScript.musicClip + 1);

        audioSource.Play();
    }

    public void NextClip()
    {
        if (SettingsScript.musicClip + 1 >= musics.Length)
        {
            SettingsScript.musicClip = 0;
        }
        else
        {
            SettingsScript.musicClip++;
        }
        soundtrack.text = "Soundtrack" + (SettingsScript.musicClip + 1);
        audioSource.clip = musics[SettingsScript.musicClip];
        audioSource.Play();
    }

    public void LastClip()
    {
        if (SettingsScript.musicClip <= 0)
        {
            SettingsScript.musicClip = musics.Length - 1;
        }
        else
        {
            SettingsScript.musicClip--;
        }
        soundtrack.text = "Soundtrack" + (SettingsScript.musicClip + 1);
        audioSource.clip = musics[SettingsScript.musicClip];
        audioSource.Play();
    }
}
