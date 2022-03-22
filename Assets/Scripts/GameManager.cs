using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        SettingsScript.score = 0;

        MaxScore data = SaveLoad.LoadGame();

        if (!data.Equals(null))
        {
            SettingsScript.maxScore = data.maxScore;
        }
        else
        {
            SettingsScript.maxScore = 0;
        }
    }
}
