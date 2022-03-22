using UnityEngine;
using System.IO; 
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveLoad
{
	private static string path = Application.persistentDataPath + "/gamesave";
	private static BinaryFormatter formatter = new BinaryFormatter();

	public static void SaveGame(int score = 0)
	{
		FileStream fs = new FileStream(path, FileMode.OpenOrCreate);

		MaxScore data = new MaxScore(SettingsScript.music, SettingsScript.sounds, SettingsScript.musicClip, score);

		formatter.Serialize(fs, data);

		fs.Close();

	}

	public static MaxScore LoadGame()
	{
		if (File.Exists(path))
		{
			FileStream fs = new FileStream(path, FileMode.Open);

			MaxScore data = formatter.Deserialize(fs) as MaxScore;

			SettingsScript.music = data.musicVolume;
			SettingsScript.sounds = data.soundsVolume;
			SettingsScript.musicClip = data.soundtrack;
			SettingsScript.maxScore = data.maxScore;

			fs.Close();

			return data;
		}
		else
		{
			return null;
		}

	}
}
