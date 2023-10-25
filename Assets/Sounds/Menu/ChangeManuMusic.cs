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
        audioSource.clip = musics[SettingsScript.MusicClip];
        audioSource.volume = SettingsScript.Music;
        soundtrack.text = "Soundtrack" + (SettingsScript.MusicClip + 1);

        audioSource.Play();
    }

    public void NextClip()
    {
        if (SettingsScript.MusicClip + 1 >= musics.Length)
        {
            SettingsScript.MusicClip = 0;
        }
        else
        {
            SettingsScript.MusicClip++;
        }
        soundtrack.text = "Soundtrack" + (SettingsScript.MusicClip + 1);
        audioSource.clip = musics[SettingsScript.MusicClip];
        audioSource.Play();
    }

    public void LastClip()
    {
        if (SettingsScript.MusicClip <= 0)
        {
            SettingsScript.MusicClip = musics.Length - 1;
        }
        else
        {
            SettingsScript.MusicClip--;
        }
        soundtrack.text = "Soundtrack" + (SettingsScript.MusicClip + 1);
        audioSource.clip = musics[SettingsScript.MusicClip];
        audioSource.Play();
    }
}
