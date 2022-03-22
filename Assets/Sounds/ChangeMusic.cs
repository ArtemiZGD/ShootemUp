using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class ChangeMusic : MonoBehaviour
{
    public Text text;
    public AudioClip[] musics;

    private AudioClip audioClip;
    private int clipNum;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        clipNum = 0;
        SetClip(clipNum);
    }

    public void NextClip()
    {
        if (clipNum + 1 >= musics.Length)
        {
            clipNum = 0;
            SetClip(clipNum);
        }
        else
        {
            SetClip(++clipNum);
        }
    }

    public void LastClip()
    {
        if (clipNum == 0)
        {
            clipNum = musics.Length - 1;
            SetClip(clipNum);
        }
        else
        {
            SetClip(--clipNum);
        }
    }

    private void SetClip(int num)
    {
        audioClip = musics[num];
        audioSource.clip = audioClip;
        if (text != null)
        {
            text.text = audioClip.name;
        }
        audioSource.Play();
    }
}
