using UnityEngine;

public class GameManager : MonoBehaviour
{
	private void Start()
	{
		SettingsScript.Score = 0;

		MaxScore data = SaveLoad.LoadGame();

		if (!data.Equals(null))
		{
			SettingsScript.MaxScore = data.MaxScoreToSave;
		}
		else
		{
			SettingsScript.MaxScore = 0;
		}
	}
}
