using UnityEngine;
using System.IO; 
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveLoad
{
	private static string _path = Application.persistentDataPath + "/gamesave";
	private static BinaryFormatter _formatter = new BinaryFormatter();

	public static void SaveGame(int score = 0)
	{
		FileStream fs = new FileStream(_path, FileMode.OpenOrCreate);

		MaxScore data = new MaxScore(SettingsScript.Music, SettingsScript.Sounds, SettingsScript.MusicClip, score);

		_formatter.Serialize(fs, data);

		fs.Close();

	}

	public static MaxScore LoadGame()
	{
		if (File.Exists(_path))
		{
			FileStream fs = new FileStream(_path, FileMode.Open);

			MaxScore data = _formatter.Deserialize(fs) as MaxScore;

			SettingsScript.Music = data.MusicVolume;
			SettingsScript.Sounds = data.SoundsVolume;
			SettingsScript.MusicClip = data.Soundtrack;
			SettingsScript.MaxScore = data.MaxScoreToSave;

			fs.Close();

			return data;
		}
		else
		{
			return null;
		}

	}
}
