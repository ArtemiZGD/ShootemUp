using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MaxScore
{
    public int maxScore;

    public int soundtrack;

    public float musicVolume;

    public float soundsVolume;

    public MaxScore(float music, float sounds, int clip, int score)
    {
        if (score > maxScore)
        {
            maxScore = score;
        }
        musicVolume = music;
        soundsVolume = sounds;
        soundtrack = clip;
    }
}
